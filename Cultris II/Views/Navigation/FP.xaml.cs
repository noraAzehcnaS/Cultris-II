using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cultris_II.Models.Navigation;
using Cultris_II.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FP : FlyoutPage
    {
        private readonly SubscriptionService _service;
        public FP()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            _service = new SubscriptionService();
            _service.StartService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var loginPage = Navigation.NavigationStack.FirstOrDefault(p => p is LoginPage);
            if (loginPage != null)
            {
                Navigation.RemovePage(loginPage);
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is FPFlyoutMenuItem item)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                FlyoutPage.ListView.SelectedItem = item;
            }
        }
    }
}