using Cultris_II.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        readonly ProfileVM viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            viewModel = new ProfileVM();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = viewModel.SetProfileContent();
        }
    }
}