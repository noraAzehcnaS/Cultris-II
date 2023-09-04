using Cultris_II.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomsPage : ContentPage
    {
        readonly RoomsVM viewModel = new RoomsVM();
        public RoomsPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsBusy = true;
        }
    }
}