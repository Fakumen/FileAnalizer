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
    public class ScannedDataTreeNode<TScanData> : TreeNode, IScanDataTreeNode<TScanData>, IScanDataTree<ScannedDataTreeNode<TScanData>>
        where TScanData : IScanData
    {
        private readonly bool isDirectory;
        public TScanData ScanData { get; }

        public ScannedDataTreeNode(TScanData scannedData) : base()
        {
            this.ScanData = scannedData;
            var sizeUnits = scannedData.Size.BestFittingUnits;
            Text = $"{scannedData.Name} [{scannedData.Size.GetInUnits(sizeUnits):f1} {sizeUnits.ToString()}]";
            if (scannedData is FolderScannedData)
            {
                var folderData = scannedData as FolderScannedData;
                foreach (var folder in folderData.Folders)
                {
                    Nodes.Add(ScannedDataTreeNode.Create(folder));
                }
                foreach (var file in folderData.Files)
                {
                    Nodes.Add(ScannedDataTreeNode.Create(file));
                }
                SelectedImageKey = ImageKey = IconPool.FolderIconKey;
            }
            else if (scannedData is FileScannedData)
            {
                var fileData = scannedData as FileScannedData;
                SelectedImageKey = ImageKey = IconPool.FileIconKey;
            }
        }

        public void AddNode(ScannedDataTreeNode<TScanData> node) => Nodes.Add(new ScannedDataTreeNode<TScanData>(node))
        {
            throw new NotImplementedException();
        }

        public void Clear() => Nodes.Clear();
    }

    public static class ScannedDataTreeNode
    {
        public static ScannedDataTreeNode<TData> Create<TData>(TData data)
            where TData : IScanData
            => new ScannedDataTreeNode<TData>(data);
    }
}
