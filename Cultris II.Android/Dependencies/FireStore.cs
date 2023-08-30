using Cultris_II.Droid.Dependencies;
using Cultris_II.Services;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

using static Cultris_II.Droid.Dependencies.Helpers.FirebaseAccess;

[assembly: Dependency(typeof(FireStore))]
namespace Cultris_II.Droid.Dependencies
{
    public class FireStore : IDataService
    {
        public bool DeletePick(string playerId)
        {
            Picks().Document(playerId).Delete();
            return true;
        }

        public bool AddPick(string playerId)
        {
            Picks().Document(playerId).Set(FieldEntry("playerId",playerId));
            return true;
        }

        public async Task<List<string>> GetPicks()
        {
            var listener = new FirebaseListener<QuerySnapshot>();
            Picks().Get().AddOnCompleteListener(listener);
            QuerySnapshot result = await listener.Task;

            return PlayersFromQuery(result);
        }
        public async Task<bool> IsUsernameRegistered()
        {
            var listener = new FirebaseListener<DocumentSnapshot>();
            User().Get().AddOnCompleteListener(listener);
            DocumentSnapshot result = await listener.Task;

            return IsUserValid(result, "username");
        }
        public async Task<string> GetUserField(string key)
        {
            var listener = new FirebaseListener<DocumentSnapshot>();
            User().Get().AddOnCompleteListener(listener);
            DocumentSnapshot result = await listener.Task;
            if (IsUserValid(result, key))
            {
                return result.GetString(key);
            }
            return string.Empty;
        }

        public async Task<string> GetUsername()
        {
            return await GetUserField("username");
        }

        public async Task<string> GetUserId()
        {
            return await GetUserField("userId");
        }

        public bool RegisterUsername(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                User().Set(GetUserFields());
                User().Update("username", username);
                Picks().Document(username).Set(GetUserFields());
                return true;
            }
            return false;
        }

        public bool RegisterUserId(string userId)
        {
            User().Update("userId", userId);
            return true;
        }
    }
}