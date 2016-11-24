using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Feed
{
    public class FeedItemResponse
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "promoted")]
        public bool Promoted { get; private set; }

        [JsonProperty(PropertyName = "up")]
        public int UpVotes { get; private set; }

        [JsonProperty(PropertyName = "down")]
        public int DownVotes { get; private set; }

        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Created { get; private set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; private set; }

        [JsonProperty(PropertyName = "Thumb")]
        public string Thumb { get; private set; }

        [JsonProperty(PropertyName = "Fullsize")]
        public string Fullsize { get; private set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; private set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        public bool Audio { get; private set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; private set; }

        [JsonProperty(PropertyName = "flags")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FeedFlags Flags { get; private set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "mark")]
        public bool Mark { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public FeedItemResponse()
        {

        }
        #endregion
    }
}
