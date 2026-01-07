namespace BiometricApp
{
    partial class students
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(students));
            this.label2 = new System.Windows.Forms.Label();
            this.addstudentsbtn = new System.Windows.Forms.Button();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.VerifyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.label2.Location = new System.Drawing.Point(20, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 59);
            this.label2.TabIndex = 21;
            this.label2.Text = "STUDENTS TABLE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addstudentsbtn
            // 
            this.addstudentsbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addstudentsbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(252)))), ((int)(((byte)(132)))));
            this.addstudentsbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addstudentsbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addstudentsbtn.FlatAppearance.BorderSize = 0;
            this.addstudentsbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.addstudentsbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.addstudentsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addstudentsbtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addstudentsbtn.ForeColor = System.Drawing.Color.White;
            this.addstudentsbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addstudentsbtn.Location = new System.Drawing.Point(900, 33);
            this.addstudentsbtn.Name = "addstudentsbtn";
            this.addstudentsbtn.Size = new System.Drawing.Size(80, 59);
            this.addstudentsbtn.TabIndex = 22;
            this.addstudentsbtn.Text = "Enroll";
            this.addstudentsbtn.UseVisualStyleBackColor = false;
            this.addstudentsbtn.Click += new System.EventHandler(this.addstudentsbtn_Click);
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AllowUserToAddRows = false;
            this.dataGridViewStudents.AllowUserToDeleteRows = false;
            this.dataGridViewStudents.AllowUserToResizeColumns = false;
            this.dataGridViewStudents.AllowUserToResizeRows = false;
            this.dataGridViewStudents.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStudents.BackgroundColor = System.Drawing.Color.Silver;
            this.dataGridViewStudents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(12, 98);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.ReadOnly = true;
            this.dataGridViewStudents.RowHeadersVisible = false;
            this.dataGridViewStudents.Size = new System.Drawing.Size(976, 514);
            this.dataGridViewStudents.TabIndex = 23;
            // 
            // VerifyBtn
            // 
            this.VerifyBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.VerifyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(252)))), ((int)(((byte)(132)))));
            this.VerifyBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VerifyBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VerifyBtn.FlatAppearance.BorderSize = 0;
            this.VerifyBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.VerifyBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.VerifyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VerifyBtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyBtn.ForeColor = System.Drawing.Color.White;
            this.VerifyBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VerifyBtn.Location = new System.Drawing.Point(810, 33);
            this.VerifyBtn.Name = "VerifyBtn";
            this.VerifyBtn.Size = new System.Drawing.Size(80, 59);
            this.VerifyBtn.TabIndex = 24;
            this.VerifyBtn.Text = "Verify";
            this.VerifyBtn.UseVisualStyleBackColor = false;
            this.VerifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // students
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.VerifyBtn);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.addstudentsbtn);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "students";
            this.Text = "students";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addstudentsbtn;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.Button VerifyBtn;
    }
}