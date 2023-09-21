using Cultris_II.Models.DataService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public enum Subscription
    {
        PROFESSIONAL_BATTLEFIELD,
        VETERAN_LOUNGE,
        BEGINNER_PARTY,
        CHEESEMAGEDDON,
        TEAMS
    }
    public interface IDataService
    {
        bool AddSubscription(Subscription sub);
        bool DeleteSubscription(Subscription sub);
        Task<List<Subscription>> GetSubscriptions();
        bool AddPick(Pick pick);
        bool DeletePick(string pickId);
        Task<List<Pick>> GetPicks();
        Task<string> GetUsername();
        Task<string> GetUserId();
        Task<bool> IsUsernameRegistered();
        bool RegisterUsername(string username);
        bool RegisterUserId(string userId);
    }
    public static class DataService
    {
        private static readonly IDataService dataService = DependencyService.Get<IDataService>();

        public static bool DeleteSubscription(Subscription sub) => dataService.DeleteSubscription(sub);
        public static bool AddSubscription(Subscription sub) => dataService.AddSubscription(sub);
        public static async Task<List<Subscription>> GetSubscriptions() => await dataService.GetSubscriptions();
        public static bool AddPick(Pick pick) => dataService.AddPick(pick);
        public static bool DeletePick(string pickId) => dataService.DeletePick(pickId);
        public static async Task<List<Pick>> GetPicks() => await dataService.GetPicks();
        public static async Task<bool> IsUsernameRegistered() => await dataService.IsUsernameRegistered();
        public static async Task<string> GetUsername() => await dataService.GetUsername();
        public static async Task<string> GetUserId() => await dataService.GetUserId();
        public static bool RegisterUsername(string username) => dataService.RegisterUsername(username); 
        public static bool RegisterUserId(string userId) => dataService.RegisterUserId(userId);
    }
}
