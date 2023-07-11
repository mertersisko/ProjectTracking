using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.SecurityModels;
using System.Text;

namespace ProjectTracking.Bussiness.Session;

public static class SessionExtentions
{
    public static void SessionAssignData<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T SessionGetData<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
    public static User FetchUser(this ISession session)
    {
        return session.SessionGetData<User>("user");
    }
}
