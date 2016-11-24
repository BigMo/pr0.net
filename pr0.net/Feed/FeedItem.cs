using pr0.net.Caching;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace pr0.net.Feed
{
    public class FeedItem
    {
        #region PROPERTIES
        public FeedItemResponse Response { get; private set; }
        public Lazy<MediaData> Thumb { get; private set; }
        public Lazy<MediaData> Image { get; private set; }
        public Lazy<MediaData> Fullsize { get; private set; }
        public MediaType Type
        {
            get
            {
                return Image.IsValueCreated ? Image.Value.Type :
                    (Response.Image.EndsWith(".webm") || Response.Image.EndsWith(".mp4") ? MediaType.Video : MediaType.Image);
            }
        }
        #endregion

        #region CONSTRUCTORS
        public FeedItem(FileCache cache, FeedItemResponse response)
        {
            Response = response;

            Thumb = new Lazy<MediaData>(() => cache[MediaPresentation.Thumb].Any(x=>x.Id == response.Id) ?
                cache[MediaPresentation.Thumb].First(x => x.Id == response.Id) :
                DownloadMedia(cache, MediaType.Image, MediaPresentation.Thumb, response.Thumb));

            Image = new Lazy<MediaData>(() => cache[MediaPresentation.Image].Any(x => x.Id == response.Id) ?
                cache[MediaPresentation.Image].First(x => x.Id == response.Id) :
                DownloadMedia(cache, Type, MediaPresentation.Image, response.Image));

            Fullsize = new Lazy<MediaData>(() => cache[MediaPresentation.Fullsize].Any(x => x.Id == response.Id) ?
                cache[MediaPresentation.Fullsize].First(x => x.Id == response.Id) :
                DownloadMedia(cache, MediaType.Image, MediaPresentation.Fullsize, response.Fullsize));
        }
        #endregion

        #region METHODS
        private MediaData DownloadMedia(FileCache cache, MediaType type, MediaPresentation pres, string mediaUrl)
        {
            string url = "http://" +
                (pres == MediaPresentation.Image ?
                    (type == MediaType.Image ? "img" : "vid") :
                    pres == MediaPresentation.Thumb ? "thumb" : "full") +
                ".pr0gramm.com/" + mediaUrl;
            string path = cache.CraftPath(pres, this.Response.Id.ToString() + mediaUrl.Substring(mediaUrl.LastIndexOf('.')));

            if (!File.Exists(path))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, path + ".tmp");
                    File.Move(path + ".tmp", path);
                }
            }

            return new MediaData(Response.Id, path, Type, pres);
        }
        #endregion
    }
}
