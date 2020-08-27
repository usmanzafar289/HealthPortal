using Hanssens.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace CarePortal.Web.Extensions
{
    public static class LocalStorageExtensions
    {
        public static string Store(StorageType key, string value)
        {
            string response = string.Empty;
            try
            {
                using (var storage = new LocalStorage())
                {
                    storage.Store(key.ToString(), value);
                    storage.Persist();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        public static string Get(StorageType key)
        {
            string response = string.Empty;
            try
            {
                using (var storage = new LocalStorage())
                {
                    response = storage.Get(key.ToString()).ToString();
                    storage.Persist();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        public static bool Exists(StorageType key)
        {
            bool response = false;
            try
            {
                using (var storage = new LocalStorage())
                {
                    response = storage.Exists(key.ToString());
                }
            }
            catch (Exception ex)
            {
                response = false;
            }
            return response;
        }

        public static string Clear()
        {
            string response = string.Empty;
            try
            {
                using (var storage = new LocalStorage())
                {
                    storage.Clear();
                    storage.Destroy();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }
    }

    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, StorageType key, string value)
        {
            session.SetString(key.ToString(), JsonConvert.SerializeObject(value));
        }

        public static object GetObject(this ISession session, StorageType key)
        {
            var value = session.GetString(key.ToString());
            return value == null ? string.Empty : JsonConvert.DeserializeObject(value);
        }
    }

    public enum StorageType
    {
        Token = 0,
        UserId = 1,
        Username = 2,
        Picture = 3,
        Role = 4,
        Name = 5
    }
}
