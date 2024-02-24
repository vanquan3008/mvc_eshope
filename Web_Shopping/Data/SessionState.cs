﻿using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace Web_Shopping.Data
{
    public static class  SessionState
    {
        public static void SetJson(this ISession session , string key , object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
