namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    partial class SdrDvrProgramCreationPanel
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
            this.settingName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConfigureTrigger = new System.Windows.Forms.Button();
            this.triggerTime = new System.Windows.Forms.RadioButton();
            this.triggerRadioText = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.changeFreqDial = new System.Windows.Forms.NumericUpDown();
            this.changeFreqEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.outputBrowseBtn = new System.Windows.Forms.Button();
            this.outputFormatRecordAf = new System.Windows.Forms.RadioButton();
            this.outputFormatRecordIq = new System.Windows.Forms.RadioButton();
            this.saveBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.triggerPilot = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changeFreqDial)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.settingName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Settings";
            // 
            // settingName
            // 
            this.settingName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingName.Location = new System.Drawing.Point(10, 37);
            this.settingName.Name = "settingName";
            this.settingName.Size = new System.Drawing.Size(347, 20);
            this.settingName.TabIndex = 1;
            this.settingName.Text = "New Program";
            this.settingName.TextChanged += new System.EventHandler(this.settingName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Program Title";
            // 
            // outputPath
            // 
            this.outputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPath.Location = new System.Drawing.Point(10, 38);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(266, 20);
            this.outputPath.TabIndex = 3;
            this.outputPath.Text = "unnamed.wav";
            this.outputPath.TextChanged += new System.EventHandler(this.outputPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output File Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.triggerPilot);
            this.groupBox2.Controls.Add(this.btnConfigureTrigger);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.triggerTime);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.triggerRadioText);
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 207);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trigger";
            // 
            // btnConfigureTrigger
            // 
            this.btnConfigureTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigureTrigger.Location = new System.Drawing.Point(10, 174);
            this.btnConfigureTrigger.Name = "btnConfigureTrigger";
            this.btnConfigureTrigger.Size = new System.Drawing.Size(347, 23);
            this.btnConfigureTrigger.TabIndex = 4;
            this.btnConfigureTrigger.Text = "Configure Trigger Settings";
            this.btnConfigureTrigger.UseVisualStyleBackColor = true;
            this.btnConfigureTrigger.Click += new System.EventHandler(this.btnConfigureTrigger_Click);
            // 
            // triggerTime
            // 
            this.triggerTime.AutoSize = true;
            this.triggerTime.Location = new System.Drawing.Point(10, 68);
            this.triggerTime.Name = "triggerTime";
            this.triggerTime.Size = new System.Drawing.Size(84, 17);
            this.triggerTime.TabIndex = 2;
            this.triggerTime.Text = "Time Trigger";
            this.triggerTime.UseVisualStyleBackColor = true;
            this.triggerTime.CheckedChanged += new System.EventHandler(this.trigger_CheckedChanged);
            // 
            // triggerRadioText
            // 
            this.triggerRadioText.AutoSize = true;
            this.triggerRadioText.Checked = true;
            this.triggerRadioText.Location = new System.Drawing.Point(10, 20);
            this.triggerRadioText.Name = "triggerRadioText";
            this.triggerRadioText.Size = new System.Drawing.Size(139, 17);
            this.triggerRadioText.TabIndex = 0;
            this.triggerRadioText.TabStop = true;
            this.triggerRadioText.Text = "RDS Radio Text Trigger";
            this.triggerRadioText.UseVisualStyleBackColor = true;
            this.triggerRadioText.CheckedChanged += new System.EventHandler(this.trigger_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.changeFreqDial);
            this.groupBox3.Controls.Add(this.changeFreqEnabled);
            this.groupBox3.Location = new System.Drawing.Point(12, 299);
            this.groupBox3.MinimumSize = new System.Drawing.Size(363, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 119);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Change Frequency";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(10, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(347, 42);
            this.label6.TabIndex = 3;
            this.label6.Text = "When enabled, the DVR will automatically switch to this frequency and begin recor" +
    "ding when the trigger is activated. Be sure to keep this off when using RDS rela" +
    "ted triggers.";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(132, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "kHz";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // changeFreqDial
            // 
            this.changeFreqDial.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.changeFreqDial.Location = new System.Drawing.Point(10, 40);
            this.changeFreqDial.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.changeFreqDial.Name = "changeFreqDial";
            this.changeFreqDial.Size = new System.Drawing.Size(120, 20);
            this.changeFreqDial.TabIndex = 1;
            this.changeFreqDial.ThousandsSeparator = true;
            this.changeFreqDial.Value = new decimal(new int[] {
            104100,
            0,
            0,
            0});
            this.changeFreqDial.ValueChanged += new System.EventHandler(this.changeFreqDial_ValueChanged);
            // 
            // changeFreqEnabled
            // 
            this.changeFreqEnabled.AutoSize = true;
            this.changeFreqEnabled.Location = new System.Drawing.Point(10, 20);
            this.changeFreqEnabled.Name = "changeFreqEnabled";
            this.changeFreqEnabled.Size = new System.Drawing.Size(65, 17);
            this.changeFreqEnabled.TabIndex = 0;
            this.changeFreqEnabled.Text = "Enabled";
            this.changeFreqEnabled.UseVisualStyleBackColor = true;
            this.changeFreqEnabled.CheckedChanged += new System.EventHandler(this.changeFreqEnabled_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.outputBrowseBtn);
            this.groupBox4.Controls.Add(this.outputFormatRecordAf);
            this.groupBox4.Controls.Add(this.outputPath);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.outputFormatRecordIq);
            this.groupBox4.Location = new System.Drawing.Point(12, 425);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(363, 121);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Recording";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(10, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(347, 30);
            this.label7.TabIndex = 5;
            this.label7.Text = "If the output file already exists, a new file will be created with a new name. No" +
    " files will be overwritten.";
            // 
            // outputBrowseBtn
            // 
            this.outputBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBrowseBtn.Location = new System.Drawing.Point(282, 37);
            this.outputBrowseBtn.Name = "outputBrowseBtn";
            this.outputBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.outputBrowseBtn.TabIndex = 4;
            this.outputBrowseBtn.Text = "Browse...";
            this.outputBrowseBtn.UseVisualStyleBackColor = true;
            this.outputBrowseBtn.Click += new System.EventHandler(this.outputBrowseBtn_Click);
            // 
            // outputFormatRecordAf
            // 
            this.outputFormatRecordAf.AutoSize = true;
            this.outputFormatRecordAf.Location = new System.Drawing.Point(91, 64);
            this.outputFormatRecordAf.Name = "outputFormatRecordAf";
            this.outputFormatRecordAf.Size = new System.Drawing.Size(76, 17);
            this.outputFormatRecordAf.TabIndex = 1;
            this.outputFormatRecordAf.Text = "Record AF";
            this.outputFormatRecordAf.UseVisualStyleBackColor = true;
            this.outputFormatRecordAf.CheckedChanged += new System.EventHandler(this.outputFormat_CheckedChanged);
            // 
            // outputFormatRecordIq
            // 
            this.outputFormatRecordIq.AutoSize = true;
            this.outputFormatRecordIq.Checked = true;
            this.outputFormatRecordIq.Location = new System.Drawing.Point(11, 64);
            this.outputFormatRecordIq.Name = "outputFormatRecordIq";
            this.outputFormatRecordIq.Size = new System.Drawing.Size(74, 17);
            this.outputFormatRecordIq.TabIndex = 0;
            this.outputFormatRecordIq.TabStop = true;
            this.outputFormatRecordIq.Text = "Record IQ";
            this.outputFormatRecordIq.UseVisualStyleBackColor = true;
            this.outputFormatRecordIq.CheckedChanged += new System.EventHandler(this.outputFormat_CheckedChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(258, 555);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(117, 23);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Save Program";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(48, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 31);
            this.label3.TabIndex = 1;
            this.label3.Text = "Triggers when a certain string is detected in the RDS Radio Text field. Ends when" +
    " that string is no longer found.";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(48, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Triggers at a specific time and will continue until a time you also set.";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(48, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(309, 31);
            this.label8.TabIndex = 6;
            this.label8.Text = "Triggered when the WBFM broadcast pilot is either lost or detected. Only useful i" +
    "n WBFM mode.";
            // 
            // triggerPilot
            // 
            this.triggerPilot.AutoSize = true;
            this.triggerPilot.Location = new System.Drawing.Point(10, 118);
            this.triggerPilot.Name = "triggerPilot";
            this.triggerPilot.Size = new System.Drawing.Size(151, 17);
            this.triggerPilot.TabIndex = 5;
            this.triggerPilot.Text = "WBFM Stereo Pilot Trigger";
            this.triggerPilot.UseVisualStyleBackColor = true;
            // 
            // SdrDvrProgramCreationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 590);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SdrDvrProgramCreationPanel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Create DVR Program";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.changeFreqDial)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox outputPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox settingName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConfigureTrigger;
        private System.Windows.Forms.RadioButton triggerTime;
        private System.Windows.Forms.RadioButton triggerRadioText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown changeFreqDial;
        private System.Windows.Forms.CheckBox changeFreqEnabled;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton outputFormatRecordAf;
        private System.Windows.Forms.RadioButton outputFormatRecordIq;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button outputBrowseBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton triggerPilot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}