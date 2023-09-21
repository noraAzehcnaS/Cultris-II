using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views;
using Cultris_II.Views.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Cultris_II.ViewModels.Base.ButtonVM;

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

        private readonly ButtonSettings loginButtonSettings = new ButtonSettings { Text = "Login", TextColor = Color.White, BackgroundColor = Color.Green };
        public ButtonVM LoginButton { get; set; }

        public LoginVM()
        {
            LoginButton = new ButtonVM
            {
                Settings = loginButtonSettings,
                ActionCommand = async _ => await OnLoginClicked(),
                IsEnabled = false
            };
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
                        ForegroundService.StartService(1,"Test",new TimeSpan(0,0,1));
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
