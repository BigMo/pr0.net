using Newtonsoft.Json;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class User
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }

        [JsonProperty(PropertyName = "registered")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Registered { get; private set; }

        [JsonProperty(PropertyName = "score")]
        public int Score { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "mark")]
        public bool Mark { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "admin")]
        public bool Admin { get; private set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "banned")]
        public bool Banned { get; private set; }
        #endregion
    }
}
