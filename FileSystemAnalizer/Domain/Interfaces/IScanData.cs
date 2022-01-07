using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public interface IScanData
    {
        string Path { get; }
        string Name { get; }
        SizeData Size { get; }
        DateTime CreationTime { get; }
        DateTime LastAccessTime { get; }
        DateTime LastWriteTime { get; }
    }
}
