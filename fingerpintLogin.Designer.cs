namespace BiometricApp
{
    partial class fingerpintLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fingerpintLogin));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblEventName = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.fImage = new System.Windows.Forms.PictureBox();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.Statuslabel = new System.Windows.Forms.Label();
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.panelHeader.Controls.Add(this.lblLocation);
            this.panelHeader.Controls.Add(this.lblDate);
            this.panelHeader.Controls.Add(this.lblEventName);
            this.panelHeader.Controls.Add(this.closebtn);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 30);
            this.panelHeader.TabIndex = 0;
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblLocation.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLocation.Location = new System.Drawing.Point(290, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(290, 30);
            this.lblLocation.TabIndex = 3;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblDate.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.Location = new System.Drawing.Point(580, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(290, 30);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEventName
            // 
            this.lblEventName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEventName.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblEventName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblEventName.Location = new System.Drawing.Point(0, 0);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(290, 30);
            this.lblEventName.TabIndex = 24;
            this.lblEventName.Text = "EVENTS NAME";
            this.lblEventName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.closebtn.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.closebtn.Location = new System.Drawing.Point(870, 0);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(30, 30);
            this.closebtn.TabIndex = 7;
            this.closebtn.Text = "x";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // fImage
            // 
            this.fImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fImage.BackgroundImage")));
            this.fImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fImage.Location = new System.Drawing.Point(15, 93);
            this.fImage.Name = "fImage";
            this.fImage.Size = new System.Drawing.Size(250, 250);
            this.fImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fImage.TabIndex = 6;
            this.fImage.TabStop = false;
            // 
            // StatusText
            // 
            this.StatusText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.StatusText.Location = new System.Drawing.Point(15, 355);
            this.StatusText.Multiline = true;
            this.StatusText.Name = "StatusText";
            this.StatusText.ReadOnly = true;
            this.StatusText.Size = new System.Drawing.Size(250, 202);
            this.StatusText.TabIndex = 25;
            // 
            // Prompt
            // 
            this.Prompt.BackColor = System.Drawing.SystemColors.Control;
            this.Prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Prompt.Location = new System.Drawing.Point(15, 68);
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.Size = new System.Drawing.Size(250, 13);
            this.Prompt.TabIndex = 24;
            // 
            // Statuslabel
            // 
            this.Statuslabel.AutoSize = true;
            this.Statuslabel.Location = new System.Drawing.Point(12, 569);
            this.Statuslabel.Name = "Statuslabel";
            this.Statuslabel.Size = new System.Drawing.Size(56, 13);
            this.Statuslabel.TabIndex = 26;
            this.Statuslabel.Text = "[STATUS]";
            // 
            // dataGridViewLogs
            // 
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            this.dataGridViewLogs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.dataGridViewLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.Location = new System.Drawing.Point(290, 63);
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.ReadOnly = true;
            this.dataGridViewLogs.RowHeadersVisible = false;
            this.dataGridViewLogs.Size = new System.Drawing.Size(600, 519);
            this.dataGridViewLogs.TabIndex = 28;
            // 
            // fingerpintLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dataGridViewLogs);
            this.Controls.Add(this.Statuslabel);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.Prompt);
            this.Controls.Add(this.fImage);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fingerpintLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fingerpintLogin";
            this.Load += new System.EventHandler(this.fingerpintLogin_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.PictureBox fImage;
        private System.Windows.Forms.TextBox StatusText;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.Label Statuslabel;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
    }
}