namespace RomanPort.UltimateSDRRecorder
{
    partial class MainControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.authorCredit = new System.Windows.Forms.Label();
            this.sdrDvr = new RomanPort.UltimateSDRRecorder.DVR.Interface.SdrDvrInterface();
            this.basebandRecorder = new RomanPort.UltimateSDRRecorder.Framework.Ui.RecorderControl();
            this.audioRecorder = new RomanPort.UltimateSDRRecorder.Framework.Ui.RecorderControl();
            this.SuspendLayout();
            // 
            // authorCredit
            // 
            this.authorCredit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorCredit.Location = new System.Drawing.Point(3, 267);
            this.authorCredit.Name = "authorCredit";
            this.authorCredit.Size = new System.Drawing.Size(212, 33);
            this.authorCredit.TabIndex = 3;
            this.authorCredit.Text = "UltimateSdrRecorder by RomanPort";
            this.authorCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sdrDvr
            // 
            this.sdrDvr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sdrDvr.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sdrDvr.Location = new System.Drawing.Point(3, 197);
            this.sdrDvr.Name = "sdrDvr";
            this.sdrDvr.Size = new System.Drawing.Size(212, 48);
            this.sdrDvr.TabIndex = 2;
            // 
            // basebandRecorder
            // 
            this.basebandRecorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basebandRecorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.basebandRecorder.Location = new System.Drawing.Point(3, 100);
            this.basebandRecorder.Name = "basebandRecorder";
            this.basebandRecorder.Size = new System.Drawing.Size(212, 100);
            this.basebandRecorder.TabIndex = 1;
            // 
            // audioRecorder
            // 
            this.audioRecorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.audioRecorder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.audioRecorder.Location = new System.Drawing.Point(3, 3);
            this.audioRecorder.Name = "audioRecorder";
            this.audioRecorder.Size = new System.Drawing.Size(212, 100);
            this.audioRecorder.TabIndex = 0;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.authorCredit);
            this.Controls.Add(this.sdrDvr);
            this.Controls.Add(this.basebandRecorder);
            this.Controls.Add(this.audioRecorder);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(218, 304);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Ui.RecorderControl audioRecorder;
        private Framework.Ui.RecorderControl basebandRecorder;
        private DVR.Interface.SdrDvrInterface sdrDvr;
        private System.Windows.Forms.Label authorCredit;
    }
}
