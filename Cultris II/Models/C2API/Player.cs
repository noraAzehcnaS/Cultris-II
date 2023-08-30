using System;
using System.Collections.Generic;
using System.Text;

namespace Cultris_II.Models.C2API
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Guest { get; set; }
        public int Currentscore { get; set; }
        public bool Afk { get; set; }
        public object Room { get; set; }
        public bool Team { get; set; }
        public string Challenge { get; set; }
        public string Avatarhash { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
    }
}
