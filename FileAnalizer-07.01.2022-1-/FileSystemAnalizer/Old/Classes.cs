using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FileAnalizer
{
    //public class ExtensionData
    //{
    //    public string Name;
    //    public string Description;
    //    public Icon Icon;
    //}

    public class FileTypeStatistics
    {
        public long Count;
        public long TotalWeight;
        public string ExtensionName;
        public Icon ExtensionIcon;
        public float RelativePercentWeight;
    }

    public interface ICategory
    {
        bool IsFileRelatedToCategory(IAnalizedFileSystemEntity analizedInfo);
        //bool IsFileRelatedToCategory(string filePath);
    }

    public interface IAnalizedFileSystemEntity//Заменить на АБК? //Заменен на IScannedData
    {
        string Path { get; }
        bool IsDirectory { get; }
        long Weight { get; }
        event Action<IAnalizedFileSystemEntity> AnalizedEntityCreated;
        FileSystemInfo fileSystemInfo { get; }
    }

    public class ScannedEntityData
    {
        public string Path => fileSystemInfo.FullName;
        public long? Weight = null;
        public long? TotalFilesCount = null;
        public long? TotalDirectoriesCount = null;
        public long? TotalElementsCount => TotalFilesCount + TotalDirectoriesCount;
        public readonly FileSystemInfo fileSystemInfo;
        public readonly bool isDirectory;
        private readonly List<ScannedEntityData> children = new List<ScannedEntityData>();

        private ScannedEntityData(FileSystemInfo fileSystemInfo)
        {
            this.fileSystemInfo = fileSystemInfo;
            isDirectory = fileSystemInfo is DirectoryInfo;

        }

        private IEnumerable<DirectoryInfo> GetDirectories()
        {
            if (!isDirectory)
                return Enumerable.Empty<DirectoryInfo>();
            return ((DirectoryInfo)fileSystemInfo)
                .EnumerateDirectories("*", SearchOption.TopDirectoryOnly)
                .Where(d => !d.Attributes.HasFlag(FileAttributes.System));
        }

        public static IEnumerable<ScannedEntityData> Scan(DirectoryInfo root)
        {
            var entityData = new ScannedEntityData(root);
            yield return entityData;//сначала выдается инфа о папке, из которой начался поиск
            foreach (var childDirectoryData in entityData.GetDirectories().Select(d => new ScannedEntityData(d)))//идем по информации о подпапках
            {
                entityData.children.Add(childDirectoryData);//добавляем папку в дочернюю к корневой
                //return Scan((DirectoryInfo)childData.fileSystemInfo);
                foreach (var _ in Scan((DirectoryInfo)childDirectoryData.fileSystemInfo))//начинаем скан из дочерней папки
                    yield return _;
            }
            foreach (var childFileData in entityData.GetDirectories().Select(d => new ScannedEntityData(d)))//идем по информации о подпапках
            {
                entityData.children.Add(childFileData);//добавляем папку в дочернюю к корневой
                //return Scan((DirectoryInfo)childData.fileSystemInfo);
                foreach (var _ in Scan((DirectoryInfo)childFileData.fileSystemInfo))//начинаем скан из дочерней папки
                    yield return _;
            }
            //Program.Print("Вернулись на уровень вверх");
        }

        //public static IEnumerable<ScannedEntityData> Scan(DirectoryInfo root)
        //{
        //    var entityData = new ScannedEntityData(root);
        //    yield return entityData;
        //    foreach (var childData in entityData.GetDirectories().Select(d => new ScannedEntityData(d)))
        //        entityData.children.Add(childData);
        //    foreach (var e in entityData.children.Select(c => Scan((DirectoryInfo)c.fileSystemInfo)))
        //        yield return e;
        //}
    }
}
