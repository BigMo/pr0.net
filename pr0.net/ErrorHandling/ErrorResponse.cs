using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.ErrorHandling
{
    public class ErrorResponse
    {
        //"{\"error\": \"limitReached\", \"code\": 429, \"msg\": \"Rate Limit Reached\"}"

        [JsonProperty(PropertyName = "error")]
        public string Error { get; private set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; private set; }

        [JsonProperty(PropertyName = "msg")]
        public string Message { get; private set; }


        public override string ToString()
        {
            return string.Format("Error {0}: {1}", Code, Message);
        }
    }
}
