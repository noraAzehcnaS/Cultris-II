namespace Cultris_II.Models.C2API
{
    public class Stats
    {
        public int UserId { get; set; }
        public int PlayedRounds { get; set; }
        public int MaxCombo { get; set; }
        public int Wins { get; set; }
        public double Playedmin { get; set; }
        public double MaxroundBpm { get; set; }
        public double AvgroundBpm { get; set; }
        public double Score { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
    }
}
