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
    public class ScannedDataTreeNode<TData> : TreeNode
        where TData : IScannedData
    {
        private readonly bool isDirectory;
        private readonly TData scannedData;

        public ScannedDataTreeNode(TData scannedData) : base()
        {
            this.scannedData = scannedData;
            Text = scannedData.Name;
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
                var iconKey = fileData.Extension;
                if (!IconPool.Contains(iconKey))
                {
                    using (var icon = Icon.ExtractAssociatedIcon(scannedData.Path))
                        IconPool.Add(iconKey, icon);
                }
                SelectedImageKey = ImageKey = iconKey;
            }
        }
    }

    public static class ScannedDataTreeNode
    {
        public static ScannedDataTreeNode<TData> Create<TData>(TData data)
            where TData : IScannedData
            => new ScannedDataTreeNode<TData>(data);
    }
}
