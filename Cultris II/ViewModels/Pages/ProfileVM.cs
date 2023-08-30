using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Pages
{
    public class ProfileVM : BaseVM
    {
        private bool stopLookingForId = false;
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public Command LookForIdCommand { get; }

        public ProfileVM()
        {
            Username = string.Empty;
            LookForIdCommand = new Command(UpdateProfileContent);
        }
        public async void SetProfileContent()
        {
            Username = await DataService.GetUsername();
        }

        public async void UpdateProfileContent()
        {
            while (true)
            {
                await Task.Delay(500);
                string result = await C2API_Service.GetUserIdFromGame();
                if (!string.IsNullOrEmpty(result))
                {
                    DataService.RegisterUserId(result);
                    break;
                }
                if (stopLookingForId)
                {
                    break;
                }
            }
        }
    }
}
