using Cultris_II.Models.C2API;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cultris_II.Services
{
    public static class C2API_Service
    {
        private const string API = "https://gewaltig.net/api/";
        private const string liveInfo = "liveInfo";
        private const string user = "user/";
        private const string g_API = "https://www.gravatar.com/avatar/";
        private const string g_size = "?s=800";

        public static Session CurrentSession = null;
        private static readonly HttpClient client = new HttpClient();
        public static async Task<string> GetUserIdFromGame()
        {
            string username = await DataService.GetUsername();
            Uri uri = new Uri($"{API}{liveInfo}");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                CurrentSession = JsonConvert.DeserializeObject<Session>(content);
                string result = GetUserIdByName(username);
                return result;
            }
            return string.Empty;
        }

        public static async Task<User> GetUserInfo(string userId)
        {
            Uri uri = new Uri($"{API}{user}{userId}");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(content);
            }
            return null;
        }

        public static string GravatarFromHash(string hash)
        {
            return $"{g_API}{hash}{g_size}";
        }
        private static string GetUserIdByName(string username) 
        {
            return CurrentSession?
                .Players?
                .FirstOrDefault(p => p.Name.Equals(username))?
                .Id.ToString()
                ?? string.Empty;
        }
    }
}
