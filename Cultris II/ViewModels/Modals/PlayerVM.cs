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
    public class PlayerVM : UserInfoBaseVM
    {
        private readonly string country;

        public Command FUFButtonCommand { get; set; }

        private Color fufButtonColor = Color.Green;
        private string fufButtonText = "Follow";
        public Color FUFButtonColor
        {
            get => fufButtonColor;
            set => SetProperty(ref fufButtonColor, value);
        }

        public string FUFButtonText
        {
            get => fufButtonText;
            set => SetProperty(ref fufButtonText, value);
        }
        public PlayerVM(Player player) 
        {
            UserId = player.Id.ToString();
            country = player.Country; 
        }
        private bool fufButtonEnabled = false;
        public bool FUFButtonEnabled
        {
            get => fufButtonEnabled;
            set => SetProperty(ref fufButtonEnabled, value,"",FUFButtonCommand.ChangeCanExecute);
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
                    FUFButtonColor = Color.Red;
                    FUFButtonText = "Unfollow";
                    FUFButtonCommand = new Command(UnfollowPlayer, () => FUFButtonEnabled);
                }
                else 
                {
                    FUFButtonColor = Color.Green;
                    FUFButtonText = "Follow";
                    FUFButtonCommand = new Command(FollowPlayer, () => FUFButtonEnabled);
                }
                FUFButtonEnabled = true;
            }
        }

        private void FollowPlayer()
        {
            DataService.AddPick(new Pick { Name = Username, PlayerId = UserId, AvatarHash = ImageSourceGravatar, Country = country});
            FUFButtonEnabled = false;
        }
        private void UnfollowPlayer()
        {
            DataService.DeletePick(UserId);
            FUFButtonEnabled = false;
        }

    }
}
