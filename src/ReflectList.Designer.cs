﻿namespace Projector
{
    partial class ReflectList
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReflectList));
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.fileLoader = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.autoSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Progress = new System.Windows.Forms.ToolStripProgressBar();
            this.stateLabel = new System.Windows.Forms.ToolStripLabel();
            this.listView = new System.Windows.Forms.ListView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFilesDlg = new System.Windows.Forms.OpenFileDialog();
            this.listItemsPic = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Name = "splitter";
            this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.toolStrip);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.listView);
            this.splitter.Size = new System.Drawing.Size(403, 401);
            this.splitter.SplitterDistance = 32;
            this.splitter.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.fileLoader,
            this.toolStripSeparator1,
            this.autoSort,
            this.toolStripSeparator2,
            this.Progress,
            this.stateLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(403, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::Projector.Properties.Resources.SAVE_16;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Save Result";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // fileLoader
            // 
            this.fileLoader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileLoader.Image = global::Projector.Properties.Resources.folder_open_16;
            this.fileLoader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileLoader.Name = "fileLoader";
            this.fileLoader.Size = new System.Drawing.Size(23, 22);
            this.fileLoader.Text = "toolStripButton1";
            this.fileLoader.Click += new System.EventHandler(this.fileLoader_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // autoSort
            // 
            this.autoSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.autoSort.Image = global::Projector.Properties.Resources.resizecol;
            this.autoSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoSort.Name = "autoSort";
            this.autoSort.Size = new System.Drawing.Size(23, 22);
            this.autoSort.Text = "Arange Columns";
            this.autoSort.Click += new System.EventHandler(this.autoSort_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Progress
            // 
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(100, 22);
            // 
            // stateLabel
            // 
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(58, 22);
            this.stateLabel.Text = "No Name";
            // 
            // listView
            // 
            this.listView.AllowColumnReorder = true;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.listItemsPic;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(403, 365);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "export.csv";
            this.saveFileDialog.Filter = "CSV|*.csv";
            // 
            // openFilesDlg
            // 
            this.openFilesDlg.DefaultExt = "csv";
            this.openFilesDlg.Filter = "csv Text|*.csv";
            this.openFilesDlg.Title = "Open Data Content";
            // 
            // listItemsPic
            // 
            this.listItemsPic.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listItemsPic.ImageStream")));
            this.listItemsPic.TransparentColor = System.Drawing.Color.Transparent;
            this.listItemsPic.Images.SetKeyName(0, "application2.png");
            // 
            // ReflectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Name = "ReflectList";
            this.Size = new System.Drawing.Size(403, 401);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel1.PerformLayout();
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton autoSort;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel stateLabel;
        private System.Windows.Forms.ToolStripProgressBar Progress;
        private System.Windows.Forms.ToolStripButton fileLoader;
        private System.Windows.Forms.OpenFileDialog openFilesDlg;
        private System.Windows.Forms.ImageList listItemsPic;
    }
}
