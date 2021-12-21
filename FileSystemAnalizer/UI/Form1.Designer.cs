using System.Windows.Forms;
namespace FileSystemAnalizer.UI
{
    partial class FileAnalizerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileHierarchyTree = new System.Windows.Forms.TreeView();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FileHierarchyTree
            // 
            this.FileHierarchyTree.Location = new System.Drawing.Point(42, 52);
            this.FileHierarchyTree.Name = "FileHierarchyTree";
            this.FileHierarchyTree.Size = new System.Drawing.Size(335, 263);
            this.FileHierarchyTree.TabIndex = 0;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(42, 12);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(126, 23);
            this.SelectFolderButton.TabIndex = 1;
            this.SelectFolderButton.Text = "Выбрать каталог";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // FileSystemAnalizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.FileHierarchyTree);
            this.Name = "FileSystemAnalizerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TreeView FileHierarchyTree;
        private Button SelectFolderButton;
    }
}

