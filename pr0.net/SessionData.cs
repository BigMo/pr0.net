﻿using Newtonsoft.Json;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class SessionData
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "lv")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastVisited { get; private set; }

        [JsonProperty(PropertyName = "t")]
        public int T { get; private set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "n")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "a")]
        public int Admin { get; private set; }

        [JsonProperty(PropertyName = "pp")]
        public int PP { get; private set; }

        [JsonProperty(PropertyName = "paid")]
        public bool Paid { get; private set; }

        [JsonIgnore]
        public string Nonce { get { return Id.Substring(0, 16); } }
        #endregion
    }
}
