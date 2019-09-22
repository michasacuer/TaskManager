namespace TaskManager.Contracts
{
    using System.Linq;

    internal static class UrlBuilder
    {
        public static string BaseUrl = "http://localhost:5000";

        public static string BuildEndponit(string controller) => $"{BaseUrl}/{controller}";

        public static string BuildEndpoint(string controller, int id) => $"{BaseUrl}/{controller}/{id}";

        public static string BuildEndpoint(string controller, string method) => $"{BaseUrl}/{controller}/{method}";

        public static string BuildEndpoint(string controller, string method, int id) => $"{BaseUrl}/{controller}/{method}/{id}";

        public static string BuildEndpoint(string controller, params string[] parameters)
            => BaseUrl + @"/" + controller + parameters.Aggregate(string.Empty, (current, next) => current + @"/" + next);
    }
}
