using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public sealed class FileScanData : IFileScanData, ITreeLeave
    {
        public string Path => fileInfo.FullName;
        public string Name => fileInfo.Name;
        public string Extension => fileInfo.Extension;
        public SizeData Size { get; }
        public DateTime CreationTime => fileInfo.CreationTime;
        public DateTime LastAccessTime => fileInfo.LastAccessTime;
        public DateTime LastWriteTime => fileInfo.LastWriteTime;

        private readonly FileInfo fileInfo;

        public FileScanData(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            Size = new SizeData(fileInfo.Length);
            //Path = fileInfo.FullName;
            //Weight = fileInfo.Length;
        }
    }
}
