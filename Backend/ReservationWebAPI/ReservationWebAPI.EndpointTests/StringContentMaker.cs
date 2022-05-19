using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ReservationWebAPI.EndpointTests
{
    public static class StringContentMaker
    {
        public static StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
