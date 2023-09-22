using System.Collections.Generic;

namespace Cultris_II.Models.C2API
{
    public class Session
    {
        public List<Player> Players { get; set; }
        public List<Room> Rooms { get; set; }

        public List<Player> PlayersInRoom(int roomId)
        {
            List<Player> players = new List<Player>();
            foreach (Player player in Players)
            {
                if (player.Room is long)
                {
                    if (player.Room.Equals((long)roomId))
                    {
                        players.Add(player);
                    }
                }
            }
            return players;
        }
    }
}
