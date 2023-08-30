
using Firebase.Auth;
using Firebase.Firestore;
using Java.Lang;
using Java.Util;
using System.Collections.Generic;

namespace Cultris_II.Droid.Dependencies.Helpers
{
    public static class FirebaseAccess
    {
        public static List<string> PlayersFromQuery(QuerySnapshot query)
        {
            List<string> players = new List<string>();
            players.Clear();

            foreach (var doc in query.Documents)
            {
                players.Add(doc.Id);
            }
            return players;
        }

        public static CollectionReference Picks() => User().Collection("picks");
        public static DocumentReference User() => FirebaseFirestore.Instance.Collection("users").Document(FirebaseAuth.Instance.CurrentUser.Uid);
        public static bool IsUserValid(DocumentSnapshot user, string key) => !string.IsNullOrEmpty(user?.GetString(key));

        public static HashMap FieldEntry(string field, string entry)
        {
            Dictionary<string, Object> keyValuePairs = new Dictionary<string, Object>
            {
                { field, entry }
            };
            return new HashMap(keyValuePairs);
        }

        public static HashMap GetUserFields()
        {
            Dictionary<string, Object> keyValuePairs = new Dictionary<string, Object>
            {
                { "username", string.Empty },
                { "userId", string.Empty }
            };
            return new HashMap(keyValuePairs);
        }
    }
}