using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Feed
{
    public class FeedResponse : BaseResponse
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "atEnd")]
        public bool AtEnd { get; private set; }

        [JsonProperty(PropertyName = "atStart")]
        public bool AtStart { get; private set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; private set; }

        [JsonProperty(PropertyName = "items")]
        public List<FeedItem> Items { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public FeedResponse()
        {

        }
        #endregion
    }
}
