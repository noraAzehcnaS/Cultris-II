using Cultris_II.Models.C2API;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Pages
{
    public class ProfileVM : BaseVM
    {
        private string
            imageSourceGravatar,
            username,
            userId,
            created,
            rank,
            score,
            winrate,
            maxCombo,
            maxBPM,
            avgBPM,
            updatesLeft;

        public string ImageSourceGravatar
        {
            get => imageSourceGravatar;
            set => SetProperty(ref imageSourceGravatar, value);
        }
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        public string Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }

        public string Rank
        {
            get => rank;
            set => SetProperty(ref rank, value);
        }

        public string Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        public string Winrate
        {
            get => winrate;
            set => SetProperty(ref winrate, value);
        }

        public string MaxCombo
        {
            get => maxCombo;
            set => SetProperty(ref maxCombo, value);
        }

        public string MaxBPM
        {
            get => maxBPM;
            set => SetProperty(ref maxBPM, value);
        }

        public string AvgBPM
        {
            get => avgBPM;
            set => SetProperty(ref avgBPM, value);
        }
        public string UpdatesLeft
        {
            get => updatesLeft;
            set => SetProperty(ref updatesLeft, value);
        }
        public Command UpdateProfile { get; }

        public ProfileVM()
        {
            Username = string.Empty;
            UserId = string.Empty;
            UpdateProfile = new Command(async() => await UpdateProfileContent(), CanUpdate);
        }

        public async Task SetProfileContent()
        {
            Username = await DataService.GetUsername();
            UserId = await DataService.GetUserId();
            UpdatesLeft = GetUpdatesLeft();

            if (!string.IsNullOrEmpty(UserId) && App.Globals.UpdateCount == 0) 
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
            }

            if(App.Globals.CurrentUser != null) 
            {
                SetProfileContentProperties(App.Globals.CurrentUser);
            }
        }

        private string GetUpdatesLeft()
        {
            int updatesLeft = 5 - App.Globals.UpdateCount;
            return $"Update ({updatesLeft})";
        }

        private void SetProfileContentProperties(User user) 
        {
            if (user != null) 
            {
                Created = user.Created;
                Rank = user.Stats.Rank.ToString();
                Score = user.Stats.Score.ToString("0.00");
                Winrate = (((float)user.Stats.Wins/user.Stats.PlayedRounds)*100).ToString("0.0") +"%";
                MaxCombo = user.Stats.MaxCombo.ToString();
                MaxBPM = user.Stats.MaxroundBpm.ToString("0.00");
                AvgBPM = user.Stats.AvgroundBpm.ToString("0.00");
                ImageSourceGravatar = C2API_Service.GravatarFromHash(user.GravatarHash);
            }
        }

        private bool CanUpdate()
        {
            return (App.Globals.UpdateCount < 5);
        }

        public async Task UpdateProfileContent()
        {
            while (string.IsNullOrEmpty(UserId)) 
            {
                await Task.Delay(500);
                string result = await C2API_Service.GetUserIdFromGame();
                if (!string.IsNullOrEmpty(result))
                {
                    DataService.RegisterUserId(result);
                    UserId = result;
                    break;
                }
            }

            if(CanUpdate())
            {
                App.Globals.CurrentUser = await C2API_Service.GetUserInfo(UserId);
                App.Globals.UpdateCount++;
                SetProfileContentProperties(App.Globals.CurrentUser);
                UpdateProfile.ChangeCanExecute();
                UpdatesLeft = GetUpdatesLeft();
            }
        }
    }
}
