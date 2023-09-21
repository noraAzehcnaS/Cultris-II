using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Navigation;
using Xamarin.Forms;
using static Cultris_II.ViewModels.Base.ButtonVM;

namespace Cultris_II.ViewModels
{
    public class RegisterVM : BaseVM
    {
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value, "", () => RegisterButton.IsEnabled = CanRegister());
        }

        public ButtonVM RegisterButton { get; set; }
        private readonly ButtonSettings registerBtnSettings = new ButtonSettings { Text = "Register", TextColor = Color.White, BackgroundColor = Color.Purple };

        public RegisterVM()
        {
            RegisterButton = new ButtonVM { Settings = registerBtnSettings, ActionCommand = _=> OnRegisterClicked() , IsEnabled = false};
        }
        private bool CanRegister() => !string.IsNullOrEmpty(Username);
        private void OnRegisterClicked()
        {
            if(DataService.RegisterUsername(Username))
            {
                Navigation().PushAsync(new FP());
            }
        }
    }
}
