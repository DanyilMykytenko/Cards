using IdentityServer4.Models;
using IdentityServer4;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Cards.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> //Scope - represents what client-app can use, identifier for resources 
            {
                new ApiScope("CardsWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> //Identity Resourse - represents ability of client-app
                                       //to look through subset of 'user statements'
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile() //such as username or birth date
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource> //Api Resource represents permission to all protected resource
                                  //which we are allowed to request access
            {
                new ApiResource("CardsWebAPI", "Web API", new[]
                    {JwtClaimTypes.Name, JwtClaimTypes.Role})
                {
                    Scopes = {"CardsWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client> //who are allowed to request access from us
            {
                new Client
                {
                    ClientId = "cards-web-app", //same id as on client-app
                    ClientName = "Cards Web",
                    AllowedGrantTypes = GrantTypes.Code, //"FLOW PROTOCOL, authorization code" - how client interact with token-service
                                                         //The application exchanges the authorization code for an access token

                    RequireClientSecret = false, //if it's true - SHA256 string, which
                                                 //must be same on a client
                    RequirePkce = true,
                    RedirectUris = //list of uris we will redirect to
                                   //after clien-app authenfication
                    {
                        "http://localhost:3000/signin-oidc"
                    },
                    AllowedCorsOrigins = //list of uris, who allowed to use identity server
                    {
                        "http://localhost:3000"
                    },
                    PostLogoutRedirectUris = //list of uris we will redirect to
                                             //after client-app log out
                    {
                        "http://localhost:3000/signout-oidc"
                    },
                    AllowedScopes = //available scopes for client-app
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CardsWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true //runs transfering token via browser
                }
            };
    }
}
