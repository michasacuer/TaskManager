namespace TaskManager.Contracts
{
    using System.Linq;

    internal static class UrlBuilder
    {
        public static string BuildEndponit(string baseUrl, string controller) => $"{baseUrl}/{controller}";
                                           
        public static string BuildEndpoint(string baseUrl, string controller, int id) => $"{baseUrl}/{controller}/{id}";
                                           
        public static string BuildEndpoint(string baseUrl, string controller, string method) => $"{baseUrl}/{controller}/{method}";
                                           
        public static string BuildEndpoint(string baseUrl, string controller, string method, int id) => $"{baseUrl}/{controller}/{method}/{id}";
                                           
        public static string BuildEndpoint(string baseUrl, string controller, params string[] parameters)
            => baseUrl + @"/" + controller + parameters.Aggregate(string.Empty, (current, next) => current + @"/" + next);
    }
}
