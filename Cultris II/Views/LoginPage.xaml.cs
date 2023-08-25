using Cultris_II.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page is LoginPage)
                {
                    //Dont  yourself lol
                }
                else
                {
                    Navigation.RemovePage(page);
                }
            }
        }
    }
}