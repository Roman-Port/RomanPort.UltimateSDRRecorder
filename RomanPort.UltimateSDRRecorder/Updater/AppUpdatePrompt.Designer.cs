namespace RomanPort.UltimateSDRRecorder.Updater
{
    partial class AppUpdatePrompt
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
            this.updateBtn = new System.Windows.Forms.Button();
            this.ignoreBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.updateText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateBtn
            // 
            this.updateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateBtn.Location = new System.Drawing.Point(174, 313);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(125, 23);
            this.updateBtn.TabIndex = 0;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // ignoreBtn
            // 
            this.ignoreBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ignoreBtn.Location = new System.Drawing.Point(93, 313);
            this.ignoreBtn.Name = "ignoreBtn";
            this.ignoreBtn.Size = new System.Drawing.Size(75, 23);
            this.ignoreBtn.TabIndex = 1;
            this.ignoreBtn.Text = "Skip";
            this.ignoreBtn.UseVisualStyleBackColor = true;
            this.ignoreBtn.Click += new System.EventHandler(this.ignoreBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelBtn.Location = new System.Drawing.Point(12, 313);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // updateText
            // 
            this.updateText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateText.Location = new System.Drawing.Point(12, 13);
            this.updateText.Name = "updateText";
            this.updateText.Size = new System.Drawing.Size(287, 297);
            this.updateText.TabIndex = 3;
            this.updateText.Text = "Update Text";
            // 
            // AppUpdatePrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 348);
            this.Controls.Add(this.updateText);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ignoreBtn);
            this.Controls.Add(this.updateBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppUpdatePrompt";
            this.Text = "Ultimate SDR Recorder Update";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.Button ignoreBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label updateText;
    }
}