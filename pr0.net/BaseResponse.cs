using Newtonsoft.Json;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class BaseResponse
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "ts")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeStamp { get; private set; }

        [JsonProperty(PropertyName = "rt")]
        public int RT { get; private set; }

        [JsonProperty(PropertyName = "qc")]
        public int QC { get; private set; }

        [JsonProperty(PropertyName = "cache")]
        public string Cache { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public BaseResponse()
        {

        }
        #endregion
    }
}
