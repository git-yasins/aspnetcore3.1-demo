using System.Net.Http;
using IdentityModel.Client;
namespace ThridPartyDemo {
    /// <summary>
    /// 第三方客户端应用
    /// </summary>
    class Program {
        static void Main (string[] args) {
            #region 客户端模式
            // //访问认证服务器
            // var diso = DiscoveryClient.GetAsync ("http://localhost:5000").Result;
            // if (diso.IsError) {
            //     System.Console.WriteLine (diso.Error);
            // }
            // //拿到TOKEN
            // var tokenClient = new TokenClient (diso.TokenEndpoint, "client", "secret");
            // var tokenResponse = tokenClient.RequestClientCredentialsAsync ("api").Result;
            // if (tokenResponse.IsError) {
            //     System.Console.WriteLine (tokenResponse.Error);
            // } else {
            //     System.Console.WriteLine (tokenResponse.Json);
            // }
            // //访问API
            // var httpClient = new HttpClient ();
            // httpClient.SetBearerToken (tokenResponse.AccessToken);
            // var response = httpClient.GetAsync ("http://localhost:5001/api/weatherforecast").Result;

            // if (response.IsSuccessStatusCode) {
            //     System.Console.WriteLine (response.Content.ReadAsStringAsync ().Result);
            // }
            #endregion

            #region password 授权模式
            //访问认证服务器
            var diso = DiscoveryClient.GetAsync ("http://localhost:5000").Result;
            if (diso.IsError) {
                System.Console.WriteLine (diso.Error);
            }
            //拿到TOKEN
            var tokenClient = new TokenClient (diso.TokenEndpoint, "pwdClient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync ("test", "admin123").Result;
            if (tokenResponse.IsError) {
                System.Console.WriteLine (tokenResponse.Error);
            } else {
                System.Console.WriteLine (tokenResponse.Json);
            }
            //访问API
            var httpClient = new HttpClient ();
            httpClient.SetBearerToken (tokenResponse.AccessToken);
            var response = httpClient.GetAsync ("http://localhost:5001/api/weatherforecast").Result;

            if (response.IsSuccessStatusCode) {
                System.Console.WriteLine (response.Content.ReadAsStringAsync ().Result);
            }
            #endregion

            #region Code模式

            #endregion
        }
    }
}