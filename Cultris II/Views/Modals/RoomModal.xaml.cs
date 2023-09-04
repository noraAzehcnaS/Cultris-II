using Cultris_II.Models.C2API;
using Cultris_II.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cultris_II.Views.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomModal : ContentPage
	{
        public RoomModal (List<Player> players)
		{
			InitializeComponent ();
            BindingContext = new RoomVM (players);
		}
	}
}