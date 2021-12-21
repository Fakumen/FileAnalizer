using FileSystemAnalizer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileSystemAnalizer.UI
{
    public partial class FileAnalizerForm : Form
    {
        public FileAnalizerForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FileHierarchyTree.AfterSelect += FileHierarchyTree_AfterSelect;
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            using(var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK && Directory.Exists(dialog.SelectedPath))
                {
                    var folderData = FolderScannedData.ScanFolder(dialog.SelectedPath);
                    FileHierarchyTree.Nodes.Clear();
                    FileHierarchyTree.ImageList = IconPool.GetImageList();
                    FileHierarchyTree.Nodes.Add(ScannedDataTreeNode.Create(folderData));
                }
            }
        }

        private void FileHierarchyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
