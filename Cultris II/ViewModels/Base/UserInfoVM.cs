using Cultris_II.Models.C2API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cultris_II.ViewModels.Base
{
    public class UserInfoVM : BaseVM
    {
        private string imageSourceGravatar;
        private string username;
        private string userId;
        private string created;
        private string rank;
        private string score;
        private string winrate;
        private string maxCombo;
        private string maxBPM;
        private string avgBPM;

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

        public UserInfoVM() 
        {
        }

        public void SetUser(User user)
        {
            Username = user.Name;
            Created = user.Created;
            Rank = user.Stats.Rank.ToString();
            Score = user.Stats.Score.ToString("0.00");
            Winrate = (((float)user.Stats.Wins / user.Stats.PlayedRounds) * 100).ToString("0.0") + "%";
            MaxCombo = user.Stats.MaxCombo.ToString();
            MaxBPM = user.Stats.MaxroundBpm.ToString("0.00");
            AvgBPM = user.Stats.AvgroundBpm.ToString("0.00");
            ImageSourceGravatar = user.GravatarHash;
        }
    }
}
