namespace BiometricApp
{
    partial class addactivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addactivity));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.location = new System.Windows.Forms.TextBox();
            this.eventName = new System.Windows.Forms.TextBox();
            this.closebtn = new System.Windows.Forms.Button();
            this.panelheader = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.CreateEventBtn = new System.Windows.Forms.Button();
            this.panelheader.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 19);
            this.label2.TabIndex = 17;
            this.label2.Text = "Venue:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "Activity:";
            // 
            // location
            // 
            this.location.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.location.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.location.Location = new System.Drawing.Point(180, 92);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(366, 23);
            this.location.TabIndex = 15;
            // 
            // eventName
            // 
            this.eventName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventName.Location = new System.Drawing.Point(180, 58);
            this.eventName.Name = "eventName";
            this.eventName.Size = new System.Drawing.Size(366, 23);
            this.eventName.TabIndex = 14;
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
            this.panelheader.Controls.Add(this.button2);
            this.panelheader.Controls.Add(this.closebtn);
            this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelheader.Location = new System.Drawing.Point(0, 0);
            this.panelheader.Name = "panelheader";
            this.panelheader.Size = new System.Drawing.Size(568, 30);
            this.panelheader.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(196)))), ((int)(((byte)(176)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(970, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // date
            // 
            this.date.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.date.Location = new System.Drawing.Point(180, 130);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(366, 23);
            this.date.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(15, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = "Date:";
            // 
            // CreateEventBtn
            // 
            this.CreateEventBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CreateEventBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(252)))), ((int)(((byte)(132)))));
            this.CreateEventBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CreateEventBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateEventBtn.FlatAppearance.BorderSize = 0;
            this.CreateEventBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.CreateEventBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(169)))), ((int)(((byte)(92)))));
            this.CreateEventBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateEventBtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateEventBtn.ForeColor = System.Drawing.Color.White;
            this.CreateEventBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CreateEventBtn.Location = new System.Drawing.Point(432, 171);
            this.CreateEventBtn.Name = "CreateEventBtn";
            this.CreateEventBtn.Size = new System.Drawing.Size(114, 42);
            this.CreateEventBtn.TabIndex = 27;
            this.CreateEventBtn.Text = "Create Event";
            this.CreateEventBtn.UseVisualStyleBackColor = false;
            this.CreateEventBtn.Click += new System.EventHandler(this.CreateEventBtn_Click);
            // 
            // addactivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 225);
            this.Controls.Add(this.CreateEventBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.date);
            this.Controls.Add(this.panelheader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.location);
            this.Controls.Add(this.eventName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addactivity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "addactivity";
            this.Load += new System.EventHandler(this.addactivity_Load);
            this.panelheader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox location;
        private System.Windows.Forms.TextBox eventName;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Panel panelheader;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CreateEventBtn;
    }
}