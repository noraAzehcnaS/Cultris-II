using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Cultris_II.ViewModels.Pages
{
    public class SubscriptionVM : BaseVM
    {
        public class PropertyButton : BaseVM
        {
            private LazyButton _button;
            public LazyButton Button { get => _button; set => SetProperty(ref _button, value); }
        }

        public List<PropertyButton> Buttons { get;} = new List<PropertyButton>();
        public SubscriptionVM() 
        {
            foreach (Subscription sub in Enum.GetValues(typeof(Subscription)))
            {
                Buttons.Insert((int)sub, new PropertyButton()
                {
                    Button = new LazyButton(() => { }, "Loading")
                    {
                        BackgroundColor = Color.Gray,
                        IsEnabled = false
                    }
                });
            }
        }

        public async Task LoadSubscriptions()
        {
            List<Subscription> subs = await DataService.GetSubscriptions();

            foreach (Subscription sub in Enum.GetValues(typeof(Subscription)))
            {
                if(subs.Contains(sub))
                {
                    Buttons[(int)sub].Button = new LazyButton(() =>
                    {
                        DataService.DeleteSubscription(sub);
                        Buttons[(int)sub].Button.IsEnabled = false;
                    }, "Unsubscribe")
                    { BackgroundColor = Color.Red };
                }
                else
                {
                    Buttons[(int)sub].Button = new LazyButton(() =>
                    {
                        DataService.AddSubscription(sub);
                        Buttons[(int)sub].Button.IsEnabled = false;
                    }, "Subscribe");
                }
            }
        }
    }
}
