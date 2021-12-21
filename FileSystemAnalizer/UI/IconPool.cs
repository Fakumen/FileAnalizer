using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAnalizer.UI
{
    public static class IconPool
    {
        public const string FolderIconKey = "explorer";
        private static readonly ImageList ImageList = new ImageList();

        static IconPool()
        {
            Add(FolderIconKey, Icon.ExtractAssociatedIcon(@"c:\windows\explorer.exe"));
        }

        public static bool Contains(string key)
            => ImageList.Images.ContainsKey(key);

        public static void Add(string key, Icon icon)
        {
            if (!Contains(key))
            {
                ImageList.Images.Add(key, icon);
            }
        }

        public static ImageList GetImageList()
            => ImageList;

        public static int GetIndex(string key)
            => ImageList.Images.IndexOfKey(key);

        public static Image Get(string key)
            => ImageList.Images[key];
    }
}
