namespace ChomikBot_ {
    partial class fileManager {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fileManager));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextDirRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.renameDirContext = new System.Windows.Forms.ToolStripMenuItem();
            this.zmieńHasłoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.drzewko = new System.Windows.Forms.TreeView();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextDirRemove,
            this.renameDirContext,
            this.zmieńHasłoToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(153, 92);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // contextDirRemove
            // 
            this.contextDirRemove.Name = "contextDirRemove";
            this.contextDirRemove.Size = new System.Drawing.Size(152, 22);
            this.contextDirRemove.Text = "Usuń";
            this.contextDirRemove.Click += new System.EventHandler(this.contextDirRemove_Click);
            // 
            // renameDirContext
            // 
            this.renameDirContext.Name = "renameDirContext";
            this.renameDirContext.Size = new System.Drawing.Size(152, 22);
            this.renameDirContext.Text = "Zmień nazwę";
            this.renameDirContext.Click += new System.EventHandler(this.renameDirContext_Click);
            // 
            // zmieńHasłoToolStripMenuItem
            // 
            this.zmieńHasłoToolStripMenuItem.Name = "zmieńHasłoToolStripMenuItem";
            this.zmieńHasłoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zmieńHasłoToolStripMenuItem.Text = "Zmień hasło";
            this.zmieńHasłoToolStripMenuItem.Click += new System.EventHandler(this.changePassCOntext_Click);
            // 
            // uploadBtn
            // 
            this.uploadBtn.Depth = 0;
            this.uploadBtn.Location = new System.Drawing.Point(229, 28);
            this.uploadBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Primary = true;
            this.uploadBtn.Size = new System.Drawing.Size(35, 28);
            this.uploadBtn.TabIndex = 2;
            this.uploadBtn.Text = "+";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // drzewko
            // 
            this.drzewko.Location = new System.Drawing.Point(12, 72);
            this.drzewko.Name = "drzewko";
            this.drzewko.Size = new System.Drawing.Size(252, 274);
            this.drzewko.TabIndex = 0;
            this.drzewko.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.drzewko_MouseDoubleClick);
            this.drzewko.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drzewko_MouseUp);
            // 
            // fileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 358);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.drzewko);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fileManager";
            this.Sizable = false;
            this.Text = "FILE MANAGER";
            this.Load += new System.EventHandler(this.fileManager_Load);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem contextDirRemove;
        private System.Windows.Forms.ToolStripMenuItem renameDirContext;
        private MaterialSkin.Controls.MaterialRaisedButton uploadBtn;
        private System.Windows.Forms.ToolStripMenuItem zmieńHasłoToolStripMenuItem;
        private System.Windows.Forms.TreeView drzewko;
    }
}