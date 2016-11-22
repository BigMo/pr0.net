using Newtonsoft.Json;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class BasicComment
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "content")]
        public string Content { get; private set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }

        [JsonProperty(PropertyName = "up")]
        public long UpVotes { get; private set; }

        [JsonProperty(PropertyName = "down")]
        public long DownVotes { get; private set; }

        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Created { get; private set; }

        [JsonProperty(PropertyName = "itemId")]
        public string ItemId { get; private set; }

        [JsonProperty(PropertyName = "thumb")]
        public string Thumb { get; private set; }
        #endregion
    }
}
