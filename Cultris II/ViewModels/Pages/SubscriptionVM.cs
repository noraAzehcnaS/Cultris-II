using Cultris_II.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static Cultris_II.ViewModels.Base.ButtonVM;

namespace Cultris_II.ViewModels.Pages
{
    public class SubscriptionVM : BaseVM
    {
        public ButtonVM ProBattleBtn { get; set; }
        public ButtonVM VetLoungeBtn { get; set; }
        public ButtonVM BeginnerPartyBtn { get; set; }
        public ButtonVM CheeseMagaddonBtn { get; set; }
        public ButtonVM TeamsBtn { get; set; }
    }
}
