using Cultris_II.Models.C2API;
using Cultris_II.Models.DataService;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Modals
{
    public class PlayerVM : UserInfoVM
    {
        readonly string country;
        private LazyButton _fufButton = new LazyButton();
        public LazyButton FUFButton { get => _fufButton; set =>SetProperty(ref _fufButton, value); }
        public PlayerVM(Player player) 
        {
            UserId = player.Id.ToString();
            country = player.Country;
            FUFButton.Text = "Loading Player";
            FUFButton.BackgroundColor = Color.Purple;
        }
        public async Task LoadUser()
        {
            User user = await C2API_Service.GetUserInfo(UserId);
            if (user != null)
            {
                user.GravatarHash = C2API_Service.GravatarFromHash(user.GravatarHash);
                SetUser(user);
                List<Pick>picks = await DataService.GetPicks();
                if (picks.Any(pick => pick.PlayerId.Equals(user.UserId.ToString())))
                {
                    FUFButton = new LazyButton(UnfollowPlayer, "Unfollow") { BackgroundColor = Color.Red };
                }
                else 
                {
                    FUFButton = new LazyButton(FollowPlayer, "Follow");
                }
            }
        }

        private void FollowPlayer()
        {
            DataService.AddPick(new Pick { Name = Username, PlayerId = UserId, AvatarHash = ImageSourceGravatar, Country = country});
            FUFButton.IsEnabled = false;
        }
        private void UnfollowPlayer()
        {
            DataService.DeletePick(UserId);
            FUFButton.IsEnabled = false;
        }

    }
}
