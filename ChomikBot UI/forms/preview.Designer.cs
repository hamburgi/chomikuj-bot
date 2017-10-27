namespace ChomikBot_UI {
    partial class preview {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(preview));
            this.previewBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // previewBrowser
            // 
            this.previewBrowser.Location = new System.Drawing.Point(-1, 64);
            this.previewBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.previewBrowser.Name = "previewBrowser";
            this.previewBrowser.Size = new System.Drawing.Size(483, 340);
            this.previewBrowser.TabIndex = 0;
            // 
            // preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 403);
            this.Controls.Add(this.previewBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "preview";
            this.Sizable = false;
            this.Text = "PREVIEW";
            this.Load += new System.EventHandler(this.preview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser previewBrowser;
    }
}