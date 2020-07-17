namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    partial class RecorderControl
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
            this.recorderContainer = new System.Windows.Forms.GroupBox();
            this.statusError = new System.Windows.Forms.Label();
            this.labelRecord = new System.Windows.Forms.Label();
            this.infoRecord = new System.Windows.Forms.Label();
            this.labelBuffer = new System.Windows.Forms.Label();
            this.infoBuffer = new System.Windows.Forms.Label();
            this.stopRecordingBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.saveBufferBtn = new System.Windows.Forms.Button();
            this.saveBufferContinueBtn = new System.Windows.Forms.Button();
            this.recorderContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // recorderContainer
            // 
            this.recorderContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recorderContainer.Controls.Add(this.statusError);
            this.recorderContainer.Controls.Add(this.labelRecord);
            this.recorderContainer.Controls.Add(this.infoRecord);
            this.recorderContainer.Controls.Add(this.labelBuffer);
            this.recorderContainer.Controls.Add(this.infoBuffer);
            this.recorderContainer.Controls.Add(this.stopRecordingBtn);
            this.recorderContainer.Controls.Add(this.settingsBtn);
            this.recorderContainer.Controls.Add(this.startBtn);
            this.recorderContainer.Controls.Add(this.saveBufferBtn);
            this.recorderContainer.Controls.Add(this.saveBufferContinueBtn);
            this.recorderContainer.ForeColor = System.Drawing.SystemColors.Control;
            this.recorderContainer.Location = new System.Drawing.Point(3, 3);
            this.recorderContainer.Name = "recorderContainer";
            this.recorderContainer.Size = new System.Drawing.Size(215, 99);
            this.recorderContainer.TabIndex = 0;
            this.recorderContainer.TabStop = false;
            this.recorderContainer.Text = "groupBox1";
            // 
            // statusError
            // 
            this.statusError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusError.BackColor = System.Drawing.Color.Transparent;
            this.statusError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.statusError.Location = new System.Drawing.Point(6, 61);
            this.statusError.Name = "statusError";
            this.statusError.Size = new System.Drawing.Size(149, 38);
            this.statusError.TabIndex = 10;
            this.statusError.Text = "ERROR";
            this.statusError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusError.Visible = false;
            // 
            // labelRecord
            // 
            this.labelRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecord.Location = new System.Drawing.Point(6, 32);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Size = new System.Drawing.Size(60, 23);
            this.labelRecord.TabIndex = 9;
            this.labelRecord.Text = "RECORD";
            this.labelRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoRecord
            // 
            this.infoRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoRecord.Location = new System.Drawing.Point(67, 32);
            this.infoRecord.Name = "infoRecord";
            this.infoRecord.Size = new System.Drawing.Size(143, 23);
            this.infoRecord.TabIndex = 8;
            this.infoRecord.Text = "00:00:00 - 0 MB";
            this.infoRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBuffer
            // 
            this.labelBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBuffer.Location = new System.Drawing.Point(6, 12);
            this.labelBuffer.Name = "labelBuffer";
            this.labelBuffer.Size = new System.Drawing.Size(60, 23);
            this.labelBuffer.TabIndex = 7;
            this.labelBuffer.Text = "BUFFER";
            this.labelBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoBuffer
            // 
            this.infoBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBuffer.Location = new System.Drawing.Point(67, 12);
            this.infoBuffer.Name = "infoBuffer";
            this.infoBuffer.Size = new System.Drawing.Size(120, 23);
            this.infoBuffer.TabIndex = 5;
            this.infoBuffer.Text = "00:00:00 - 0 MB";
            this.infoBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stopRecordingBtn
            // 
            this.stopRecordingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopRecordingBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stopRecordingBtn.Location = new System.Drawing.Point(158, 70);
            this.stopRecordingBtn.Name = "stopRecordingBtn";
            this.stopRecordingBtn.Size = new System.Drawing.Size(52, 23);
            this.stopRecordingBtn.TabIndex = 4;
            this.stopRecordingBtn.Text = "Stop";
            this.stopRecordingBtn.UseVisualStyleBackColor = true;
            this.stopRecordingBtn.Visible = false;
            this.stopRecordingBtn.Click += new System.EventHandler(this.stopRecordingBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.settingsBtn.Location = new System.Drawing.Point(187, 12);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(23, 23);
            this.settingsBtn.TabIndex = 3;
            this.settingsBtn.Text = "⚙";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.startBtn.Location = new System.Drawing.Point(157, 70);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(52, 23);
            this.startBtn.TabIndex = 2;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // saveBufferBtn
            // 
            this.saveBufferBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBufferBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveBufferBtn.Location = new System.Drawing.Point(6, 70);
            this.saveBufferBtn.Name = "saveBufferBtn";
            this.saveBufferBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBufferBtn.TabIndex = 1;
            this.saveBufferBtn.Text = "Save Buffer";
            this.saveBufferBtn.UseVisualStyleBackColor = true;
            this.saveBufferBtn.Click += new System.EventHandler(this.saveBufferBtn_Click);
            // 
            // saveBufferContinueBtn
            // 
            this.saveBufferContinueBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBufferContinueBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveBufferContinueBtn.Location = new System.Drawing.Point(80, 70);
            this.saveBufferContinueBtn.Name = "saveBufferContinueBtn";
            this.saveBufferContinueBtn.Size = new System.Drawing.Size(61, 23);
            this.saveBufferContinueBtn.TabIndex = 0;
            this.saveBufferContinueBtn.Text = "+Record";
            this.saveBufferContinueBtn.UseVisualStyleBackColor = true;
            this.saveBufferContinueBtn.Click += new System.EventHandler(this.saveBufferContinueBtn_Click);
            // 
            // RecorderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.recorderContainer);
            this.Name = "RecorderControl";
            this.Size = new System.Drawing.Size(221, 105);
            this.recorderContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox recorderContainer;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button saveBufferBtn;
        private System.Windows.Forms.Button saveBufferContinueBtn;
        private System.Windows.Forms.Button stopRecordingBtn;
        private System.Windows.Forms.Label labelRecord;
        private System.Windows.Forms.Label infoRecord;
        private System.Windows.Forms.Label labelBuffer;
        private System.Windows.Forms.Label infoBuffer;
        private System.Windows.Forms.Label statusError;
    }
}
