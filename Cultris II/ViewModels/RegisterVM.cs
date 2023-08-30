using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Navigation;
using Cultris_II.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cultris_II.ViewModels
{
    public class RegisterVM : BaseVM
    {
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value, "EntriesHaveText");
        }

        public bool EntriesHaveText
        {
            get => CheckEntriesHaveText();
        }

        private bool CheckEntriesHaveText() => !string.IsNullOrEmpty(Username);

        public Command RegisterCommand { get; }

        public RegisterVM()
        {
            RegisterCommand = new Command<bool>(OnRegisterClicked, CanRegister);
        }

        private bool CanRegister(bool param)
        {
            return EntriesHaveText;
        }

        private void OnRegisterClicked(bool param)
        {
            if(DataService.RegisterUsername(Username))
            {
                Navigation().PushAsync(new FP());
            }
        }
    }
}
