using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cultris_II.Models.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FP : FlyoutPage
    {
        public FP()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
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