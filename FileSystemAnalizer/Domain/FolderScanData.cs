using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemAnalizer.Infrastructure;

namespace FileSystemAnalizer.Domain
{
    public sealed class FolderScanData : IFolderScanData//, ITree<FolderScanData, FileScanData>
    {
        public long TotalFilesCount => EnsureDataIsReady(totalFilesCount.Value);
        public long TotalFoldersCount => EnsureDataIsReady(totalFoldersCount.Value);
        public SizeData Size => EnsureDataIsReady(size.Value);//Размер (не "На диске")
        public string Path => directoryInfo.FullName;
        public string Name => directoryInfo.Name;
        public DateTime CreationTime => directoryInfo.CreationTime;
        public DateTime LastAccessTime => directoryInfo.LastAccessTime;
        public DateTime LastWriteTime => directoryInfo.LastWriteTime;
        public long FoldersCount => folders.Count();
        public long FilesCount => files.Count();
        public long TotalElementsCount => TotalFilesCount + TotalFoldersCount;
        public IEnumerable<IFolderScanData> Folders => folders;//.Select(f => f) Нельзя сделать даункаст к листу и изменить
        public IEnumerable<IFileScanData> Files => files;
        public bool IsDataReady { get; private set; } = false;

        private readonly Lazy<long> totalFilesCount;
        private readonly Lazy<long> totalFoldersCount;
        private readonly Lazy<SizeData> size;
        private readonly List<FolderScanData> folders = new List<FolderScanData>();
        private readonly List<FileScanData> files = new List<FileScanData>();
        private readonly DirectoryInfo directoryInfo;
        private bool isInspected;
        private int readySubfoldersCount;

        public event Action<IFolderScanData> DataReady;

        public FolderScanData(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
            SizeData sizeFactory() => new SizeData(
                Folders.Sum(f => f.Size.SizeInBytes)
                + Files.Sum(f => f.Size.SizeInBytes));
            size = new Lazy<SizeData>(sizeFactory);
            long totalFoldersCountFactory() => folders.Sum(f => f.TotalFoldersCount) + FoldersCount;
            long totalFilesCountFactory() => folders.Sum(f => f.TotalFilesCount) + FilesCount;
            totalFoldersCount = new Lazy<long>(totalFoldersCountFactory);
            totalFilesCount = new Lazy<long>(totalFilesCountFactory);
        }

        public FolderScanData(string directoryPath) : this(new DirectoryInfo(directoryPath)) { }

        public void Inspect()
        {
            if (isInspected)
                return;
            foreach (var directory in directoryInfo.SafeEnumerateDirectories().Filter(!UserAccess.IsCurrentUserAdmin))
            {
                var folderData = new FolderScanData(directory);
                folderData.DataReady += OnSubfolderDataReady;
                folders.Add(folderData);
            }
            foreach (var file in directoryInfo.EnumerateFiles().Filter(!UserAccess.IsCurrentUserAdmin))
            {
                files.Add(new FileScanData(file));
            }
            if (FoldersCount == 0)
                ClaimDataReady();
            //directoryInfo
            //    .EnumerateDirectories()
            //    .Filter(!UserAccess.IsCurrentUserAdmin)
            //    .AsParallel()
            //    .ForAll(d =>
            //    {
            //        var data = new FolderScanData(d);
            //        lock (folders)
            //            folders.Add(data);
            //    });
            //directoryInfo
            //    .EnumerateFiles()
            //    .Filter(!UserAccess.IsCurrentUserAdmin)
            //    .AsParallel()
            //    .ForAll(f =>
            //    {
            //        var data = new FileScanData(f);
            //        lock (files)
            //            files.Add(data);
            //    });
            isInspected = true;
        }

        public void InspectAll()
        {
            Inspect();
            foreach (var f in Folders)
                f.InspectAll();
        }

        private void OnSubfolderDataReady(IFolderScanData data)
        {
            readySubfoldersCount++;
            if (!IsDataReady && readySubfoldersCount == FoldersCount)
            {
                ClaimDataReady();
                data.DataReady -= OnSubfolderDataReady;
            }
        }

        private void ClaimDataReady()
        {
            IsDataReady = true;
            DataReady?.Invoke(this);
        }

        private T EnsureDataIsReady<T>(T value)
        {
            if (!IsDataReady)
                throw new ObjectNotReadyException($"Object was not ready for use.Please, ensure that object's {nameof(IsDataReady)} property is true before use");
            return value;
        }
    }
}
