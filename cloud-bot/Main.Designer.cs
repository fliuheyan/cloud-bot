namespace cloud_bot
{
    partial class Main
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
            this.createButton = new System.Windows.Forms.Button();
            this.injectButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.processListView = new System.Windows.Forms.ListBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(655, 290);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(142, 53);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // injectButton
            // 
            this.injectButton.Location = new System.Drawing.Point(655, 214);
            this.injectButton.Name = "injectButton";
            this.injectButton.Size = new System.Drawing.Size(142, 53);
            this.injectButton.TabIndex = 2;
            this.injectButton.Text = "inject";
            this.injectButton.UseVisualStyleBackColor = true;
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(115, 65);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(142, 53);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // processListView
            // 
            this.processListView.FormattingEnabled = true;
            this.processListView.ItemHeight = 18;
            this.processListView.Location = new System.Drawing.Point(115, 162);
            this.processListView.Name = "processListView";
            this.processListView.Size = new System.Drawing.Size(481, 238);
            this.processListView.TabIndex = 5;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(392, 91);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(107, 18);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "statusLabel";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.processListView);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.injectButton);
            this.Controls.Add(this.createButton);
            this.Name = "Main";
            this.Text = "Cloud-Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button injectButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ListBox processListView;
        private System.Windows.Forms.Label statusLabel;
    }


}

