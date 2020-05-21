using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityModel.OidcConstants;

namespace Volunteer.Identity.Configuration
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
                ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = new [] { IdentityModel.OidcConstants.GrantTypes.ClientCredentials },

            // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

            // scopes that client has access to
                AllowedScopes = { "api1" }
            }
        };
    }
}
