using pr0.net.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.ErrorHandling
{
    public class ErrorException : Exception
    {
        private static string GetMessageFromRestResponse(IRestResponse response)
        {
            if (string.IsNullOrEmpty(response.Content))
                return "Empty error-message";

            try
            {
                return new Newtonsoft.Json.JsonSerializer().Deserialize<ErrorResponse>(response.Content).ToString();
            }
            catch
            {
                return "Unexpected error-message: " + response.Content;
            }

        }
        public ErrorException(IRestResponse response) : base(GetMessageFromRestResponse(response))
        {
        }
    }
}
