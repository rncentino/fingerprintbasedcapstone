namespace BiometricApp
{
    partial class events
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(events));
            this.dataGridViewEvents = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.CreateEventBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEvents
            // 
            this.dataGridViewEvents.AllowUserToAddRows = false;
            this.dataGridViewEvents.AllowUserToDeleteRows = false;
            this.dataGridViewEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEvents.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.dataGridViewEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvents.Location = new System.Drawing.Point(12, 98);
            this.dataGridViewEvents.Name = "dataGridViewEvents";
            this.dataGridViewEvents.ReadOnly = true;
            this.dataGridViewEvents.RowHeadersVisible = false;
            this.dataGridViewEvents.Size = new System.Drawing.Size(976, 514);
            this.dataGridViewEvents.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Font = new System.Drawing.Font("Bauhaus 93", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(121)))), ((int)(((byte)(194)))));
            this.label2.Location = new System.Drawing.Point(20, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 59);
            this.label2.TabIndex = 22;
            this.label2.Text = "EVENTS TABLE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.CreateEventBtn.Location = new System.Drawing.Point(874, 33);
            this.CreateEventBtn.Name = "CreateEventBtn";
            this.CreateEventBtn.Size = new System.Drawing.Size(114, 59);
            this.CreateEventBtn.TabIndex = 26;
            this.CreateEventBtn.Text = "Create Event";
            this.CreateEventBtn.UseVisualStyleBackColor = false;
            this.CreateEventBtn.Click += new System.EventHandler(this.CreateEventBtn_Click);
            // 
            // events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.CreateEventBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewEvents);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "events";
            this.Text = "events";
            this.Load += new System.EventHandler(this.events_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEvents;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CreateEventBtn;
    }
}