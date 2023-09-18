using Cultris_II.Models.C2API;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Modals
{
    public class RoomVM : BaseVM
    {
        public ObservableCollection<Player> Players { get; set; }
        public Command<Player> PlayerTappedCommand { get; }

        public RoomVM(List<Player> players) 
        {
            Players = new ObservableCollection<Player>();
            foreach (Player p in players)
            {
                p.Avatarhash = C2API_Service.GravatarFromHash(p.Avatarhash);
                p.Country = C2API_Service.FlagFromCountry(p.Country);
                Players.Add(p);
            }
            PlayerTappedCommand = new Command<Player>(PlayerTapped); 
        }

        private void PlayerTapped(Player player) 
        {
            if(!player.Guest)
            {
                Navigation().PushModalAsync(new PlayerModal(player));
            }
        }
    }
}
