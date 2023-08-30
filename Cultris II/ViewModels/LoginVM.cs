using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views;
using Cultris_II.Views.Navigation;
using Xamarin.Forms;

namespace Cultris_II.ViewModels
{
    public class LoginVM : BaseVM
    {
        private string email;
        private string password;
        private bool CanLogin(bool param) => EntriesHaveText;
        private bool CheckEntriesHaveText() => !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password));
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, "EntriesHaveText");
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value, "EntriesHaveText");
        }

        public bool EntriesHaveText
        {
            get => CheckEntriesHaveText();
        }

        public Command LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new Command<bool>(OnLoginClicked, CanLogin);
        }
        private async void OnLoginClicked(bool param)
        {
            if (await AuthService.LoginUser(email,password))
            {
                if(await DataService.IsUsernameRegistered())
                {
                    await Navigation().PushAsync(new FP());
                }
                else
                {
                    await Navigation().PushAsync(new RegisterPage());
                }
            }

        }
    }
}
