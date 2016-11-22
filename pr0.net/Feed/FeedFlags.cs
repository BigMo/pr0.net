using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Feed
{
    [Flags]
    public enum FeedFlags
    {
        NSFW = 2,
        NSFL = 4,
        SFW = 9
    }
}
