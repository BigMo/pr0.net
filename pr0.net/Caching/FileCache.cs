using Newtonsoft.Json;
using pr0.net.Feed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pr0.net.Caching
{
    public class FileCache
    {
        #region VARIABLES
        private DirectoryInfo root;
        [JsonProperty]
        private List<MediaData> thumbs;
        [JsonProperty]
        private List<MediaData> images;
        [JsonProperty]
        private List<MediaData> fullsize;
        #endregion

        #region PROPERTIES
        public DirectoryInfo RootDirectory { get { return root; } }
        #endregion

        #region CONSTRUCTORS
        public FileCache(DirectoryInfo rootDirectory)
        {
            root = rootDirectory;
            thumbs = new List<MediaData>();
            images = new List<MediaData>();
            fullsize = new List<MediaData>();

            LoadCache();
        }
        #endregion

        #region METHODS
        private void LoadCache()
        {
            LoadCache(MediaPresentation.Thumb);
            LoadCache(MediaPresentation.Image);
            LoadCache(MediaPresentation.Fullsize);
        }

        private void LoadCache(MediaPresentation pres)
        {
            DirectoryInfo subDir = new DirectoryInfo(Path.Combine(root.FullName, pres.ToString()));
            if (!subDir.Exists)
            {
                subDir.Create();
                return;
            }

            foreach (var file in subDir.GetFiles())
            {
                try
                {
                    int idx = file.Name.LastIndexOf('.');
                    string name = file.Name.Substring(0, idx);

                    MediaData d = new MediaData(int.Parse(name),
                        file.FullName,
                        file.Extension == ".webm" || file.Extension == ".mp4" ? MediaType.Video : MediaType.Image,
                        pres);
                    if (!this[pres].Any(x => x.Id == d.Id))
                        this[pres].Add(d);
                }
                catch
                {
                    try
                    {
                        file.Delete();
                    }
                    catch { }
                }
            }
        }
        public List<MediaData> this[MediaPresentation pres]
        {
            get
            {
                switch (pres)
                {
                    case MediaPresentation.Thumb:
                        return thumbs;
                    case MediaPresentation.Image:
                        return images;
                    case MediaPresentation.Fullsize:
                        return fullsize;
                }
                return thumbs;
            }
        }
        public string CraftPath(MediaPresentation presentation, string mediaPath)
        {
            DirectoryInfo subDir = new DirectoryInfo(Path.Combine(root.FullName, presentation.ToString()));

            int idx = mediaPath.LastIndexOf('/');
            string fileName = mediaPath.Substring(idx + 1, mediaPath.Length - idx - 1);
            return Path.Combine(subDir.FullName, fileName);
        }
        #endregion
    }
}
