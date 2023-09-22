using Cultris_II.Models.C2API;
using Cultris_II.Models.DataService;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Modals;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Pages
{
    public class PicksVM : BaseVM
    {
        public ObservableCollection<Player> Players { get; set; }
        public Command<Player> PlayerTappedCommand { get; }

        public PicksVM()
        {
            Players = new ObservableCollection<Player>();
            PlayerTappedCommand = new Command<Player>(PlayerTapped);
        }

        public async Task LoadPicks()
        {
            Players.Clear();
            foreach(Pick pick in await DataService.GetPicks())
            {
                Player player = new Player
                {
                    Name = pick.Name,
                    Avatarhash = pick.AvatarHash,
                    Id = int.Parse(pick.PlayerId),
                    Country = pick.Country
                };
                Players.Add(player);
            }
        }

        private void PlayerTapped(Player player)
        {
            if (!player.Guest)
            {
                Navigation().PushModalAsync(new PlayerModal(player));
            }
        }
    }
}
