using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Volunteer.Identity.DAL.Configuration
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> Apis =>
        new List<ApiResource>
        {
            new ApiResource("api1", "My API")
        };


        public static IEnumerable<Client> Clients =>
            new List<Client>
       {
            new Client
            {
                RequireConsent = false,
                ClientId = "angular_spa",
                ClientName = "Angular SPA",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = { "openid", "profile", "email", "api.read" },
                RedirectUris = {"http://localhost:4200/auth-callback"},
                PostLogoutRedirectUris = {"http://localhost:4200/"},
                AllowedCorsOrigins = {"http://localhost:4200"},
                AllowAccessTokensViaBrowser = true,
                AccessTokenLifetime = 3600
            }
        };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
