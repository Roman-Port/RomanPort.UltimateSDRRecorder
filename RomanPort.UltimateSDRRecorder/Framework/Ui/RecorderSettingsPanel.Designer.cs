namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    partial class RecorderSettingsPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.rewindBufferLabel = new System.Windows.Forms.Label();
            this.rewindBufferLength = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.browseOutputFolder = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ampGroup = new System.Windows.Forms.GroupBox();
            this.clippingMeter = new System.Windows.Forms.Label();
            this.afAmplicationTrack = new System.Windows.Forms.TrackBar();
            this.afAmplificationLabel = new System.Windows.Forms.Label();
            this.amplificationReset = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rewindBufferLength)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.ampGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afAmplicationTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rewindBufferLabel);
            this.groupBox1.Controls.Add(this.rewindBufferLength);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rewind Buffer";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "The rewind buffer allows you to save the last seconds received without recording." +
    " The rewind buffer sits in RAM.";
            // 
            // rewindBufferLabel
            // 
            this.rewindBufferLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rewindBufferLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rewindBufferLabel.Location = new System.Drawing.Point(97, 16);
            this.rewindBufferLabel.Name = "rewindBufferLabel";
            this.rewindBufferLabel.Size = new System.Drawing.Size(196, 23);
            this.rewindBufferLabel.TabIndex = 1;
            this.rewindBufferLabel.Text = "seconds / 100 MB";
            this.rewindBufferLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rewindBufferLength
            // 
            this.rewindBufferLength.Location = new System.Drawing.Point(6, 19);
            this.rewindBufferLength.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.rewindBufferLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rewindBufferLength.Name = "rewindBufferLength";
            this.rewindBufferLength.Size = new System.Drawing.Size(90, 20);
            this.rewindBufferLength.TabIndex = 0;
            this.rewindBufferLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.rewindBufferLength.ValueChanged += new System.EventHandler(this.rewindBufferLength_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.browseOutputFolder);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RDS Auto-Name";
            // 
            // browseOutputFolder
            // 
            this.browseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseOutputFolder.Location = new System.Drawing.Point(224, 36);
            this.browseOutputFolder.Name = "browseOutputFolder";
            this.browseOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.browseOutputFolder.TabIndex = 6;
            this.browseOutputFolder.Text = "Browse...";
            this.browseOutputFolder.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(10, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output Folder";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(7, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "RDS Auto-Name allows you to automatically name saved files based upon the RDS Rad" +
    "ioText name.";
            // 
            // ampGroup
            // 
            this.ampGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ampGroup.Controls.Add(this.amplificationReset);
            this.ampGroup.Controls.Add(this.clippingMeter);
            this.ampGroup.Controls.Add(this.afAmplicationTrack);
            this.ampGroup.Controls.Add(this.afAmplificationLabel);
            this.ampGroup.Location = new System.Drawing.Point(12, 215);
            this.ampGroup.Name = "ampGroup";
            this.ampGroup.Size = new System.Drawing.Size(305, 92);
            this.ampGroup.TabIndex = 7;
            this.ampGroup.TabStop = false;
            this.ampGroup.Text = "Amplification";
            // 
            // clippingMeter
            // 
            this.clippingMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clippingMeter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.clippingMeter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clippingMeter.ForeColor = System.Drawing.Color.White;
            this.clippingMeter.Location = new System.Drawing.Point(141, 61);
            this.clippingMeter.Name = "clippingMeter";
            this.clippingMeter.Padding = new System.Windows.Forms.Padding(5);
            this.clippingMeter.Size = new System.Drawing.Size(151, 23);
            this.clippingMeter.TabIndex = 8;
            this.clippingMeter.Text = "CLIPPING DETECTED";
            this.clippingMeter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // afAmplicationTrack
            // 
            this.afAmplicationTrack.LargeChange = 10;
            this.afAmplicationTrack.Location = new System.Drawing.Point(10, 19);
            this.afAmplicationTrack.Maximum = 200;
            this.afAmplicationTrack.Name = "afAmplicationTrack";
            this.afAmplicationTrack.Size = new System.Drawing.Size(282, 45);
            this.afAmplicationTrack.SmallChange = 5;
            this.afAmplicationTrack.TabIndex = 5;
            this.afAmplicationTrack.TickFrequency = 10;
            this.afAmplicationTrack.Value = 10;
            this.afAmplicationTrack.Scroll += new System.EventHandler(this.afAmplicationTrack_Scroll);
            // 
            // afAmplificationLabel
            // 
            this.afAmplificationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.afAmplificationLabel.Location = new System.Drawing.Point(7, 61);
            this.afAmplificationLabel.Name = "afAmplificationLabel";
            this.afAmplificationLabel.Size = new System.Drawing.Size(109, 23);
            this.afAmplificationLabel.TabIndex = 3;
            this.afAmplificationLabel.Text = "AF Amplification: 1";
            this.afAmplificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // amplificationReset
            // 
            this.amplificationReset.Location = new System.Drawing.Point(116, 61);
            this.amplificationReset.Name = "amplificationReset";
            this.amplificationReset.Size = new System.Drawing.Size(23, 23);
            this.amplificationReset.TabIndex = 8;
            this.amplificationReset.Text = "1";
            this.amplificationReset.UseVisualStyleBackColor = true;
            this.amplificationReset.Click += new System.EventHandler(this.amplificationReset_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBtn.Location = new System.Drawing.Point(242, 316);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 8;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // RecorderSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 351);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.ampGroup);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecorderSettingsPanel";
            this.Text = "Recorder Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecorderSettingsPanel_FormClosing);
            this.Load += new System.EventHandler(this.RecorderSettingsPanel_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rewindBufferLength)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ampGroup.ResumeLayout(false);
            this.ampGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afAmplicationTrack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label rewindBufferLabel;
        private System.Windows.Forms.NumericUpDown rewindBufferLength;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseOutputFolder;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox ampGroup;
        private System.Windows.Forms.Label afAmplificationLabel;
        private System.Windows.Forms.TrackBar afAmplicationTrack;
        private System.Windows.Forms.Label clippingMeter;
        private System.Windows.Forms.Button amplificationReset;
        private System.Windows.Forms.Button applyBtn;
    }
}