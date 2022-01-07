using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public interface IFolderScanData : IScanData, ILazyDataLoader<IFolderScanData>
    {
        //long TotalFilesCount { get; }
        //long TotalFoldersCount { get; }
        //long FoldersCount { get; }
        //long FilesCount { get; }
        //long TotalElementsCount { get; }
        IEnumerable<IFolderScanData> Folders { get; }
        IEnumerable<IFileScanData> Files { get; }

        void Inspect();
        void InspectAll();
    }
}
