using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Identity.Constants;
using Volunteer.Identity.DAL.Entities;
using Volunteer.Identity.Models;

namespace Volunteer.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEventService _events;
        private readonly IClientStore _clientStore;

        public AuthController(IIdentityServerInteractionService interaction, 
            UserManager<AppUser> userManager, 
            IEventService events, 
            IClientStore clientStore)
        {
            _interaction = interaction;
            _userManager = userManager;
            _events = events;
            _clientStore = clientStore;
        }
        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            // check if we are in the context of an authorization request
            //var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            //if (button != "login")
            //{
            //    if (context != null)
            //    {
            //        // if the user cancels, send a result back into IdentityServer as if they 
            //        // denied the consent (even if this client does not require consent).
            //        // this will send back an access denied OIDC error response to the client.
            //        await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);

            //        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
            //        if (await _clientStore.IsPkceClientAsync(context.ClientId))
            //        {
            //            // if the client is PKCE then we assume it's native, so this change in how to
            //            // return the response is for better UX for the end user.
            //            return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
            //        }

            //        return Redirect(model.ReturnUrl);
            //    }
            //    else
            //    {
            //        // since we don't have a valid context, then we just go back to the home page
            //        return Redirect("~/");
            //    }
            //}

            if (ModelState.IsValid)
            {
                // validate username/password
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.Email));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    // issue authentication cookie with subject ID and username
                    await HttpContext.SignInAsync(user.Id, user.UserName, props);

                    //if (context != null)
                    //{
                    //    if (await _clientStore.IsPkceClientAsync(context.ClientId))
                    //    {
                    //        // if the client is PKCE then we assume it's native, so this change in how to
                    //        // return the response is for better UX for the end user.
                    //        return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                    //    }

                    //    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    //    return Redirect(model.ReturnUrl);
                    //}

                    // request for a local page
                    //if (Url.IsLocalUrl(model.ReturnUrl))
                    //{
                    //    return Redirect(model.ReturnUrl);
                    //}
                    //else if (string.IsNullOrEmpty(model.ReturnUrl))
                    //{
                    //    return Redirect("~/");
                    //}
                    //else
                    //{
                    //    // user might have clicked on a malicious link - should be logged
                    //    throw new Exception("invalid return URL");
                    //}
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            return Ok(model);
        }
    }
}
