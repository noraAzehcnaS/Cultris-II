using Cultris_II.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FPFlyout : ContentPage
    {
        public ListView ListView;

        public FPFlyout()
        {
            InitializeComponent();

            BindingContext = new FPFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}