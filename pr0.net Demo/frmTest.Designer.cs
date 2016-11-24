namespace pr0.net_Demo
{
    partial class frmTest
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.imlThumbs = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ForeColor = System.Drawing.Color.FloralWhite;
            this.listView1.LargeImageList = this.imlThumbs;
            this.listView1.Location = new System.Drawing.Point(20, 60);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(416, 364);
            this.listView1.SmallImageList = this.imlThumbs;
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(128, 256);
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imlThumbs
            // 
            this.imlThumbs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlThumbs.ImageSize = new System.Drawing.Size(128, 128);
            this.imlThumbs.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 444);
            this.Controls.Add(this.listView1);
            this.Name = "frmTest";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "pr0.net";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTest_FormClosing);
            this.Shown += new System.EventHandler(this.frmTest_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imlThumbs;
    }
}