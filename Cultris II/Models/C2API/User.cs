
namespace Cultris_II.Models.C2API
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string GravatarHash { get; set; }
        public Stats Stats { get; set; }
        public Challenges Challenges { get; set; }
    }
}

