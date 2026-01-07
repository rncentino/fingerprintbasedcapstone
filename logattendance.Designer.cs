namespace BiometricApp
{
    partial class logattendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(logattendance));
            this.flowLayoutPanelEvents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelEvents
            // 
            this.flowLayoutPanelEvents.AutoScroll = true;
            this.flowLayoutPanelEvents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelEvents.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelEvents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelEvents.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelEvents.Name = "flowLayoutPanelEvents";
            this.flowLayoutPanelEvents.Size = new System.Drawing.Size(1000, 700);
            this.flowLayoutPanelEvents.TabIndex = 0;
            this.flowLayoutPanelEvents.WrapContents = false;
            // 
            // logattendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.flowLayoutPanelEvents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "logattendance";
            this.Text = "logattendance";
            this.Load += new System.EventHandler(this.logattendance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEvents;
    }
}