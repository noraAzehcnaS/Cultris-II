using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Navigation;
using Xamarin.Forms;

namespace Cultris_II.ViewModels
{
    public class RegisterVM : BaseVM
    {
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value, "", RegisterCommand.ChangeCanExecute);
        }

        public Command RegisterCommand { get; }

        public RegisterVM()
        {
            RegisterCommand = new Command(OnRegisterClicked, CanRegister);
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
