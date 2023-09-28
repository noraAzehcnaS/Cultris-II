using Cultris_II.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        readonly LoginVM vm;
        public LoginPage()
        {
            InitializeComponent();
            vm = new LoginVM();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (!(page is LoginPage))
                {
                    Navigation.RemovePage(page);
                }
            }
            _ = vm.GetLogin();
        }
    }
}