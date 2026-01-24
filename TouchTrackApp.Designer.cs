namespace BiometricApp
{
    partial class TouchTrackApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TouchTrackApp));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Statuslabel = new System.Windows.Forms.Label();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.fImage = new System.Windows.Forms.PictureBox();
            this.closebtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.Statuslabel);
            this.panel1.Controls.Add(this.StatusText);
            this.panel1.Controls.Add(this.Prompt);
            this.panel1.Controls.Add(this.fImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 530);
            this.panel1.TabIndex = 0;
            // 
            // Statuslabel
            // 
            this.Statuslabel.AutoSize = true;
            this.Statuslabel.Location = new System.Drawing.Point(24, 509);
            this.Statuslabel.Name = "Statuslabel";
            this.Statuslabel.Size = new System.Drawing.Size(56, 13);
            this.Statuslabel.TabIndex = 30;
            this.Statuslabel.Text = "[STATUS]";
            // 
            // StatusText
            // 
            this.StatusText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.StatusText.Location = new System.Drawing.Point(24, 295);
            this.StatusText.Multiline = true;
            this.StatusText.Name = "StatusText";
            this.StatusText.ReadOnly = true;
            this.StatusText.Size = new System.Drawing.Size(250, 202);
            this.StatusText.TabIndex = 29;
            // 
            // Prompt
            // 
            this.Prompt.BackColor = System.Drawing.SystemColors.Control;
            this.Prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Prompt.Location = new System.Drawing.Point(24, 8);
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.Size = new System.Drawing.Size(250, 13);
            this.Prompt.TabIndex = 28;
            // 
            // fImage
            // 
            this.fImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fImage.BackgroundImage")));
            this.fImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fImage.Location = new System.Drawing.Point(24, 33);
            this.fImage.Name = "fImage";
            this.fImage.Size = new System.Drawing.Size(250, 250);
            this.fImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fImage.TabIndex = 27;
            this.fImage.TabStop = false;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.closebtn.Location = new System.Drawing.Point(710, 0);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(20, 20);
            this.closebtn.TabIndex = 110;
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // TouchTrackApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(730, 530);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TouchTrackApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TouchTrackApp";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Statuslabel;
        private System.Windows.Forms.TextBox StatusText;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.PictureBox fImage;
        private System.Windows.Forms.Button closebtn;
    }
}