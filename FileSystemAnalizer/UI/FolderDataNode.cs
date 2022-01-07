using FileSystemAnalizer.App;
using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAnalizer.UI
{
    public class FolderDataNode : TreeNode, IFolderDataNode<FolderDataNode, FileDataNode>
    {
        public IFolderScanData ScanData { get; }

        public FolderDataNode(IFolderScanData folderData)
        {
            ScanData = folderData;
            var sizeUnits = ScanData.Size.BestFittingUnits;
            Text = $"{ScanData.Name} [{ScanData.Size.GetInUnits(sizeUnits):f1} {sizeUnits.ToString()}]";
            SelectedImageKey = ImageKey = IconPool.FolderIconKey;
            foreach (var folder in ScanData.Folders)
            {
                AddNode(new FolderDataNode(folder));
            }
            foreach (var file in ScanData.Files)
            {
                AddNode(new FileDataNode(file));
            }
        }

        public void AddNode(FileDataNode fileNode)
        {
            Nodes.Add(fileNode);
        }

        public void AddNode(FolderDataNode folderNode)
        {
            Nodes.Add(folderNode);
        }
    }

    public class FileDataNode : TreeNode, IFileDataNode
    {
        public IFileScanData ScanData { get; }

        public FileDataNode(IFileScanData fileData)
        {
            ScanData = fileData;
            var sizeUnits = ScanData.Size.BestFittingUnits;
            Text = $"{ScanData.Name} [{ScanData.Size.GetInUnits(sizeUnits):f1} {sizeUnits.ToString()}]";
            SelectedImageKey = ImageKey = IconPool.FileIconKey;
        }
    }
}
