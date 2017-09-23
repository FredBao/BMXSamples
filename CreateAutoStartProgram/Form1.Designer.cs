namespace CreateAutoStartProgram
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
            this.BtnAdd = new System.Windows.Forms.Button();
            this.程序路径 = new System.Windows.Forms.Label();
            this.TbProgramPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(386, 84);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 0;
            this.BtnAdd.Text = "添加";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // 程序路径
            // 
            this.程序路径.AutoSize = true;
            this.程序路径.Location = new System.Drawing.Point(12, 24);
            this.程序路径.Name = "程序路径";
            this.程序路径.Size = new System.Drawing.Size(41, 12);
            this.程序路径.TabIndex = 1;
            this.程序路径.Text = "label1";
            // 
            // TbProgramPath
            // 
            this.TbProgramPath.Location = new System.Drawing.Point(59, 21);
            this.TbProgramPath.Name = "TbProgramPath";
            this.TbProgramPath.Size = new System.Drawing.Size(402, 21);
            this.TbProgramPath.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 110);
            this.Controls.Add(this.TbProgramPath);
            this.Controls.Add(this.程序路径);
            this.Controls.Add(this.BtnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label 程序路径;
        private System.Windows.Forms.TextBox TbProgramPath;
    }
}

