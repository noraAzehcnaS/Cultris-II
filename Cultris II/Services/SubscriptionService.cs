using Cultris_II.Models.C2API;
using Cultris_II.Models.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public static class SubscriptionService
    {
        private static Session session;
        private static readonly XamarinForegroundService _getSession = new XamarinForegroundService("C2 Watcher", TimeSpan.FromSeconds(45),async () => session = await C2API_Service.GetLiveInfo());
        private static readonly XamarinForegroundService _watchForPicks = new XamarinForegroundService("C2 Picks", TimeSpan.FromSeconds(50), async () => await CheckForPicks());
        private static readonly XamarinForegroundService _watchForSubscriptions = new XamarinForegroundService("C2 Subscriptions", TimeSpan.FromSeconds(50), async () => await CheckForSubscriptions());

        public static void Stop()
        {
            _getSession.Stop();
            _watchForPicks.Stop();
            _watchForSubscriptions.Stop();
        }
        public static void Start()
        {
            _getSession.Start();
            _watchForPicks.Start();
            _watchForSubscriptions.Start();
        }
        private static async Task CheckForPicks() 
        {
            if(session == null) return;

            List<Pick> picks = await DataService.GetPicks();
            List<Pick> filteredPicks = picks
                .Where(p => session.Players.Any(player => p.PlayerId.Equals(player.Id.ToString())))
                .ToList();
            if (filteredPicks.Count == 0)
            {
                _watchForPicks.ClearNotification();
                return;
            }

            string notificationMessage = string.Empty;
            foreach(var pick in filteredPicks) 
            {
                notificationMessage += pick.Name + ", ";
            }
            notificationMessage = notificationMessage.Remove(notificationMessage.Length - 2);
            _watchForPicks.DoNotification(notificationMessage);
        }

        private static async Task CheckForSubscriptions()
        {
            List<Subscription> subscriptions = await DataService.GetSubscriptions();
            if(subscriptions.Count == 0) return;

            string notificationMessage = string.Empty;

            if (subscriptions.Contains(Subscription.PROFESSIONAL_BATTLEFIELD)) { notificationMessage += await CheckProRoom(); }
            if (subscriptions.Contains(Subscription.VETERAN_LOUNGE)) { notificationMessage += CheckVeteranRoom(); }
            if (subscriptions.Contains(Subscription.BEGINNER_PARTY)) { notificationMessage += CheckBeginnerRoom(); }
            if (subscriptions.Contains(Subscription.CHEESEMAGEDDON)) { notificationMessage += CheckCheeseRoom(); }
            if (subscriptions.Contains(Subscription.TEAMS)) { notificationMessage += CheckTeamsRoom(); }

            if(string.IsNullOrEmpty(notificationMessage))
            {
                _watchForSubscriptions.ClearNotification();
            }
            else
            {
                _watchForSubscriptions.DoNotification(notificationMessage);
            }
        }

        private static async Task<string> CheckProRoom() 
        {
            string msg = string.Empty;
            if (session == null) return msg;

            Room ffa = session.Rooms.First(r => r.Id.Equals(0));
            if (ffa.Players == 0) return msg;

            User bestPlayer = await C2API_Service.GetUserInfo(ffa.Bestplayer.Id.ToString());
            if (bestPlayer == null) return msg;

            if (bestPlayer.Stats.Rank < 11)
            {
                msg += "Pro's are lurking around!";
            }
            return msg;
        }
        private static string CheckVeteranRoom() 
        {
            if (session == null) return string.Empty; ;

            Room ffa = session.Rooms.First(r => r.Id.Equals(0));
            if (ffa.Players < 4) return string.Empty; ;

            return "FFA is Active";
        }
        private static string CheckBeginnerRoom() 
        {
            if (session == null) return string.Empty;

            Room rookieP = session.Rooms.First(r => r.Id.Equals(1));
            if (rookieP.Players < 4) return string.Empty;

            return "Rookie Playground is active! ";
        }
        private static string CheckCheeseRoom() 
        {
            if (session == null) return string.Empty;

            Room cheeseRoom = session.Rooms.First(r => r.Id.Equals(2));
            if (cheeseRoom.Players < 4) return string.Empty;
            
            return "CHEESEMAGEDDON! ";
        }
        private static string CheckTeamsRoom() 
        {
            string msg = string.Empty;
            if (session == null) return msg;

            List<Room> teamRooms = session.Rooms.Where(r => r.Teamplay).ToList();
            foreach(Room room in teamRooms)
            {
                var players = session.PlayersInRoom(room.Id);
                if (session.PlayersInRoom(room.Id).Count % 2 != 0 && players.Count != 0)
                {
                    msg += $"{room.Name} might need a player! ";
                }
            }
            return msg;
        }
    }
}
