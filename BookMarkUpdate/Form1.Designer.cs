namespace BookMarkUpdate
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.page = new System.Windows.Forms.TabControl();
            this.page_Save = new System.Windows.Forms.TabPage();
            this.page_Sync = new System.Windows.Forms.TabPage();
            this.page.SuspendLayout();
            this.SuspendLayout();
            // 
            // page
            // 
            this.page.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.page.Controls.Add(this.page_Save);
            this.page.Controls.Add(this.page_Sync);
            this.page.Location = new System.Drawing.Point(12, 30);
            this.page.Name = "page";
            this.page.SelectedIndex = 0;
            this.page.Size = new System.Drawing.Size(769, 472);
            this.page.TabIndex = 0;
            // 
            // page_Save
            // 
            this.page_Save.Location = new System.Drawing.Point(8, 39);
            this.page_Save.Name = "page_Save";
            this.page_Save.Padding = new System.Windows.Forms.Padding(3);
            this.page_Save.Size = new System.Drawing.Size(753, 425);
            this.page_Save.TabIndex = 0;
            this.page_Save.Text = "保存";
            this.page_Save.UseVisualStyleBackColor = true;
            // 
            // page_Sync
            // 
            this.page_Sync.Location = new System.Drawing.Point(8, 39);
            this.page_Sync.Name = "page_Sync";
            this.page_Sync.Padding = new System.Windows.Forms.Padding(3);
            this.page_Sync.Size = new System.Drawing.Size(753, 455);
            this.page_Sync.TabIndex = 1;
            this.page_Sync.Text = "同步";
            this.page_Sync.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 514);
            this.Controls.Add(this.page);
            this.Name = "Form1";
            this.Text = "Form1";
            this.page.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl page;
        private System.Windows.Forms.TabPage page_Save;
        private System.Windows.Forms.TabPage page_Sync;
    }
}

