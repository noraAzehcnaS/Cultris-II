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

        private const string GravatarAPI = "https://www.gravatar.com/avatar/";
        private const string FlagAPI = "https://flagsapi.com/";

        private static readonly HttpClient client = new HttpClient();
        public static async Task<string> GetUserIdFromGame() => GetUserIdFromSession(await DataService.GetUsername(), await GetLiveInfo());

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

        public static string GravatarFromHash(string hash) => (hash.StartsWith(GravatarAPI)) ? hash : $"{GravatarAPI}{hash}?s=800";
        public static string FlagFromCountry(string country) => (country.StartsWith(FlagAPI)) ? country : $"https://flagsapi.com/{country}/flat/64.png";

        public static async Task<Session> GetLiveInfo()
        {
            Uri uri = new Uri($"{API}{liveInfo}");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Session>(content);
            }
            return null;
        }
        private static string GetUserIdFromSession(string username, Session session) 
        {
            return session?
                .Players?
                .FirstOrDefault(p => p.Name.Equals(username))?
                .Id.ToString()
                ?? string.Empty;
        }
    }
}
