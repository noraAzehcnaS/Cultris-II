using Cultris_II.Models.C2API;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cultris_II.Services
{
    public static class C2API_Service
    {
        public static User CurrentUser = null;
        public static Session CurrentSession = null;
        private static readonly HttpClient client = new HttpClient();
        public static async void ReloadInfo()
        {
            Uri uri = new Uri("https://gewaltig.net/api/user/"+DataService.GetUserId());
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                CurrentUser = JsonConvert.DeserializeObject<User>(content);
            }
        }

        public static async Task<string> GetUserIdFromGame()
        {
            string username = await DataService.GetUsername();
            Uri uri = new Uri("https://gewaltig.net/api/liveinfo");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                CurrentSession = JsonConvert.DeserializeObject<Session>(content);
                return GetUserIdByName(username);
            }
            return string.Empty;
        }

        private static string GetUserIdByName(string username) 
        {
            if(CurrentSession != null)
            {
                foreach (Player player in CurrentSession.Players)
                {
                    if (player.Name.Equals(username))
                    {
                        return player.Id.ToString();
                    }
                }
            }
            return string.Empty;
        }
    }
}
