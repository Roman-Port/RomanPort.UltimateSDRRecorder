namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    partial class SdrDvrInterface
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
            this.dvrContainer = new System.Windows.Forms.GroupBox();
            this.dvrStatus = new System.Windows.Forms.Label();
            this.actionsBtn = new System.Windows.Forms.Button();
            this.statusLight = new System.Windows.Forms.Label();
            this.dvrContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvrContainer
            // 
            this.dvrContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvrContainer.Controls.Add(this.statusLight);
            this.dvrContainer.Controls.Add(this.dvrStatus);
            this.dvrContainer.Controls.Add(this.actionsBtn);
            this.dvrContainer.ForeColor = System.Drawing.SystemColors.Control;
            this.dvrContainer.Location = new System.Drawing.Point(3, 3);
            this.dvrContainer.Name = "dvrContainer";
            this.dvrContainer.Size = new System.Drawing.Size(215, 42);
            this.dvrContainer.TabIndex = 1;
            this.dvrContainer.TabStop = false;
            this.dvrContainer.Text = "DVR Controller";
            // 
            // dvrStatus
            // 
            this.dvrStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvrStatus.Location = new System.Drawing.Point(24, 13);
            this.dvrStatus.Name = "dvrStatus";
            this.dvrStatus.Size = new System.Drawing.Size(164, 23);
            this.dvrStatus.TabIndex = 6;
            this.dvrStatus.Text = "No Programs Registered!";
            this.dvrStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // actionsBtn
            // 
            this.actionsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.actionsBtn.Location = new System.Drawing.Point(187, 13);
            this.actionsBtn.Name = "actionsBtn";
            this.actionsBtn.Size = new System.Drawing.Size(23, 23);
            this.actionsBtn.TabIndex = 3;
            this.actionsBtn.Text = "⚙";
            this.actionsBtn.UseVisualStyleBackColor = true;
            this.actionsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // statusLight
            // 
            this.statusLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLight.BackColor = System.Drawing.Color.Transparent;
            this.statusLight.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLight.Location = new System.Drawing.Point(-3, -4);
            this.statusLight.Name = "statusLight";
            this.statusLight.Size = new System.Drawing.Size(29, 52);
            this.statusLight.TabIndex = 7;
            this.statusLight.Text = "•";
            this.statusLight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SdrDvrInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.dvrContainer);
            this.Name = "SdrDvrInterface";
            this.Size = new System.Drawing.Size(221, 48);
            this.dvrContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox dvrContainer;
        private System.Windows.Forms.Button actionsBtn;
        private System.Windows.Forms.Label dvrStatus;
        private System.Windows.Forms.Label statusLight;
    }
}
