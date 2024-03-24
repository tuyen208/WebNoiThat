
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebShop.Extension
{
    public static class SessionExtensions
    {
        public static void Set<T>(this HttpSessionStateBase session, string key, T value)
        {
            session[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this HttpSessionStateBase session, string key)
        {
            var value = session[key] as string;

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
