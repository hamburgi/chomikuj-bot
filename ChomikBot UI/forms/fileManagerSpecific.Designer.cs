namespace ChomikBot_.forms {
    partial class fileManagerSpecific {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fileManagerSpecific));
            this.filePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uploadBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // filePanel
            // 
            this.filePanel.AutoScroll = true;
            this.filePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.filePanel.Location = new System.Drawing.Point(0, 63);
            this.filePanel.Name = "filePanel";
            this.filePanel.Size = new System.Drawing.Size(727, 415);
            this.filePanel.TabIndex = 0;
            // 
            // uploadBtn
            // 
            this.uploadBtn.Depth = 0;
            this.uploadBtn.Location = new System.Drawing.Point(680, 29);
            this.uploadBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Primary = true;
            this.uploadBtn.Size = new System.Drawing.Size(35, 28);
            this.uploadBtn.TabIndex = 3;
            this.uploadBtn.Text = "+";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // fileManagerSpecific
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 478);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.filePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fileManagerSpecific";
            this.Sizable = false;
            this.Text = "FILE MANAGER";
            this.Load += new System.EventHandler(this.fileManagerSpecific_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel filePanel;
        private MaterialSkin.Controls.MaterialRaisedButton uploadBtn;
    }
}