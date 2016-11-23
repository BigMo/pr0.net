using pr0.net;
using pr0.net.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class Session
    {
        #region VARIABLES
        private List<RestResponseCookie> cookies;
        #endregion

        #region PROPERTIES
        public RestResponseCookie[] Cookies { get { return cookies.ToArray(); } }
        public RestResponseCookie SessionCookie
        {
            get
            {
                if (!cookies.Any(x => x.Name == "me"))
                    return null;
                return cookies.First(x => x.Name == "me");
            }
        }
        public bool IsLoggedIn
        {
            get
            {
                var sessionCookie = SessionCookie;
                if (sessionCookie == null)
                    return false;

                return sessionCookie.Value != "deleted";
            }
        }
        public SessionData Info
        {
            get
            {
                if (!IsLoggedIn)
                    return null;

                try
                {

                    string json = RestSharp.Extensions.MonoHttp.HttpUtility.UrlDecode(SessionCookie.Value);
                    return new Newtonsoft.Json.JsonSerializer().Deserialize<SessionData>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion

        #region CONSTRUCTORS
        public Session()
        {
            cookies = new List<RestResponseCookie>();
        }
        #endregion

        #region METHODS
        public void StoreCookies(IList<RestResponseCookie> cookies)
        {
            foreach (var cookie in cookies)
                if (!this.cookies.Any(x => x.Equals(cookie)))
                    this.cookies.Add(cookie);
        }
        public void LoadSessionCookie(string value)
        {
            if (IsLoggedIn)
                return;

            if (SessionCookie != null)
            {
                SessionCookie.Value = value;
            }
            else
            {
                var cookie = new RestResponseCookie();
                cookie.Name = "me";
                cookie.Value = value;
                cookie.Domain = "pr0gramm.com";
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddYears(10);
                cookies.Add(cookie);
            }
         }
        #endregion
    }
}
