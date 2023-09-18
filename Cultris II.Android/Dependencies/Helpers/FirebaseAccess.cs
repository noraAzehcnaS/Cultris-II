
using Cultris_II.Models.DataService;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Lang;
using Java.Util;
using System.Collections.Generic;

namespace Cultris_II.Droid.Dependencies.Helpers
{
    public static class FirebaseAccess
    {
        public static List<Pick> PicksFromQuery(QuerySnapshot query)
        {
            List<Pick> picks = new List<Pick>();
            picks.Clear();

            foreach (var doc in query.Documents)
            {
                Pick pick = new Pick
                {
                    PlayerId = doc.GetString("playerId"),
                    Name = doc.GetString("name"),
                    Country = doc.GetString("country"),
                    AvatarHash = doc.GetString("avatar")
                };
                picks.Add(pick);
            }
            return picks;
        }

        public static CollectionReference Picks() => UserByAuthId().Collection("picks");
        public static DocumentReference UserByAuthId() => FirebaseFirestore.Instance.Collection("users").Document(FirebaseAuth.Instance.CurrentUser.Uid);
        public static bool IsFieldValid(DocumentSnapshot user, string key) => !string.IsNullOrEmpty(user?.GetString(key));

        public static HashMap PickToFields(Pick pick)
        {
            Dictionary<string, Object> keyValuePairs = new Dictionary<string, Object>
            {
                { "name", pick.Name },
                { "playerId", pick.PlayerId },
                { "country", pick.Country },
                { "avatar", pick.AvatarHash },
            };
            return new HashMap(keyValuePairs);
        }
    }
}