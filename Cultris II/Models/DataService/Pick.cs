using System;
using System.Collections.Generic;
using System.Text;

namespace Cultris_II.Models.DataService
{
    public class Pick
    {
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string AvatarHash { get; set; }
        public string Country { get; set; }
        public int Followers { get; set; }
    }
}
