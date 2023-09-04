using Cultris_II.Models.C2API;
using Cultris_II.ViewModels.Base;
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
                Players.Add(p);
            }
        }
    }
}
