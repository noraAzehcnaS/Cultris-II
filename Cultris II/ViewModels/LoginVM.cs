using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views;
using Cultris_II.Views.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels
{
    public class LoginVM : BaseVM
    {
        private string email;
        private string password;
        private bool CanLogin() => !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password));
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value,"", LoginCommand.ChangeCanExecute);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value,"", LoginCommand.ChangeCanExecute);
        }

        public Command LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new Command(async() => await OnLoginClicked(), CanLogin);
        }
        private async Task OnLoginClicked()
        {
            bool isLoggedIn = false;
            try
            {
                isLoggedIn = await AuthService.LoginUser(email, password);
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", ex.Message,"OK");
            }
            finally 
            { 
                if (isLoggedIn)
                {
                    if (await DataService.IsUsernameRegistered())
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
}
