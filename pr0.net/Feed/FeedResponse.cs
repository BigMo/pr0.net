using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Feed
{
    public class FeedResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "atEnd")]
        public bool AtEnd { get; private set; }

        [JsonProperty(PropertyName = "atStart")]
        public bool AtStart { get; private set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; private set; }

        [JsonProperty(PropertyName = "items")]
        public List<FeedItemResponse> Items { get; private set; }
    }
}
