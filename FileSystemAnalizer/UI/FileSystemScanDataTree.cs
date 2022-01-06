using FileSystemAnalizer.App;
using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystemAnalizer.UI
{
    public class FileSystemScanDataTree : IScanDataTree<ScannedDataTreeNode<FolderScannedData>>
    {
        private readonly TreeView treeView;

        public FileSystemScanDataTree(TreeView treeView)
        {
            this.treeView = treeView;
        }

        public void AddNode(ScannedDataTreeNode<FolderScannedData> node)
        {
            treeView.Nodes.Add(node);
        }

        public void Clear()
        {
            treeView.Nodes.Clear();
        }
    }
}
