namespace BiometricApp
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.studentsbtn = new System.Windows.Forms.Button();
            this.eventsbtn = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.panelheader = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelheader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // studentsbtn
            // 
            this.studentsbtn.BackColor = System.Drawing.Color.White;
            this.studentsbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.studentsbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.studentsbtn.FlatAppearance.BorderSize = 0;
            this.studentsbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.studentsbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.studentsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.studentsbtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentsbtn.ForeColor = System.Drawing.Color.Black;
            this.studentsbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.studentsbtn.Location = new System.Drawing.Point(750, 0);
            this.studentsbtn.Name = "studentsbtn";
            this.studentsbtn.Size = new System.Drawing.Size(100, 50);
            this.studentsbtn.TabIndex = 2;
            this.studentsbtn.Text = "STUDENTS";
            this.studentsbtn.UseVisualStyleBackColor = false;
            this.studentsbtn.Click += new System.EventHandler(this.studentsbtn_Click);
            // 
            // eventsbtn
            // 
            this.eventsbtn.BackColor = System.Drawing.Color.White;
            this.eventsbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.eventsbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.eventsbtn.FlatAppearance.BorderSize = 0;
            this.eventsbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.eventsbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.eventsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eventsbtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsbtn.ForeColor = System.Drawing.Color.Black;
            this.eventsbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.eventsbtn.Location = new System.Drawing.Point(650, 0);
            this.eventsbtn.Name = "eventsbtn";
            this.eventsbtn.Size = new System.Drawing.Size(100, 50);
            this.eventsbtn.TabIndex = 1;
            this.eventsbtn.Text = "EVENTS";
            this.eventsbtn.UseVisualStyleBackColor = false;
            this.eventsbtn.Click += new System.EventHandler(this.eventsbtn_Click);
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closebtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.Red;
            this.closebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closebtn.Location = new System.Drawing.Point(975, 0);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(25, 25);
            this.closebtn.TabIndex = 4;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // mainpanel
            // 
            this.mainpanel.BackColor = System.Drawing.Color.White;
            this.mainpanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Location = new System.Drawing.Point(0, 50);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(1000, 650);
            this.mainpanel.TabIndex = 2;
            // 
            // panelheader
            // 
            this.panelheader.BackColor = System.Drawing.Color.White;
            this.panelheader.Controls.Add(this.button1);
            this.panelheader.Controls.Add(this.studentsbtn);
            this.panelheader.Controls.Add(this.label1);
            this.panelheader.Controls.Add(this.closebtn);
            this.panelheader.Controls.Add(this.eventsbtn);
            this.panelheader.Controls.Add(this.pictureBox1);
            this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelheader.Location = new System.Drawing.Point(0, 0);
            this.panelheader.Name = "panelheader";
            this.panelheader.Size = new System.Drawing.Size(1000, 50);
            this.panelheader.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(850, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "LOGOUT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(69, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 50);
            this.label1.TabIndex = 5;
            this.label1.Text = "Northern Samar Colleges";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.mainpanel);
            this.Controls.Add(this.panelheader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelheader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.Button studentsbtn;
        private System.Windows.Forms.Button eventsbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Panel panelheader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

