using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public sealed class FolderScannedData : IScannedData
    {
        public readonly long TotalFilesCount;
        public readonly long TotalFoldersCount;
        public long Weight => weight;
        public string Path => directoryInfo.FullName;
        public string Name => directoryInfo.Name;
        public DateTime CreationTime => directoryInfo.CreationTime;
        public DateTime LastAccessTime => directoryInfo.LastAccessTime;
        public DateTime LastWriteTime => directoryInfo.LastWriteTime;
        public long FoldersCount => folders.Count();
        public long FilesCount => files.Count();
        public long TotalElementsCount => TotalFilesCount + TotalFoldersCount;
        public IEnumerable<FolderScannedData> Folders => folders.Select(f => f);//Нельзя сделать даункаст к листу и изменить
        public IEnumerable<FileScannedData> Files => files.Select(f => f);

        private readonly long weight;//Размер (не "На диске")
        private readonly List<FolderScannedData> folders = new List<FolderScannedData>();
        private readonly List<FileScannedData> files = new List<FileScannedData>();
        private readonly DirectoryInfo directoryInfo;

        private FolderScannedData(
            DirectoryInfo directoryInfo,
            Func<DirectoryInfo, bool> directorySelector,
            Func<FileInfo, bool> fileSelector)
        {
            this.directoryInfo = directoryInfo;
            //directoryInfo.EnumerateDirectories().AsParallel().ForAll(d => folders.Add(new FolderScannedData(d)));
            foreach (var directory in directoryInfo
                .EnumerateDirectories()
                .Where(directorySelector)
                .AsParallel()
                .AsSequential())
            {
                folders.Add(new FolderScannedData(directory, directorySelector, fileSelector));
            }
            //foreach (var directory in directoryInfo.EnumerateDirectories())
            //{
            //    folders.Add(new FolderScannedData(directory));
            //}
            foreach (var file in directoryInfo.EnumerateFiles().Where(fileSelector))
            {
                files.Add(new FileScannedData(file));
            }
            //Все объекты внутри проинициализированны
            weight = folders.Sum(f => f.Weight) + files.Sum(f => f.Weight);
            TotalFoldersCount = folders.Sum(f => f.TotalFoldersCount) + FoldersCount;
            TotalFilesCount = folders.Sum(f => f.TotalFilesCount) + FilesCount;
        }

        public static FolderScannedData ScanFolder(
            string path,
            Func<DirectoryInfo, bool> directorySelector = null,
            Func<FileInfo, bool> fileSelector = null)
        {
            if (directorySelector == null)
                directorySelector = directory => true;
            if (fileSelector == null)
                fileSelector = file => true;
            return new FolderScannedData(new DirectoryInfo(path), directorySelector, fileSelector);
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
