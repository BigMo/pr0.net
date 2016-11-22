using Newtonsoft.Json;
using pr0.net.Feed;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using pr0.net.Utils;
using pr0.net.Login;
using pr0.net.ErrorHandling;

namespace pr0.net
{
    public class pr0API
    {
        #region VARIABLES
        private const string DEFAULT_USERAGENT = "Mozilla / 5.0(Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
        private const string PR0URL = "https://pr0gramm.com";
        private RestClient client;
        private List<RestResponseCookie> cookies;
        #endregion

        #region PROPERTIES
        private RestResponseCookie SessionCookie
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
        public SessionData Session
        {
            get
            {
                if (!IsLoggedIn)
                    return null;

                //try
                //{
                
                    string json = RestSharp.Extensions.MonoHttp.HttpUtility.UrlDecode(SessionCookie.Value);
                    return new Newtonsoft.Json.JsonSerializer().Deserialize<SessionData>(json);
                //}
                //catch
                //{
                //    return null;
                //}
            }
        }
        #endregion

        #region CONSTRUCTORS
        public pr0API()
        {
            client = new RestClient(PR0URL);
            cookies = new List<RestResponseCookie>();
        }
        #endregion

        #region METHODS
        private RestRequest CraftRequest(string url, Method method)
        {
            var request = new RestRequest(url, method);
            request.RequestFormat = DataFormat.Json;
            foreach (var cookie in cookies)
                request.AddCookie(cookie.Name, cookie.Value);
            request.AddHeader("User-Agent", DEFAULT_USERAGENT);
            request.JsonSerializer = new Utils.JsonSerializer();

            return request;
        }
        private void ProcessResponse(IRestResponse response)
        {
            if (response.ErrorException != null)
                throw response.ErrorException;

            //Add all the cookies to our safe
            foreach (var cookie in response.Cookies)
                if (!cookies.Any(x => x.Equals(cookie)))
                    cookies.Add(cookie);

        }
        private T Consume<T>(string url, Action<RestRequest> prepare, Action<IRestResponse<T>> evaluate, Method method = Method.GET) where T : new()
        {
            var request = CraftRequest(url, method);
            prepare?.Invoke(request);

            IRestResponse<T> response = client.Execute<T>(request);
            ProcessResponse(response);
            evaluate?.Invoke(response);

            return response.Data;
        }
        private void Consume(string url, Action<RestRequest> prepare, Action<IRestResponse> evaluate, Method method = Method.GET)
        {
            var request = CraftRequest(url, method);
            prepare?.Invoke(request);

            IRestResponse response = client.Execute(request);
            ProcessResponse(response);
            evaluate?.Invoke(response);
        }


        public FeedResponse GetFeed(string tags = null, FeedFlags flags = FeedFlags.SFW, bool promoted = false, FeedItem olderThan = null)
        {
            return Consume<FeedResponse>("api/items/get",
                (request) =>
                {
                    request.AddParameter("flags", (int)flags);
                    if (promoted)
                        request.AddParameter("promoted", 1);
                    if (olderThan != null)
                        request.AddParameter("older", olderThan.Id);
                    if (!string.IsNullOrEmpty(tags))
                        request.AddParameter("tags", tags);
                },
                null);
        }
        public FeedResponse GetNextFeed(string tags, FeedFlags flags, bool promoted, FeedResponse previous)
        {
            return GetFeed(tags, flags, promoted, previous.Items.Last());
        }
        public void Login(string username, string password)
        {
            if (IsLoggedIn)
                return;

            Consume("api/user/login",
                (request) =>
                {
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("name", username);
                    request.AddParameter("password", password);
                },
                (response) =>
                {
                    if (response.ErrorException != null)
                        throw response.ErrorException;
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new ErrorException(response);

                    if (!response.Cookies.Any(x => x.Name == "me"))
                    {
                        if (!string.IsNullOrEmpty(response.Content))
                        {
                            LoginFailedResponse lfr = null;
                            try
                            {
                                lfr = new Newtonsoft.Json.JsonSerializer().Deserialize<LoginFailedResponse>(response.Content);
                            } catch
                            {
                                throw new Exception(string.Format("Server returned: \"{0}\"", response.Content));
                            }
                            throw new Exception(lfr.ToString());
                        }
                        throw new Exception("Unexpected response");
                    }
                }, Method.POST);
        }
        public void Logout()
        {
            //TODO: Implement!
        }
        #endregion
    }
}
