﻿using Cultris_II.ViewModels.Pages;
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
    public partial class PicksPage : ContentPage
    {
        readonly PicksVM vm;
        public PicksPage()
        {
            InitializeComponent();
            vm = new PicksVM();
            BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadPicks();
        }
    }
}