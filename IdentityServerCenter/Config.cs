using System.Collections;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;
namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResource()
        {
            return new List<ApiResource> { new ApiResource("api", "My Api") };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client () {
                    ClientId = "client", //客户端模式
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = { new Secret ("secret".Sha256 ()) },
                        AllowedScopes = { "api" }
                        },
                        new Client { //密码模式
                        ClientId = "pwdClient",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        ClientSecrets = {
                        new Secret ("secret".Sha256 ())
                        },
                        RequireClientSecret = false, //client_secret非必传
                        AllowedScopes = { "api" }
                        }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "1",
                        Username = "test",
                        Password = "admin123"
                }
            };
        }
    }
}
