using Cultris_II.Droid.Dependencies;
using Cultris_II.Models.DataService;
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
        public bool DeletePick(string pickId)
        {
            Picks().Document(pickId).Delete();
            return true;
        }

        public bool AddPick(Pick pick)
        {
            Picks().Document(pick.PlayerId).Set(PickToFields(pick));
            return true;
        }

        public async Task<List<Pick>> GetPicks()
        {
            var listener = new FirebaseListener<QuerySnapshot>();
            Picks().Get().AddOnCompleteListener(listener);
            QuerySnapshot result = await listener.Task;

            return PicksFromQuery(result);
        }
        public async Task<bool> IsUsernameRegistered()
        {
            var listener = new FirebaseListener<DocumentSnapshot>();
            UserByAuthId().Get().AddOnCompleteListener(listener);
            DocumentSnapshot result = await listener.Task;

            return IsFieldValid(result, "name");
        }
        public async Task<string> GetUserField(string key)
        {
            var listener = new FirebaseListener<DocumentSnapshot>();
            UserByAuthId().Get().AddOnCompleteListener(listener);
            DocumentSnapshot result = await listener.Task;
            if (IsFieldValid(result, key))
            {
                return result.GetString(key);
            }
            return string.Empty;
        }

        public async Task<string> GetUsername()
        {
            return await GetUserField("name");
        }

        public async Task<string> GetUserId()
        {
            return await GetUserField("playerId");
        }

        public bool RegisterUsername(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                Pick pick = new Pick { Name = username, PlayerId = string.Empty };
                UserByAuthId().Set(PickToFields(pick));
                return true;
            }
            return false;
        }

        public bool RegisterUserId(string userId)
        {
            UserByAuthId().Update("playerId", userId);
            return true;
        }
    }
}