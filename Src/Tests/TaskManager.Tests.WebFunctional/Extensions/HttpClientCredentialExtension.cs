namespace TaskManager.Tests.WebFunctional.Extensions
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TaskManager.Application.Queries;

    public static class HttpClientCredentialExtension
    {
        public static async Task GetCredential(this HttpClient client, string username, string password)
        {
            var loginModel = new LoginQuery
            {
                UserName = username,
                Password = password
            };

            var response = await client.PostAsJsonAsync("Account/Login", loginModel);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", json.DeserializeObjectFromJson<LoginModel>().Bearer);
        }
    }
}
