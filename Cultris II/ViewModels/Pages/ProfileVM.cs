using Cultris_II.Models.C2API;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Cultris_II.ViewModels.Base.ButtonVM;

namespace Cultris_II.ViewModels.Pages
{
    public class ProfileVM : UserInfoVM
    {
        private readonly ButtonSettings updateButtonSettings = new ButtonSettings { TextColor = Color.White, BackgroundColor = Color.Green };
        public ButtonVM UpdateButton { get; set; }

        public ProfileVM()
        {
            Username = string.Empty;
            UserId = string.Empty;
            UpdateButton = new ButtonVM
            {
                Settings = updateButtonSettings,
                ActionCommand = async _ => await UpdateProfileContent()
            };
        }

        public async Task SetProfileContent()
        {
            Username = await DataService.GetUsername();
            UserId = await DataService.GetUserId();
            UpdateButton.Text = $"Update ({5 - App.Globals.UpdateCount})";

            if (App.Globals.CurrentUser == null && !string.IsNullOrEmpty(UserId) && App.Globals.UpdateCount == 0)
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
            }
            SetProfileContentProperties(App.Globals.CurrentUser);
        }

        private void SetProfileContentProperties(User user) 
        {
            if (user == null) 
            {
                UpdateButton.Text = "Update While In-Game";
                return; 
            }
            user.GravatarHash = C2API_Service.GravatarFromHash(user.GravatarHash);
            SetUser(user);
        }

        public async Task UpdateProfileContent()
        {
            while (string.IsNullOrEmpty(UserId)) 
            {
                string result = await C2API_Service.GetUserIdFromGame();
                if (!string.IsNullOrEmpty(result))
                {
                    DataService.RegisterUserId(result);
                    UserId = result;
                    break;
                }
                await Task.Delay(5000);
            }

            if(App.Globals.UpdateCount < 5)
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
                App.Globals.UpdateCount++;
                SetProfileContentProperties(App.Globals.CurrentUser);
                UpdateButton.Text = $"Update ({5 - App.Globals.UpdateCount})";
                UpdateButton.IsEnabled = App.Globals.UpdateCount < 5;
            }
        }
    }
}
