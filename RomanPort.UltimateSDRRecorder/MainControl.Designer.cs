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
            this.appSettingsBtn = new System.Windows.Forms.Button();
            this.updateMsgBtn = new System.Windows.Forms.Button();
            this.updateMsgText = new System.Windows.Forms.Label();
            this.sdrDvr = new RomanPort.UltimateSDRRecorder.DVR.Interface.SdrDvrInterface();
            this.basebandRecorder = new RomanPort.UltimateSDRRecorder.Framework.Ui.RecorderControl();
            this.audioRecorder = new RomanPort.UltimateSDRRecorder.Framework.Ui.RecorderControl();
            this.SuspendLayout();
            // 
            // appSettingsBtn
            // 
            this.appSettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appSettingsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.appSettingsBtn.Location = new System.Drawing.Point(6, 272);
            this.appSettingsBtn.Name = "appSettingsBtn";
            this.appSettingsBtn.Size = new System.Drawing.Size(207, 23);
            this.appSettingsBtn.TabIndex = 4;
            this.appSettingsBtn.Text = "UltimateSdrRecorder by RomanPort";
            this.appSettingsBtn.UseVisualStyleBackColor = true;
            this.appSettingsBtn.Click += new System.EventHandler(this.appSettingsBtn_Click);
            // 
            // updateMsgBtn
            // 
            this.updateMsgBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateMsgBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateMsgBtn.Location = new System.Drawing.Point(170, 272);
            this.updateMsgBtn.Name = "updateMsgBtn";
            this.updateMsgBtn.Size = new System.Drawing.Size(43, 23);
            this.updateMsgBtn.TabIndex = 5;
            this.updateMsgBtn.Text = "Info";
            this.updateMsgBtn.UseVisualStyleBackColor = true;
            this.updateMsgBtn.Click += new System.EventHandler(this.updateMsgBtn_Click);
            // 
            // updateMsgText
            // 
            this.updateMsgText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateMsgText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.updateMsgText.Location = new System.Drawing.Point(5, 272);
            this.updateMsgText.Name = "updateMsgText";
            this.updateMsgText.Size = new System.Drawing.Size(161, 23);
            this.updateMsgText.TabIndex = 6;
            this.updateMsgText.Text = "New plugin update available";
            this.updateMsgText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Controls.Add(this.updateMsgText);
            this.Controls.Add(this.updateMsgBtn);
            this.Controls.Add(this.appSettingsBtn);
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
        private System.Windows.Forms.Button appSettingsBtn;
        private System.Windows.Forms.Button updateMsgBtn;
        private System.Windows.Forms.Label updateMsgText;
    }
}
