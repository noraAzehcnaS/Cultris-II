using System.Collections.Generic;

namespace Cultris_II.Models.C2API
{
    public class Session
    {
        public List<Player> Players { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
