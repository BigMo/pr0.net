using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using pr0.net;
using pr0.net.Caching;
using pr0.net.Feed;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pr0.net_Demo
{
    public partial class frmTest : MetroFramework.Forms.MetroForm
    {
        Session s = new Session();
        FileCache c = new FileCache(new DirectoryInfo("cache"));

        public frmTest()
        {
            InitializeComponent();            
        }

        private void frmTest_Shown(object sender, EventArgs e)
        {
            var req = new FeedRequest.Builder().ByFlags(FeedFlags.SFW).Tags("kadse").Build();
            var res = req.GetResponse(s);
            foreach (var itm in res.Items)
            {
                FeedItem i = new FeedItem(c, itm);
                imlThumbs.Images.Add(i.Response.Id.ToString(), Image.FromFile(i.Thumb.Value.Path));
                ListViewItem ltv = new ListViewItem();
                ltv.Text = i.Response.Id.ToString();
                ltv.ImageKey = ltv.Text;
                listView1.Items.Add(ltv);
            }
        }

        private void frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText("cache.json", JsonConvert.SerializeObject(c, Formatting.Indented));//, converters));
        }

        public class CustomComparerDictionaryCreationConverter<T> : CustomCreationConverter<IDictionary>
        {
            private IEqualityComparer<T> comparer;
            public CustomComparerDictionaryCreationConverter(IEqualityComparer<T> comparer)
            {
                if (comparer == null)
                    throw new ArgumentNullException("comparer");
                this.comparer = comparer;
            }

            public override bool CanConvert(Type objectType)
            {
                return HasCompatibleInterface(objectType)
                    && HasCompatibleConstructor(objectType);
            }

            private static bool HasCompatibleInterface(Type objectType)
            {
                return objectType.GetInterfaces()
                    .Where(i => HasGenericTypeDefinition(i, typeof(IDictionary<,>)))
                    .Where(i => typeof(T).IsAssignableFrom(i.GetGenericArguments().First()))
                    .Any();
            }

            private static bool HasGenericTypeDefinition(Type objectType, Type typeDefinition)
            {
                return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeDefinition;
            }

            private static bool HasCompatibleConstructor(Type objectType)
            {
                return objectType.GetConstructor(new Type[] { typeof(IEqualityComparer<T>) }) != null;
            }

            public override IDictionary Create(Type objectType)
            {
                return Activator.CreateInstance(objectType, comparer) as IDictionary;
            }
        }
    }
}
