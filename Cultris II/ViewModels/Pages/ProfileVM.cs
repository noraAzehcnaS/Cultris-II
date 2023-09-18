using Cultris_II.Models.C2API;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Pages
{
    public class ProfileVM : UserInfoBaseVM
    {
        private string updatesLeft;
        public string UpdatesLeft
        {
            get => updatesLeft;
            set => SetProperty(ref updatesLeft, value);
        }
        public Command UpdateProfile { get; }

        public ProfileVM()
        {
            Username = string.Empty;
            UserId = string.Empty;
            UpdateProfile = new Command(async() => await UpdateProfileContent(), CanUpdate);
        }

        public async Task SetProfileContent()
        {
            Username = await DataService.GetUsername();
            UserId = await DataService.GetUserId();
            UpdatesLeft = GetUpdatesLeft();

            if (!string.IsNullOrEmpty(UserId) && App.Globals.UpdateCount == 0) 
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
            }

            if(App.Globals.CurrentUser != null) 
            {
                SetProfileContentProperties(App.Globals.CurrentUser);
            }
        }

        private string GetUpdatesLeft()
        {
            int updatesLeft = 5 - App.Globals.UpdateCount;
            return $"Update ({updatesLeft})";
        }

        private void SetProfileContentProperties(User user) 
        {
            user.GravatarHash = C2API_Service.GravatarFromHash(user.GravatarHash);
            SetUser(user);
        }

        private bool CanUpdate()
        {
            return (App.Globals.UpdateCount < 5);
        }

        public async Task UpdateProfileContent()
        {
            while (string.IsNullOrEmpty(UserId)) 
            {
                await Task.Delay(500);
                string result = await C2API_Service.GetUserIdFromGame();
                if (!string.IsNullOrEmpty(result))
                {
                    DataService.RegisterUserId(result);
                    UserId = result;
                    break;
                }
            }

            if(CanUpdate())
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
                App.Globals.UpdateCount++;
                SetProfileContentProperties(App.Globals.CurrentUser);
                UpdateProfile.ChangeCanExecute();
                UpdatesLeft = GetUpdatesLeft();
            }
        }
    }
}
