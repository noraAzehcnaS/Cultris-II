using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views;
using Cultris_II.Views.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            set => SetProperty(ref email, value,"", () => LoginButton.IsEnabled = CanLogin());
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value,"", () => LoginButton.IsEnabled = CanLogin());
        }
        public LazyButton LoginButton { get; set; }

        public LoginVM()
        {
            LoginButton = new LazyButton(async () => await OnLoginClicked(), "Login") { IsEnabled = false };
            SubscriptionService.Stop();
        }

        public async Task GetLogin()
        {
            Email = await SecureStorage.GetAsync("Email");
            Password = await SecureStorage.GetAsync("Password");
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
                        await SecureStorage.SetAsync("Email", email);
                        await SecureStorage.SetAsync("Password", password);
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
