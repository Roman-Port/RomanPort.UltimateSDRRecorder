namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    partial class FileCopyProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.transferStatus = new System.Windows.Forms.ProgressBar();
            this.copyTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // transferStatus
            // 
            this.transferStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferStatus.Location = new System.Drawing.Point(12, 36);
            this.transferStatus.Name = "transferStatus";
            this.transferStatus.Size = new System.Drawing.Size(362, 23);
            this.transferStatus.TabIndex = 0;
            // 
            // copyTitle
            // 
            this.copyTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyTitle.Location = new System.Drawing.Point(13, 13);
            this.copyTitle.Name = "copyTitle";
            this.copyTitle.Size = new System.Drawing.Size(361, 20);
            this.copyTitle.TabIndex = 1;
            this.copyTitle.Text = "Preparing to copy...";
            // 
            // FileCopyProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 71);
            this.ControlBox = false;
            this.Controls.Add(this.copyTitle);
            this.Controls.Add(this.transferStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileCopyProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Copy";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar transferStatus;
        private System.Windows.Forms.Label copyTitle;
    }
}