
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

        public static CollectionReference Picks() => UserByAuthId().Collection("picks");
        public static DocumentReference UserByAuthId() => FirebaseFirestore.Instance.Collection("users").Document(FirebaseAuth.Instance.CurrentUser.Uid);
        public static Query UserByPlayerId(string userId) => FirebaseFirestore.Instance.Collection("users").WhereEqualTo("userId", userId);
        public static Query UserByUsername(string username) => FirebaseFirestore.Instance.Collection("users").WhereEqualTo("userId", username);
        public static bool IsFieldValid(DocumentSnapshot user, string key) => !string.IsNullOrEmpty(user?.GetString(key));

        public static HashMap UserField(string username, string userId, int follows)
        {
            Dictionary<string, Object> keyValuePairs = new Dictionary<string, Object>
            {
                { "username", username },
                { "userId", userId },
                { "follows", follows }
            };
            return new HashMap(keyValuePairs);
        }
    }
}