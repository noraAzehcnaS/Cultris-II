using Cultris_II.Models.C2API;
using Cultris_II.Views;
using Xamarin.Forms;

namespace Cultris_II
{
    public partial class App : Application
    {
        public static class Globals
        {
            public static User CurrentUser;
            public static int UpdateCount;
        } 
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            Globals.UpdateCount = 0;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
