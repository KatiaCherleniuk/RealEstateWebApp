using Newtonsoft.Json.Linq;

namespace RealEstateWebApp.Business.Helpers
{
    public static class ObjectHelper
    {
        public static T Copy<T>(T origin)
        {
            return JObject.FromObject(origin).ToObject<T>();
        }
    }
}