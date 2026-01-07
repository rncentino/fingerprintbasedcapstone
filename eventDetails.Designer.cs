namespace BiometricApp
{
    partial class eventDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eventDetails));
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panelheader = new System.Windows.Forms.Panel();
            this.lblEventName = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.AttendanceLogView = new System.Windows.Forms.DataGridView();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbYearLevel = new System.Windows.Forms.ComboBox();
            this.cmbCourse = new System.Windows.Forms.ComboBox();
            this.panelheader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttendanceLogView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblLocation.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLocation.Location = new System.Drawing.Point(290, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(290, 30);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblDate.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDate.Location = new System.Drawing.Point(580, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(290, 30);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelheader
            // 
            this.panelheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.panelheader.Controls.Add(this.lblLocation);
            this.panelheader.Controls.Add(this.lblEventName);
            this.panelheader.Controls.Add(this.closebtn);
            this.panelheader.Controls.Add(this.lblDate);
            this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelheader.Location = new System.Drawing.Point(0, 0);
            this.panelheader.Name = "panelheader";
            this.panelheader.Size = new System.Drawing.Size(900, 30);
            this.panelheader.TabIndex = 8;
            // 
            // lblEventName
            // 
            this.lblEventName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEventName.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblEventName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblEventName.Location = new System.Drawing.Point(0, 0);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(290, 30);
            this.lblEventName.TabIndex = 23;
            this.lblEventName.Text = "EVENTS NAME";
            this.lblEventName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.closebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.Black;
            this.closebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closebtn.Location = new System.Drawing.Point(870, 0);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(30, 30);
            this.closebtn.TabIndex = 6;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // AttendanceLogView
            // 
            this.AttendanceLogView.AllowUserToAddRows = false;
            this.AttendanceLogView.AllowUserToDeleteRows = false;
            this.AttendanceLogView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AttendanceLogView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AttendanceLogView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AttendanceLogView.Location = new System.Drawing.Point(12, 96);
            this.AttendanceLogView.Name = "AttendanceLogView";
            this.AttendanceLogView.ReadOnly = true;
            this.AttendanceLogView.RowHeadersVisible = false;
            this.AttendanceLogView.Size = new System.Drawing.Size(876, 486);
            this.AttendanceLogView.TabIndex = 0;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.btnExportPDF.FlatAppearance.BorderSize = 0;
            this.btnExportPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.btnExportPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExportPDF.Location = new System.Drawing.Point(765, 62);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(118, 21);
            this.btnExportPDF.TabIndex = 9;
            this.btnExportPDF.Text = "Export to PDF";
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 62);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(321, 20);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(339, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(117, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbYearLevel
            // 
            this.cmbYearLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearLevel.FormattingEnabled = true;
            this.cmbYearLevel.Location = new System.Drawing.Point(462, 63);
            this.cmbYearLevel.Name = "cmbYearLevel";
            this.cmbYearLevel.Size = new System.Drawing.Size(142, 21);
            this.cmbYearLevel.TabIndex = 12;
            // 
            // cmbCourse
            // 
            this.cmbCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCourse.FormattingEnabled = true;
            this.cmbCourse.Location = new System.Drawing.Point(610, 62);
            this.cmbCourse.Name = "cmbCourse";
            this.cmbCourse.Size = new System.Drawing.Size(142, 21);
            this.cmbCourse.TabIndex = 13;
            // 
            // eventDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.cmbCourse);
            this.Controls.Add(this.cmbYearLevel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.panelheader);
            this.Controls.Add(this.AttendanceLogView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "eventDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "eventDetails";
            this.Load += new System.EventHandler(this.eventDetails_Load);
            this.panelheader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AttendanceLogView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel panelheader;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.DataGridView AttendanceLogView;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbYearLevel;
        private System.Windows.Forms.ComboBox cmbCourse;
    }
}