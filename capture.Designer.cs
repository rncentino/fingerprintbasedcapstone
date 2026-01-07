namespace BiometricApp
{
    partial class capture
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(capture));
            this.fImage = new System.Windows.Forms.PictureBox();
            this.closebtn = new System.Windows.Forms.Button();
            this.panelheader = new System.Windows.Forms.Panel();
            this.student_id = new System.Windows.Forms.TextBox();
            this.lname = new System.Windows.Forms.TextBox();
            this.fname = new System.Windows.Forms.TextBox();
            this.mname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yearLevel = new System.Windows.Forms.ComboBox();
            this.course = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.Statuslabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.start_scan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).BeginInit();
            this.panelheader.SuspendLayout();
            this.SuspendLayout();
            // 
            // fImage
            // 
            this.fImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fImage.BackgroundImage")));
            this.fImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fImage.Location = new System.Drawing.Point(18, 268);
            this.fImage.Name = "fImage";
            this.fImage.Size = new System.Drawing.Size(250, 250);
            this.fImage.TabIndex = 5;
            this.fImage.TabStop = false;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.closebtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closebtn.Location = new System.Drawing.Point(537, 1);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(30, 30);
            this.closebtn.TabIndex = 6;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // panelheader
            // 
            this.panelheader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.panelheader.Controls.Add(this.closebtn);
            this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelheader.Location = new System.Drawing.Point(0, 0);
            this.panelheader.Name = "panelheader";
            this.panelheader.Size = new System.Drawing.Size(568, 30);
            this.panelheader.TabIndex = 7;
            // 
            // student_id
            // 
            this.student_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.student_id.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.student_id.Location = new System.Drawing.Point(183, 56);
            this.student_id.Name = "student_id";
            this.student_id.Size = new System.Drawing.Size(366, 23);
            this.student_id.TabIndex = 0;
            this.student_id.TextChanged += new System.EventHandler(this.student_id_TextChanged);
            // 
            // lname
            // 
            this.lname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lname.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lname.Location = new System.Drawing.Point(183, 90);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(366, 23);
            this.lname.TabIndex = 1;
            this.lname.TextChanged += new System.EventHandler(this.lname_TextChanged);
            // 
            // fname
            // 
            this.fname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fname.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fname.Location = new System.Drawing.Point(183, 124);
            this.fname.Name = "fname";
            this.fname.Size = new System.Drawing.Size(366, 23);
            this.fname.TabIndex = 2;
            this.fname.TextChanged += new System.EventHandler(this.fname_TextChanged);
            // 
            // mname
            // 
            this.mname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mname.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mname.Location = new System.Drawing.Point(183, 158);
            this.mname.Name = "mname";
            this.mname.Size = new System.Drawing.Size(366, 23);
            this.mname.TabIndex = 3;
            this.mname.TextChanged += new System.EventHandler(this.mname_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label1.Location = new System.Drawing.Point(18, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Student ID:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label2.Location = new System.Drawing.Point(18, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Last Name:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label3.Location = new System.Drawing.Point(18, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "First Name:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label4.Location = new System.Drawing.Point(18, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Middle Name:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // yearLevel
            // 
            this.yearLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearLevel.FormattingEnabled = true;
            this.yearLevel.Location = new System.Drawing.Point(183, 192);
            this.yearLevel.Name = "yearLevel";
            this.yearLevel.Size = new System.Drawing.Size(366, 21);
            this.yearLevel.TabIndex = 4;
            this.yearLevel.SelectedIndexChanged += new System.EventHandler(this.yearLevel_SelectedIndexChanged_1);
            // 
            // course
            // 
            this.course.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.course.FormattingEnabled = true;
            this.course.Location = new System.Drawing.Point(183, 226);
            this.course.Name = "course";
            this.course.Size = new System.Drawing.Size(366, 21);
            this.course.TabIndex = 5;
            this.course.SelectedIndexChanged += new System.EventHandler(this.course_SelectedIndexChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label5.Location = new System.Drawing.Point(18, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "Year Level:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.label6.Location = new System.Drawing.Point(18, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 19);
            this.label6.TabIndex = 19;
            this.label6.Text = "Course/Program:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Prompt
            // 
            this.Prompt.BackColor = System.Drawing.SystemColors.Control;
            this.Prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Prompt.Location = new System.Drawing.Point(278, 268);
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.Size = new System.Drawing.Size(271, 13);
            this.Prompt.TabIndex = 22;
            // 
            // StatusText
            // 
            this.StatusText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.StatusText.Location = new System.Drawing.Point(278, 290);
            this.StatusText.Multiline = true;
            this.StatusText.Name = "StatusText";
            this.StatusText.ReadOnly = true;
            this.StatusText.Size = new System.Drawing.Size(271, 228);
            this.StatusText.TabIndex = 23;
            // 
            // Statuslabel
            // 
            this.Statuslabel.AutoSize = true;
            this.Statuslabel.Location = new System.Drawing.Point(18, 535);
            this.Statuslabel.Name = "Statuslabel";
            this.Statuslabel.Size = new System.Drawing.Size(56, 13);
            this.Statuslabel.TabIndex = 24;
            this.Statuslabel.Text = "[STATUS]";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // start_scan
            // 
            this.start_scan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(252)))), ((int)(((byte)(132)))));
            this.start_scan.FlatAppearance.BorderSize = 0;
            this.start_scan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.start_scan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.start_scan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start_scan.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.start_scan.ForeColor = System.Drawing.Color.White;
            this.start_scan.Location = new System.Drawing.Point(436, 530);
            this.start_scan.Name = "start_scan";
            this.start_scan.Size = new System.Drawing.Size(113, 38);
            this.start_scan.TabIndex = 25;
            this.start_scan.Text = "Start Scan";
            this.start_scan.UseVisualStyleBackColor = false;
            this.start_scan.Click += new System.EventHandler(this.start_scan_Click);
            // 
            // capture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.ClientSize = new System.Drawing.Size(568, 580);
            this.Controls.Add(this.start_scan);
            this.Controls.Add(this.Statuslabel);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.Prompt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.course);
            this.Controls.Add(this.yearLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mname);
            this.Controls.Add(this.fname);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.student_id);
            this.Controls.Add(this.panelheader);
            this.Controls.Add(this.fImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "capture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Register Student";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.capture_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).EndInit();
            this.panelheader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fImage;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Panel panelheader;
        private System.Windows.Forms.TextBox student_id;
        private System.Windows.Forms.TextBox lname;
        private System.Windows.Forms.TextBox fname;
        private System.Windows.Forms.TextBox mname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox yearLevel;
        private System.Windows.Forms.ComboBox course;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.TextBox StatusText;
        private System.Windows.Forms.Label Statuslabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button start_scan;
    }
}