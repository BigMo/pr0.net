using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pr0.net.Caching
{
    public class MediaData
    {
        #region PROPERTIES
        public int Id { get; private set; }
        public string Path { get; private set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MediaType Type { get; private set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MediaPresentation Presentation { get; private set; }
        #endregion

        #region CONSTRUCTORS
        public MediaData()
        {

        }
        public MediaData(int id, string path, MediaType type, MediaPresentation presentation)
        {
            Id = id;
            Path = path;
            Type = type;
            Presentation = presentation;
        }
        #endregion
    }
}
