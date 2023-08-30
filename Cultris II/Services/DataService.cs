using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public interface IDataService
    {
        bool AddPick(string playerId);
        bool DeletePick(string playerId);
        Task<List<string>> GetPicks();
        Task<string> GetUsername();
        Task<string> GetUserId();
        Task<bool> IsUsernameRegistered();
        bool RegisterUsername(string username);
        bool RegisterUserId(string userId);
    }
    public static class DataService
    {
        private static readonly IDataService dataService = DependencyService.Get<IDataService>();
        public static bool AddPick(string playerId) { return dataService.AddPick(playerId); }
        public static bool DeletePick(string playerId) { return dataService.DeletePick(playerId); }
        public static async Task<List<string>> GetPicks() { return await dataService.GetPicks(); }
        public static async Task<bool> IsUsernameRegistered() { return await dataService.IsUsernameRegistered(); }
        public static async Task<string> GetUsername() { return await dataService.GetUsername(); }
        public static async Task<string> GetUserId() { return await dataService.GetUserId(); }
        public static bool RegisterUsername(string username) { return dataService.RegisterUsername(username); }
        public static bool RegisterUserId(string userId) { return dataService.RegisterUserId(userId); }

    }
}
