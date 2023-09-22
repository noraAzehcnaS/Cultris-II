using Cultris_II.Models.C2API;
using Cultris_II.Models.DataService;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Cultris_II.ViewModels.Base.ButtonVM;

namespace Cultris_II.ViewModels.Modals
{
    public class PlayerVM : UserInfoVM
    {
        private readonly string country;

        private readonly ButtonSettings follow = new ButtonSettings {Text = "Follow", TextColor = Color.White, BackgroundColor = Color.Green };
        private readonly ButtonSettings unfollow = new ButtonSettings { Text = "Unfollow", TextColor = Color.White, BackgroundColor = Color.Red };
        private readonly ButtonSettings loading = new ButtonSettings { Text = "Loading Player Info", TextColor = Color.Black, BackgroundColor = Color.Gray };
        public ButtonVM FUFButton { get; set; }
        public PlayerVM(Player player) 
        {
            UserId = player.Id.ToString();
            country = player.Country;
            FUFButton = new ButtonVM { Settings = loading };
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
                    FUFButton.Settings = unfollow;
                    FUFButton.ActionCommand = _ => UnfollowPlayer();
                }
                else 
                {
                    FUFButton.Settings = follow;
                    FUFButton.ActionCommand = _ => FollowPlayer();
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
