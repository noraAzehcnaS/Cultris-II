using Cultris_II.Models.C2API;
using Cultris_II.Models.DataService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public class SubNotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
    }
    public interface ISubscriptionsService
    {
        void StartService();
        void StopService();
    }
    public class SubscriptionService
    {
        private string subscriptionMsg = string.Empty;
        public SubscriptionService() 
        {
            MessagingCenter.Subscribe<ISubscriptionsService>(this, "SubscriptionService", async(_) => await HandleSubscriptions());
        }
        private readonly ISubscriptionsService _service = DependencyService.Get<ISubscriptionsService>();
        public void StartService() => _service.StartService();
        public void StopService() => _service.StopService();

        private async Task HandleSubscriptions()
        {
            Session session = await C2API_Service.GetLiveInfo();
            if(session == null) { return; }
            await CheckForPicks(session.Players);
            await CheckForSubscriptions(session);

        }

        private async Task CheckForPicks(List<Player> players) 
        {
            if (players == null) { return; }
            string pickString = string.Empty;
            List<Pick> picks = await DataService.GetPicks();
            List<Pick> filteredPicks = picks
                .Where(p => players.Any(player => p.PlayerId.Equals(player.Id.ToString())))
                .ToList();

            if(filteredPicks.Count > 0)
            {
                foreach(var pick in filteredPicks) 
                {
                    pickString += pick.Name + ", ";
                }
            }
            MessagingCenter.Send(this, "SubNotification", new SubNotification { Id = 301, Title = "Your Picks", Message = pickString });
        }

        private async Task CheckForSubscriptions(Session session)
        {
            subscriptionMsg = string.Empty;
            List<Subscription> subscriptions = await DataService.GetSubscriptions();
            if (subscriptions.Contains(Subscription.PROFESSIONAL_BATTLEFIELD)) { await CheckProRoom(session); }
            if (subscriptions.Contains(Subscription.VETERAN_LOUNGE)) { CheckVeteranRoom(session); }
            if (subscriptions.Contains(Subscription.BEGINNER_PARTY)) { CheckBeginnerRoom(session); }
            if (subscriptions.Contains(Subscription.CHEESEMAGEDDON)) { CheckCheeseRoom(session); }
            if (subscriptions.Contains(Subscription.TEAMS)) { CheckTeamsRoom(session); }

            MessagingCenter.Send(this, "SubNotification", new SubNotification { Id = 1001, Title = "Subscriptions", Message = subscriptionMsg });
        }

        private async Task CheckProRoom(Session session) 
        {
            Room ffa = session.Rooms.First(r => r.Id.Equals(0));
            if(ffa.Players == 0) { return; }
            User bestPlayer = await C2API_Service.GetUserInfo(ffa.Bestplayer.Id.ToString());
            if(bestPlayer == null) { return; }
            if (bestPlayer.Stats.Rank < 11)
            {
                subscriptionMsg += "Pro's are lurking around! ";
            }
        }
        private void CheckVeteranRoom(Session session) 
        {
            Room ffa = session.Rooms.First(r => r.Id.Equals(0));
            if (ffa.Players > 4) 
            {
                subscriptionMsg += "FFA is active! ";
            }
        }
        private void CheckBeginnerRoom(Session session) 
        {
            Room rookieP = session.Rooms.First(r => r.Id.Equals(1));
            if (rookieP.Players > 4)
            {
                subscriptionMsg += "Rookie Playground is active! ";
            }
        }
        private void CheckCheeseRoom(Session session) 
        {
            Room cheeseRoom = session.Rooms.First(r => r.Id.Equals(2));
            if (cheeseRoom.Players > 4)
            {
                subscriptionMsg += "CHEESEMAGEDDON! ";
            }
        }
        private void CheckTeamsRoom(Session session) 
        {
            List<Room> teamRooms = session.Rooms.Where(r => r.Teamplay).ToList();
            foreach(Room room in teamRooms)
            {
                var players = session.PlayersInRoom(room.Id);
                if (session.PlayersInRoom(room.Id).Count % 2 != 0 && players.Count != 0)
                {
                    subscriptionMsg += $"{room.Name} might need a player!";
                }
            }
        }
    }
}
