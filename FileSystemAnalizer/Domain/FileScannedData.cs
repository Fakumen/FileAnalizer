using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public sealed class FileScannedData : IScannedData
    {
        public string Path => fileInfo.FullName;
        public string Name => fileInfo.Name;
        public string Extension => fileInfo.Extension;
        public long Weight => fileInfo.Length;
        public DateTime CreationTime => fileInfo.CreationTime;
        public DateTime LastAccessTime => fileInfo.LastAccessTime;
        public DateTime LastWriteTime => fileInfo.LastWriteTime;

        private readonly FileInfo fileInfo;

        public FileScannedData(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            //Path = fileInfo.FullName;
            //Weight = fileInfo.Length;
        }
    }
}
