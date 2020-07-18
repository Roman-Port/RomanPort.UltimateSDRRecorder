namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    partial class RdsAutoNameForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nameList = new System.Windows.Forms.ListView();
            this.rdsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtLastSeen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveBtn = new System.Windows.Forms.Button();
            this.manualFilenameSave = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Confirm which RDS name you\'d like to save the file with. The system attempted to " +
    "guess what name to use.";
            // 
            // nameList
            // 
            this.nameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rdsName,
            this.rtLastSeen});
            this.nameList.FullRowSelect = true;
            this.nameList.GridLines = true;
            this.nameList.HideSelection = false;
            this.nameList.Location = new System.Drawing.Point(12, 35);
            this.nameList.MultiSelect = false;
            this.nameList.Name = "nameList";
            this.nameList.Size = new System.Drawing.Size(619, 174);
            this.nameList.TabIndex = 1;
            this.nameList.UseCompatibleStateImageBehavior = false;
            this.nameList.View = System.Windows.Forms.View.Details;
            this.nameList.SelectedIndexChanged += new System.EventHandler(this.nameList_SelectedIndexChanged);
            // 
            // rdsName
            // 
            this.rdsName.Text = "RDS RadioText";
            this.rdsName.Width = 225;
            // 
            // rtLastSeen
            // 
            this.rtLastSeen.Text = "Last Seen";
            this.rtLastSeen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rtLastSeen.Width = 97;
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(509, 222);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(122, 23);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // manualFilenameSave
            // 
            this.manualFilenameSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.manualFilenameSave.Location = new System.Drawing.Point(428, 222);
            this.manualFilenameSave.Name = "manualFilenameSave";
            this.manualFilenameSave.Size = new System.Drawing.Size(75, 23);
            this.manualFilenameSave.TabIndex = 3;
            this.manualFilenameSave.Text = "Manual";
            this.manualFilenameSave.UseVisualStyleBackColor = true;
            this.manualFilenameSave.Click += new System.EventHandler(this.manualFilenameSave_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.Location = new System.Drawing.Point(347, 222);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // RdsAutoNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 257);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.manualFilenameSave);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.nameList);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 296);
            this.Name = "RdsAutoNameForm";
            this.Text = "RDS Auto Name";
            this.Load += new System.EventHandler(this.RdsAutoNameForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView nameList;
        private System.Windows.Forms.ColumnHeader rdsName;
        private System.Windows.Forms.ColumnHeader rtLastSeen;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button manualFilenameSave;
        private System.Windows.Forms.Button cancelBtn;
    }
}