using System;

namespace Cultris_II.Models.Navigation
{
    public class FPFlyoutMenuItem
    {
        public FPFlyoutMenuItem()
        {
            TargetType = typeof(FPFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string Icon { get; set; }
    }
}