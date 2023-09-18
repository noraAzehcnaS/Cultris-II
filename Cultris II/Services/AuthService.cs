using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.Services
{
    public interface IAuthService
    {
        Task<bool> LoginUser(string username, string password);
        bool IsAuthenticated();

        string GetCurrentUserId();
    }
    public static class AuthService
    {
        private static readonly IAuthService auth = DependencyService.Get<IAuthService>();

        public static async Task<bool> LoginUser(string email, string password)
        {
            return await auth.LoginUser(email, password);
        }

        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
