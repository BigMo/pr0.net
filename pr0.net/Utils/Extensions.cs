using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pr0.net.Utils
{
    public static class Extensions
    {
        public static bool Equals(this RestResponseCookie cookie, RestResponseCookie other)
        {
            return cookie.Comment == other.Comment &&
                cookie.CommentUri == other.CommentUri &&
                cookie.Discard == other.Discard &&
                cookie.Domain == other.Domain &&
                cookie.Expired == other.Expired &&
                cookie.Expires == other.Expires &&
                cookie.HttpOnly == other.HttpOnly &&
                cookie.Name == other.Name &&
                cookie.Path == other.Path &&
                cookie.Port == other.Port &&
                cookie.Secure == other.Secure &&
                cookie.TimeStamp == other.TimeStamp &&
                cookie.Value == other.Value &&
                cookie.Version == other.Version;
        }

        public static T Deserialize<T>(this Newtonsoft.Json.JsonSerializer json, string source)
        {
            return Deserialize<T>(json, source, Encoding.UTF8);
        }
        public static T Deserialize<T>(this Newtonsoft.Json.JsonSerializer json, string source, Encoding enc)
        {
            using (MemoryStream mem = new MemoryStream(enc.GetBytes(source)))
                using (StreamReader reader = new StreamReader(mem, enc))
                    return (T)json.Deserialize(reader, typeof(T));
        }
    }
}
