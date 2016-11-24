using pr0.net;
using pr0.net.Feed;
using pr0.net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Feed
{
    public class FeedRequest
    {
        #region VARIABLES
        private FeedRequest older, newer;
        #endregion

        #region PROPERTIES
        public FeedFlags Flags { get; set; }
        public FeedItemResponse OlderThan { get; set; }
        public FeedItemResponse NewerThan { get; set; }
        public string ByUser { get; set; }
        public string Tags { get; set; }
        public bool PromotedOnly { get; set; }
        public FeedResponse Response { get; private set; }
        public FeedRequest Older
        {
            get
            {
                if (older == null)
                    older = CreateOlder();

                return older;
            }
        }
        public FeedRequest Newer
        {
            get
            {
                if (newer == null)
                    newer = CreateNewer();

                return newer;
            }
        }
        #endregion

        #region CONSTRUCTORS
        public FeedRequest()
        {
            Flags = FeedFlags.SFW;
        }
        #endregion

        #region METHODS
        public FeedResponse GetResponse(Session session)
        {
            return Response = RestAPI.GetFeed(session, this);
        }
        private FeedRequest CreateOlder()
        {
            return new Builder()
                .ByFlags(this.Flags)
                .ByUser(this.ByUser)
                .Tags(this.Tags)
                .OlderThan(this.Response.Items.Aggregate((c, d) => c.Id < d.Id ? c : d))
                .Build();
        }
        private FeedRequest CreateNewer()
        {
            return new Builder()
                .ByFlags(this.Flags)
                .ByUser(this.ByUser)
                .Tags(this.Tags)
                .OlderThan(this.Response.Items.Aggregate((c, d) => c.Id > d.Id ? c : d))
                .Build();
        }
        #endregion

        public class Builder : Builder<FeedRequest>
        {
            #region METHODS
            public Builder ByFlags(FeedFlags value)
            {
                this.Make.Flags = value;
                return this;
            }
            public Builder OlderThan(FeedItemResponse value)
            {
                this.Make.OlderThan = value;
                return this;
            }
            public Builder NewerThan(FeedItemResponse value)
            {
                this.Make.NewerThan = value;
                return this;
            }
            public Builder ByUser(string value)
            {
                this.Make.ByUser = value;
                return this;
            }
            public Builder Tags(string value)
            {
                this.Make.Tags = value;
                return this;
            }
            #endregion
        }
    }
}
