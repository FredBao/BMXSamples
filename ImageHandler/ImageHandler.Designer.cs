namespace ImageHandler
{
    partial class ImageHandler
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Y_Offset = new System.Windows.Forms.NumericUpDown();
            this.X_Offset = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_FontSize = new System.Windows.Forms.ComboBox();
            this.cb_FontFamily = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.hierarchyTableAdapter1 = new XtraReportsDemos.TreeView.dsHierarchyTableAdapters.HierarchyTableAdapter();
            this.connectionNameControl1 = new DevExpress.DataAccess.UI.Native.ConnectionNameControl();
            this.providerChooser1 = new DevExpress.DataAccess.UI.Native.ProviderChooser();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Y_Offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X_Offset)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.providerChooser1);
            this.panel1.Controls.Add(this.connectionNameControl1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 493);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(754, 354);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Y_Offset);
            this.panel3.Controls.Add(this.X_Offset);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cb_FontSize);
            this.panel3.Controls.Add(this.cb_FontFamily);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(754, 139);
            this.panel3.TabIndex = 0;
            // 
            // Y_Offset
            // 
            this.Y_Offset.Location = new System.Drawing.Point(449, 81);
            this.Y_Offset.Name = "Y_Offset";
            this.Y_Offset.Size = new System.Drawing.Size(164, 21);
            this.Y_Offset.TabIndex = 14;
            // 
            // X_Offset
            // 
            this.X_Offset.Location = new System.Drawing.Point(449, 55);
            this.X_Offset.Name = "X_Offset";
            this.X_Offset.Size = new System.Drawing.Size(164, 21);
            this.X_Offset.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(390, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "垂直偏移";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "水平偏移";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 10;
            // 
            // cb_FontSize
            // 
            this.cb_FontSize.FormattingEnabled = true;
            this.cb_FontSize.Items.AddRange(new object[] {
            "10"});
            this.cb_FontSize.Location = new System.Drawing.Point(105, 106);
            this.cb_FontSize.Name = "cb_FontSize";
            this.cb_FontSize.Size = new System.Drawing.Size(258, 20);
            this.cb_FontSize.TabIndex = 9;
            this.cb_FontSize.Text = "10";
            // 
            // cb_FontFamily
            // 
            this.cb_FontFamily.FormattingEnabled = true;
            this.cb_FontFamily.Location = new System.Drawing.Point(106, 80);
            this.cb_FontFamily.Name = "cb_FontFamily";
            this.cb_FontFamily.Size = new System.Drawing.Size(257, 20);
            this.cb_FontFamily.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(106, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "选择颜色";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "选择文字大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "选择文字字体";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(187, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "预览效果";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(268, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存图片";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "附加文字";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(257, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "惠山万达艺海琴行杯";
            // 
            // hierarchyTableAdapter1
            // 
            this.hierarchyTableAdapter1.ClearBeforeFill = true;
            // 
            // connectionNameControl1
            // 
            this.connectionNameControl1.ConnectionName = "";
            this.connectionNameControl1.IsConnectionNameChanged = false;
            this.connectionNameControl1.Location = new System.Drawing.Point(212, 197);
            this.connectionNameControl1.Name = "connectionNameControl1";
            this.connectionNameControl1.Size = new System.Drawing.Size(401, 29);
            this.connectionNameControl1.TabIndex = 2;
            this.connectionNameControl1.Load += new System.EventHandler(this.connectionNameControl1_Load);
            // 
            // providerChooser1
            // 
            this.providerChooser1.ConnectionNameVisible = true;
            this.providerChooser1.DataProviderConfigurator = null;
            this.providerChooser1.Location = new System.Drawing.Point(94, 247);
            this.providerChooser1.Name = "providerChooser1";
            this.providerChooser1.Size = new System.Drawing.Size(572, 123);
            this.providerChooser1.TabIndex = 3;
            this.providerChooser1.Load += new System.EventHandler(this.providerChooser1_Load);
            // 
            // ImageHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(754, 493);
            this.Controls.Add(this.panel1);
            this.Name = "ImageHandler";
            this.Text = "ImageHandler";
            this.Load += new System.EventHandler(this.ImageHandler_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Y_Offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X_Offset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cb_FontFamily;
        private System.Windows.Forms.ComboBox cb_FontSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Y_Offset;
        private System.Windows.Forms.NumericUpDown X_Offset;
        private XtraReportsDemos.TreeView.dsHierarchyTableAdapters.HierarchyTableAdapter hierarchyTableAdapter1;
        private DevExpress.DataAccess.UI.Native.ConnectionNameControl connectionNameControl1;
        private DevExpress.DataAccess.UI.Native.ProviderChooser providerChooser1;
    }
}

