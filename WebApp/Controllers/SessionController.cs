using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    //Static class in the controller namespace. This class provides two methods to store a list in a session
    public static class SessionExtensions
    {
        //Outputs the list from the session
        public static T GetComplexData<T>(this ISession Session, string Key)
        {
            var Data = Session.GetString(Key);
            if (Data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(Data);
        }

        //Sets the session and saves the list to the session
        public static void SetComplexData(this ISession Session, string Key, object Value)
        {
            Session.SetString(Key, JsonConvert.SerializeObject(Value));
        }
    }
}
