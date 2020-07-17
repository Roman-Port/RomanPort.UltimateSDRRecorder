namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    partial class SdrDvrActiveRecordingStatusPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopProgramBtn = new System.Windows.Forms.Button();
            this.toggleStatusProgramBtn = new System.Windows.Forms.Button();
            this.deleteProgramBtn = new System.Windows.Forms.Button();
            this.programList = new System.Windows.Forms.ListView();
            this.programName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.triggerType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.outputType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.retuneFreq = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.recordingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.recordingSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.createProgramBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.stopProgramBtn);
            this.groupBox1.Controls.Add(this.toggleStatusProgramBtn);
            this.groupBox1.Controls.Add(this.deleteProgramBtn);
            this.groupBox1.Controls.Add(this.programList);
            this.groupBox1.Controls.Add(this.createProgramBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 287);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Programs";
            // 
            // stopProgramBtn
            // 
            this.stopProgramBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopProgramBtn.Enabled = false;
            this.stopProgramBtn.Location = new System.Drawing.Point(394, 258);
            this.stopProgramBtn.Name = "stopProgramBtn";
            this.stopProgramBtn.Size = new System.Drawing.Size(75, 23);
            this.stopProgramBtn.TabIndex = 5;
            this.stopProgramBtn.Text = "Stop";
            this.stopProgramBtn.UseVisualStyleBackColor = true;
            this.stopProgramBtn.Click += new System.EventHandler(this.stopProgramBtn_Click);
            // 
            // toggleStatusProgramBtn
            // 
            this.toggleStatusProgramBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleStatusProgramBtn.Enabled = false;
            this.toggleStatusProgramBtn.Location = new System.Drawing.Point(475, 258);
            this.toggleStatusProgramBtn.Name = "toggleStatusProgramBtn";
            this.toggleStatusProgramBtn.Size = new System.Drawing.Size(75, 23);
            this.toggleStatusProgramBtn.TabIndex = 4;
            this.toggleStatusProgramBtn.Text = "Disable";
            this.toggleStatusProgramBtn.UseVisualStyleBackColor = true;
            this.toggleStatusProgramBtn.Click += new System.EventHandler(this.disableProgramBtn_Click);
            // 
            // deleteProgramBtn
            // 
            this.deleteProgramBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteProgramBtn.Enabled = false;
            this.deleteProgramBtn.Location = new System.Drawing.Point(556, 258);
            this.deleteProgramBtn.Name = "deleteProgramBtn";
            this.deleteProgramBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteProgramBtn.TabIndex = 3;
            this.deleteProgramBtn.Text = "Delete";
            this.deleteProgramBtn.UseVisualStyleBackColor = true;
            this.deleteProgramBtn.Click += new System.EventHandler(this.deleteProgramBtn_Click);
            // 
            // programList
            // 
            this.programList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.programList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.programName,
            this.triggerType,
            this.outputType,
            this.retuneFreq,
            this.status,
            this.recordingTime,
            this.recordingSize});
            this.programList.FullRowSelect = true;
            this.programList.GridLines = true;
            this.programList.HideSelection = false;
            this.programList.Location = new System.Drawing.Point(6, 19);
            this.programList.MultiSelect = false;
            this.programList.Name = "programList";
            this.programList.Size = new System.Drawing.Size(625, 233);
            this.programList.TabIndex = 2;
            this.programList.UseCompatibleStateImageBehavior = false;
            this.programList.View = System.Windows.Forms.View.Details;
            this.programList.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // programName
            // 
            this.programName.Text = "Program Name";
            this.programName.Width = 119;
            // 
            // triggerType
            // 
            this.triggerType.Text = "Trigger";
            this.triggerType.Width = 93;
            // 
            // outputType
            // 
            this.outputType.Text = "Type";
            this.outputType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.outputType.Width = 38;
            // 
            // retuneFreq
            // 
            this.retuneFreq.Text = "Retune";
            this.retuneFreq.Width = 86;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 84;
            // 
            // recordingTime
            // 
            this.recordingTime.Text = "Recording Time";
            this.recordingTime.Width = 98;
            // 
            // recordingSize
            // 
            this.recordingSize.Text = "Recording Size";
            this.recordingSize.Width = 101;
            // 
            // createProgramBtn
            // 
            this.createProgramBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createProgramBtn.Location = new System.Drawing.Point(6, 258);
            this.createProgramBtn.Name = "createProgramBtn";
            this.createProgramBtn.Size = new System.Drawing.Size(123, 23);
            this.createProgramBtn.TabIndex = 1;
            this.createProgramBtn.Text = "Create Program...";
            this.createProgramBtn.UseVisualStyleBackColor = true;
            this.createProgramBtn.Click += new System.EventHandler(this.createProgramBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(574, 306);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // SdrDvrActiveRecordingStatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 341);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SdrDvrActiveRecordingStatusPanel";
            this.Text = "DVR Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SdrDvrActiveRecordingStatusPanel_FormClosing);
            this.Load += new System.EventHandler(this.SdrDvrActiveRecordingStatusPanel_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createProgramBtn;
        private System.Windows.Forms.ListView programList;
        private System.Windows.Forms.ColumnHeader programName;
        private System.Windows.Forms.ColumnHeader triggerType;
        private System.Windows.Forms.ColumnHeader outputType;
        private System.Windows.Forms.ColumnHeader retuneFreq;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader recordingTime;
        private System.Windows.Forms.Button stopProgramBtn;
        private System.Windows.Forms.Button toggleStatusProgramBtn;
        private System.Windows.Forms.Button deleteProgramBtn;
        private System.Windows.Forms.ColumnHeader recordingSize;
        private System.Windows.Forms.Button saveBtn;
    }
}