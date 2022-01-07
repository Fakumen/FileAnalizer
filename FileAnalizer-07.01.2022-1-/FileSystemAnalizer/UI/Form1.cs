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
using FileSystemAnalizer.App;

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
                if (result == DialogResult.OK)
                {
                    var scanner = new FolderScanner() as Scanner<string, FolderScanData>;
                    FileHierarchyTree.ImageList = IconPool.GetImageList();
                    var hierarchy = new FileSystemScanDataTree(FileHierarchyTree);
                    hierarchy.Clear();
                    hierarchy.AddNode(new FolderDataNode(scanner.TryScan(dialog.SelectedPath)));
                }
            }
        }

        private void FileHierarchyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
