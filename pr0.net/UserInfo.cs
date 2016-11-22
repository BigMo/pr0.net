using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net
{
    public class UserInfo : BaseResponse
    {
        #region PROPERTIES
        [JsonProperty(PropertyName = "user")]
        public User User { get; private set; }

        [JsonProperty(PropertyName = "comments")]
        public List<BasicComment> Comments { get; private set; }

        [JsonProperty(PropertyName = "commentCount")]
        public int CommentCount { get; private set; }

        [JsonProperty(PropertyName = "uploadCount")]
        public int UploadCount { get; private set; }

        [JsonProperty(PropertyName = "uploadCount")]
        public bool LikesArePublic { get; private set; }
        #endregion
    }
}
