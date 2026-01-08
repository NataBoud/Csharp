using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CaisseEnregistreuse.Services.Helpers
{
    public static class SessionExtensions
    {
        // Sérialiser un objet et le stocker dans la session
        public static void SetObjectAsJson(this ISession session, string key, object value)
            => session.SetString(key, JsonSerializer.Serialize(value));

        // Récupérer un objet depuis la session et le désérialiser
        public static T? GetObjectFromJson<T>(this ISession session, string key)
            => session.GetString(key) is string json ? JsonSerializer.Deserialize<T>(json) : default;
    }
}

