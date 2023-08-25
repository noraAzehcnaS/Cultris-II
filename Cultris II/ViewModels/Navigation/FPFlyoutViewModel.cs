using Cultris_II.Models.Navigation;
using Cultris_II.Views;
using Cultris_II.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cultris_II.ViewModels.Navigation
{
    public class FPFlyoutViewModel
    {
        public ObservableCollection<FPFlyoutMenuItem> MenuItems { get; set; }

        public FPFlyoutViewModel()
        {
            MenuItems = new ObservableCollection<FPFlyoutMenuItem>(new[]
            {
                    new FPFlyoutMenuItem { Id = 0, Title = "Profile", TargetType = typeof(ProfilePage) , Icon = "players_circle_icon.png"},
                    new FPFlyoutMenuItem { Id = 1, Title = "Rooms", TargetType = typeof(RoomsPage), Icon = "room_icon.png"},
                    new FPFlyoutMenuItem { Id = 2, Title = "Rankings", TargetType = typeof(RankingsPage), Icon = "ranking_icon.png" },
                    new FPFlyoutMenuItem { Id = 3, Title = "Picks", TargetType = typeof(PicksPage) , Icon = "ranking_icon.png"},
                    new FPFlyoutMenuItem { Id = 4, Title = "Subscriptions", TargetType = typeof(SubscriptionsPage),Icon = "ranking_icon.png" },
                });
        }
    }
}
