namespace ImageHandler
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public partial class ImageHandler : Form
    {
        private static string FontName = string.Empty;

        private static string FontSize = string.Empty;

        private static string ProcessImagePath = string.Empty;

        private static Brush SelectedColor = Brushes.Black;

        private static Bitmap SourceBitMap = null;

        FontFamily[] fontFamilies;

        public ImageHandler()
        {
            this.InitializeComponent();
        }

        private void AttachTextToImage(string attachedText, Bitmap sourceBitmap)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            // 设置文件类型  
            sfd.Filter = "*.jpg|*.png";

            // 设置文件名称：
            sfd.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + ".jpg";

            // 设置默认文件类型显示顺序  
            sfd.FilterIndex = 2;

            // 保存对话框是否记忆上次打开的目录  
            sfd.RestoreDirectory = true;

            // 点了保存按钮进入  
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                PointF firstLocation = new PointF(
                    sourceBitmap.Width * ((float)this.X_Offset.Value / 100f),
                    sourceBitmap.Height * ((float)this.Y_Offset.Value / 100f));
                using (Graphics graphics = Graphics.FromImage(sourceBitmap))
                {
                    using (Font font = new Font(
                        (FontFamily)this.cb_FontFamily.SelectedItem,
                        Convert.ToInt32(this.cb_FontSize.SelectedItem)))
                    {
                        graphics.DrawString(this.textBox1.Text, font, SelectedColor, firstLocation);
                    }
                }

                sourceBitmap.Save(sfd.FileName.ToString());
            }
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"Pictures";
            ofd.Filter = "*附加图片|*.jpg;*.png;*.gif";
            ofd.RestoreDirectory = true;
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ProcessImagePath = ofd.FileName;
                this.label2.Text = ProcessImagePath;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ProcessImagePath))
                {
                    SourceBitMap = (Bitmap)Image.FromFile(ProcessImagePath);
                    this.AttachTextToImage(this.textBox1.Text, SourceBitMap);
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("请选择文件路径");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误:{ex.ToString()},请联系管理员或程序开发者.");
            }
        }

        /// <summary>
        /// 预览图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var sourceBitmap = (Bitmap)Image.FromFile(ProcessImagePath);
            PointF firstLocation = new PointF(
                sourceBitmap.Width * ((float)this.X_Offset.Value / 100f),
                sourceBitmap.Height * ((float)this.Y_Offset.Value / 100f));
            using (Graphics graphics = Graphics.FromImage(sourceBitmap))
            {
                using (Font font = new Font(
                    (FontFamily)this.cb_FontFamily.SelectedItem,
                    Convert.ToInt32(this.cb_FontSize.SelectedItem)))
                {
                    graphics.DrawString(this.textBox1.Text, font, SelectedColor, firstLocation);
                }
            }

            this.pictureBox1.Image = sourceBitmap;
        }

        /// <summary>
        /// 选择颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.colorDialog1.ShowDialog();
            SelectedColor = new SolidBrush(this.colorDialog1.Color);
        }

        private void ImageHandler_Load(object sender, EventArgs e)
        {
            this.cb_FontFamily.DisplayMember = "Name";
            this.cb_FontFamily.DataSource = new InstalledFontCollection().Families;
            this.cb_FontFamily.Text = "Tahoma";
            for (int i = 11; i <= 200; i++)
            {
                this.cb_FontSize.Items.Add(i);
            }
        }

        private void connectionNameControl1_Load(object sender, EventArgs e)
        {

        }

        private void providerChooser1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Item
    {
        public string Name { get; set; }

        public FontFamily Value { get; set; }
    }
}