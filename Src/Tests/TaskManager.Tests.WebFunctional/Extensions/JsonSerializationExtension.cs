namespace TaskManager.Tests.WebFunctional.Extensions
{
    using Newtonsoft.Json;

    public static class ObjectSerializeExtension
    {
        public static string SerializeObjectToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObjectFromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
