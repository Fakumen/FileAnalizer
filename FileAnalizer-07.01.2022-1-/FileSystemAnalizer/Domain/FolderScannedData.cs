using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemAnalizer.Infrastructure;

namespace FileSystemAnalizer.Domain
{
    public sealed class FolderScannedData : IScanData, ITree<FolderScannedData, FileScannedData>
    {
        public long TotalFilesCount => totalFilesCount.Value;
        public long TotalFoldersCount => totalFoldersCount.Value;
        public SizeData Size => size.Value;//Размер (не "На диске")
        public string Path => directoryInfo.FullName;
        public string Name => directoryInfo.Name;
        public DateTime CreationTime => directoryInfo.CreationTime;
        public DateTime LastAccessTime => directoryInfo.LastAccessTime;
        public DateTime LastWriteTime => directoryInfo.LastWriteTime;
        public long FoldersCount => folders.Count();
        public long FilesCount => files.Count();
        public long TotalElementsCount => TotalFilesCount + TotalFoldersCount;
        public IEnumerable<FolderScannedData> Folders => folders;//.Select(f => f) Нельзя сделать даункаст к листу и изменить
        public IEnumerable<FileScannedData> Files => files;
        public IEnumerable<FolderScannedData> Trees => Folders;
        public IEnumerable<FileScannedData> Leaves => Files;

        private readonly Lazy<long> totalFilesCount;
        private readonly Lazy<long> totalFoldersCount;
        private readonly Lazy<SizeData> size;
        private readonly List<FolderScannedData> folders = new List<FolderScannedData>();
        private readonly List<FileScannedData> files = new List<FileScannedData>();
        private readonly DirectoryInfo directoryInfo;
        private bool isInspected;

        public FolderScannedData(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
            //Все объекты внутри проинициализированны
            SizeData sizeFactory() => new SizeData(
                Folders.Sum(f => f.Size.SizeInBytes)
                + Files.Sum(f => f.Size.SizeInBytes));
            size = new Lazy<SizeData>(sizeFactory);
            long totalFoldersCountFactory() => folders.Sum(f => f.TotalFoldersCount) + FoldersCount;
            long totalFilesCountFactory() => folders.Sum(f => f.TotalFilesCount) + FilesCount;
            totalFoldersCount = new Lazy<long>(totalFoldersCountFactory);
            totalFilesCount = new Lazy<long>(totalFilesCountFactory);
        }

        public FolderScannedData(string directoryPath) : this(new DirectoryInfo(directoryPath)) { }

        public void Inspect()
        {
            if (isInspected)
                return;
            foreach (var directory in directoryInfo.EnumerateDirectories().Filter(!UserAccess.IsCurrentUserAdmin))
            {
                folders.Add(new FolderScannedData(directory));
            }
            foreach (var file in directoryInfo.EnumerateFiles().Filter(!UserAccess.IsCurrentUserAdmin))
            {
                files.Add(new FileScannedData(file));
            }
            //directoryInfo.EnumerateDirectories().AsParallel().AsQueryable().ForAll(d => folders.Add(new FolderScannedData(d)));
            //directoryInfo.EnumerateFiles().AsParallel().AsQueryable().ForAll(f => files.Add(new FileScannedData(f)));
            isInspected = true;
        }

        public void InspectAll()
        {
            Inspect();
            foreach (var f in Folders)
                f.InspectAll();
        }

        public IEnumerable<FolderScannedData> EnumerateAllFolders()
        {
            //сначала печатает 1ый уровень папок, затем вложенные
            foreach (var firstLayerFolder in folders)
                yield return firstLayerFolder;
            foreach (var firstLayerFolder in folders)
            {
                foreach (var SecondLayerFolder in firstLayerFolder.EnumerateAllFolders())
                    yield return SecondLayerFolder;
            }
        }
    }
}
