
using Cultris_II.Models.DataService;
using Cultris_II.Services;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System.Collections.Generic;
using System;
using Enum = System.Enum;
using Object = Java.Lang.Object;

namespace Cultris_II.Droid.Dependencies.Helpers
{
    public static class FirebaseAccess
    {
        public static List<Pick> PicksFromQuery(QuerySnapshot query)
        {
            List<Pick> picks = new List<Pick>();
            picks.Clear();
            if (query.IsEmpty) { return picks; }

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

        public static List<Subscription> SubcriptionsFromQuery(QuerySnapshot query) 
        {
            List<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Clear();
            if (query.IsEmpty) { return subscriptions; }

            foreach (var doc in query.Documents)
            {
                Subscription sub = SubcriptionFromString(doc.GetString("EVENT"));
                subscriptions.Add(sub);
            }
            return subscriptions;
        }

        public static CollectionReference Subscriptions() => UserByAuthId().Collection("subscriptions");
        public static CollectionReference Picks() => UserByAuthId().Collection("picks");
        public static DocumentReference UserByAuthId() => FirebaseFirestore.Instance.Collection("users").Document(FirebaseAuth.Instance.CurrentUser.Uid);
        public static bool IsFieldValid(DocumentSnapshot user, string key) => !string.IsNullOrEmpty(user?.GetString(key));
        public static string SubcriptionToString(Subscription subscription) => Enum.GetName(typeof(Subscription), subscription);
        public static Subscription SubcriptionFromString(string subscription) => (Subscription)Enum.Parse(typeof(Subscription), subscription);
        public static HashMap SubcriptionToField(Subscription subscription)
        {
            Dictionary<string, Object> keyValuePairs = new Dictionary<string, Object>
            {
                { "EVENT", SubcriptionToString(subscription) },
                { "number_of_players", 6 }
            };
            return new HashMap(keyValuePairs);
        }
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