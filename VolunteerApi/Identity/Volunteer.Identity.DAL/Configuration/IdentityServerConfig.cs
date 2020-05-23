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
                ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowOfflineAccess = true,
            // scopes that client has access to
                AllowedScopes = { "api1", StandardScopes.OfflineAccess }
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
