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
        public const string FolderIconKey = "folder";
        public const string FileIconKey = "file";
        private static readonly ImageList ImageList = new ImageList();

        static IconPool()
        {
            var projectDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            ImageList.Images.Add(FolderIconKey, Image.FromFile($@"{projectDirectory}\Resources\folder.png"));
            ImageList.Images.Add(FileIconKey, Image.FromFile($@"{projectDirectory}\Resources\file.png"));
        }

        public static ImageList GetImageList()
            => ImageList;
    }
}
