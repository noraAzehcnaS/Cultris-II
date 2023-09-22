using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
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

        private readonly ButtonSettings proBtnSettings = new ButtonSettings { Text ="Professional Battlefield", TextColor = Color.White, BackgroundColor = Color.Purple};
        private readonly ButtonSettings veteranBtnSettings = new ButtonSettings { Text = "Veteran Lounge", TextColor = Color.White, BackgroundColor = Color.Blue };
        private readonly ButtonSettings beginnerBtnSettings = new ButtonSettings { Text = "Beginner Party", TextColor = Color.White, BackgroundColor = Color.Green };
        private readonly ButtonSettings cheesemageddonBtnSettings = new ButtonSettings { Text = "Cheesemageddon", TextColor = Color.Black, BackgroundColor = Color.Yellow };
        private readonly ButtonSettings teamsBtnSettings = new ButtonSettings { Text = "TEAMS!", TextColor = Color.White, BackgroundColor = Color.Black };
        private readonly ButtonSettings unsubscribe = new ButtonSettings { Text ="Unsubscribe", TextColor = Color.White, BackgroundColor = Color.Red };
        public SubscriptionVM() 
        {
            ProBattleBtn = new ButtonVM { Settings = proBtnSettings, ActionCommand = _ => Subscribe(Subscription.PROFESSIONAL_BATTLEFIELD, ProBattleBtn) };
            VetLoungeBtn = new ButtonVM { Settings = veteranBtnSettings, ActionCommand = _ => Subscribe(Subscription.VETERAN_LOUNGE, VetLoungeBtn) };
            BeginnerPartyBtn = new ButtonVM { Settings = beginnerBtnSettings, ActionCommand = _ => Subscribe(Subscription.BEGINNER_PARTY, BeginnerPartyBtn) };
            CheeseMagaddonBtn = new ButtonVM { Settings = cheesemageddonBtnSettings, ActionCommand = _ => Subscribe(Subscription.CHEESEMAGEDDON, CheeseMagaddonBtn) };
            TeamsBtn = new ButtonVM { Settings = teamsBtnSettings, ActionCommand = _ => Subscribe(Subscription.TEAMS, TeamsBtn) };

        }

        public async Task LoadSubscriptions()
        {
            List<Subscription> subs = await DataService.GetSubscriptions();
            if (subs.Count > 0) 
            {
                if (subs.Contains(Subscription.PROFESSIONAL_BATTLEFIELD))
                {
                    ProBattleBtn.ActionCommand = _ => Unsubscribe(Subscription.PROFESSIONAL_BATTLEFIELD, ProBattleBtn);
                    ProBattleBtn.Settings = unsubscribe;
                }

                if (subs.Contains(Subscription.VETERAN_LOUNGE))
                {
                    VetLoungeBtn.ActionCommand = _ => Unsubscribe(Subscription.VETERAN_LOUNGE, VetLoungeBtn);
                    VetLoungeBtn.Settings = unsubscribe;
                }

                if (subs.Contains(Subscription.BEGINNER_PARTY))
                {
                    BeginnerPartyBtn.ActionCommand = _ => Unsubscribe(Subscription.BEGINNER_PARTY, BeginnerPartyBtn);
                    BeginnerPartyBtn.Settings = unsubscribe;
                }

                if (subs.Contains(Subscription.CHEESEMAGEDDON))
                {
                    CheeseMagaddonBtn.ActionCommand = _ => Unsubscribe(Subscription.CHEESEMAGEDDON, CheeseMagaddonBtn);
                    CheeseMagaddonBtn.Settings = unsubscribe;
                }

                if (subs.Contains(Subscription.TEAMS))
                {
                    TeamsBtn.ActionCommand = _ => Unsubscribe(Subscription.TEAMS, TeamsBtn);
                    TeamsBtn.Settings = unsubscribe;
                }
            }
        }

        private void Unsubscribe(Subscription subscription, ButtonVM button) 
        {
            DataService.DeleteSubscription(subscription);
            button.IsEnabled = false;
        }

        private void Subscribe(Subscription subscription, ButtonVM button)
        {
            DataService.AddSubscription(subscription);
            button.IsEnabled = false;
        }
    }
}
