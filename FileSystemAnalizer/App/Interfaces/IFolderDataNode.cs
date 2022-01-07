using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.App
{
    public interface IFolderDataNode<TFolderData, TFileData> : IDataNode<IFolderScanData>
        where TFolderData : IFolderDataNode<TFolderData, TFileData>
        where TFileData : IFileDataNode
    {
        void AddNode(TFolderData folderNode);
        void AddNode(TFileData fileNode);
    }
}
