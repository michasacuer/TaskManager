namespace TaskManager.Tests.WebFunctional.Extensions
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TaskManager.Application.Queries;

    public static class HttpClientCredentialExtension
    {
        public static async Task GetMockManagerCredential(this HttpClient client)
        {
            var loginModel = new LoginQuery
            {
                UserName = "username2",
                Password = "password11"
            };

            var user = await SendLoginRequest(client, loginModel);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Bearer);
        }

        public static async Task<LoginModel> GetMockManagerCredentialWithUserInfo(this HttpClient client)
        {
            var loginModel = new LoginQuery
            {
                UserName = "username2",
                Password = "password11"
            };

            var user = await SendLoginRequest(client, loginModel);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Bearer);

            return user;
        }

        private static async Task<LoginModel> SendLoginRequest(HttpClient client, LoginQuery loginModel)
        {
            var response = await client.PostAsJsonAsync("Account/Login", loginModel);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json.DeserializeObjectFromJson<LoginModel>();
        }
    }
}
