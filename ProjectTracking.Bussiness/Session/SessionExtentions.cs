using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using System.Text;

namespace ProjectTracking.Bussiness.Session;

public static class SessionExtentions
{
    public static void SessionAssignData<T>(this ISession session, string key, T value)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        session.SetString(key, JsonConvert.SerializeObject(value, settings));
        
    }
    public static T? SessionGetData<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        return value == null ? default : JsonConvert.DeserializeObject<T>(value, settings);
    }
    public static User FetchUser(this ISession session)
    {
        return session.SessionGetData<User>("user");
    }
}
