using Newtonsoft.Json;

namespace Quiron.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ToConvert<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}