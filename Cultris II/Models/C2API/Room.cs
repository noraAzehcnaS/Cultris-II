using System;
using System.Collections.Generic;
using System.Text;

namespace Cultris_II.Models.C2API
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mode { get; set; }
        public bool Protected { get; set; }
        public bool Teamplay { get; set; }
        public object Maxplayers { get; set; }
        public int Players { get; set; }
        public Bestplayer Bestplayer { get; set; }
        public string ImageSource { get; set; }
    }
}
