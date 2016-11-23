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
    public static class RestAPI
    {
        #region VARIABLES
        private const string DEFAULT_USERAGENT = "Mozilla / 5.0(Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
        private const string PR0URL = "https://pr0gramm.com";
        private static RestClient client = new RestClient(PR0URL);
        #endregion

        #region METHODS
        private static RestRequest CraftRequest(Session session, string url, Method method)
        {
            var request = new RestRequest(url, method);
            request.RequestFormat = DataFormat.Json;
            foreach (var cookie in session.Cookies)
                request.AddCookie(cookie.Name, cookie.Value);
            request.AddHeader("User-Agent", DEFAULT_USERAGENT);
            request.JsonSerializer = new Utils.JsonSerializer();

            return request;
        }
        private static void ProcessResponse(Session session, IRestResponse response)
        {
            if (response.ErrorException != null)
                throw response.ErrorException;

            session.StoreCookies(response.Cookies);
        }
        private static T Consume<T>(Session session, string url, Action<RestRequest> prepare, Action<IRestResponse<T>> evaluate, Method method = Method.GET) where T : new()
        {
            var request = CraftRequest(session, url, method);
            prepare?.Invoke(request);

            IRestResponse<T> response = client.Execute<T>(request);
            ProcessResponse(session, response);
            evaluate?.Invoke(response);

            return response.Data;
        }
        private static void Consume(Session session, string url, Action<RestRequest> prepare, Action<IRestResponse> evaluate, Method method = Method.GET)
        {
            var request = CraftRequest(session, url, method);
            prepare?.Invoke(request);

            IRestResponse response = client.Execute(request);
            ProcessResponse(session, response);
            evaluate?.Invoke(response);
        }

        public static FeedResponse GetFeed(Session session, FeedRequest feedRequest)
        {
            return Consume<FeedResponse>(session, "api/items/get",
                (request) =>
                {
                    request.AddParameter("flags", (int)feedRequest.Flags);
                    if (feedRequest.PromotedOnly)
                        request.AddParameter("promoted", 1);
                    if (feedRequest.OlderThan != null)
                        request.AddParameter("older", feedRequest.OlderThan.Id);
                    if (feedRequest.NewerThan != null)
                        request.AddParameter("newer", feedRequest.NewerThan.Id);
                    if (!string.IsNullOrEmpty(feedRequest.Tags))
                        request.AddParameter("tags", feedRequest.Tags);
                    if (!string.IsNullOrEmpty(feedRequest.ByUser))
                        request.AddParameter("user", feedRequest.ByUser);
                },
                null);
        }
        public static void Login(Session session, string username, string password)
        {
            if (session.IsLoggedIn)
                return;

            Consume(session, "api/user/login",
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
        public static void Logout()
        {
            //TODO: Implement!
        }
        #endregion
    }
}
