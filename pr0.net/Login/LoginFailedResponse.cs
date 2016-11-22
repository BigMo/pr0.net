using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Login
{
    public class LoginFailedResponse : BaseResponse
    {
        //"{\"success\":false,\"ban\":null,\"ts\":1479850082,\"cache\":null,\"rt\":2,\"qc\":1}"
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; private set; }

        [JsonProperty(PropertyName = "ban")]
        public string Ban{ get; private set; }

        public override string ToString()
        {
            if (Ban == null)
                return string.Format("[{0} {1}] Login failed: Invalid credentials", TimeStamp.ToShortDateString(), TimeStamp.ToLongTimeString());
            else
                return string.Format("[{0} {1}] Login failed, you were banned: {2}", TimeStamp.ToShortDateString(), TimeStamp.ToLongTimeString(), Ban);
        }
    }
}
