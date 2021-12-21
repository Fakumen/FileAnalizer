using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public interface IScannedData
    {
        [DisplayedName("Путь")]
        string Path { get; }
        [DisplayedName("Название")]
        string Name { get; }
        [DisplayedName("Размер")]
        long Weight { get; }
        [DisplayedName("Созданно")]
        DateTime CreationTime { get; }
        [DisplayedName("Последнее открытие")]
        DateTime LastAccessTime { get; }
        [DisplayedName("Изменено")]
        DateTime LastWriteTime { get; }
        //bool IsDirectory { get; }
    }
}
