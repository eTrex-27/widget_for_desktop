using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Number_2C
{
    public partial class PaintForm : Form
    {
        bool Drawing = false;
        int sizew = 10;
        int sizeh = 10;
        Graphics _graphics;
        PointF oldPoint = new PointF(0, 0);
        Pen _pen;
        Bitmap bitmap;

        public PaintForm()
        {
            InitializeComponent();
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
            _pen = new Pen(Color.White);

            bitmap = new Bitmap(paintBox.Size.Width, paintBox.Size.Height);

            _graphics = Graphics.FromImage(bitmap);
            paintBox.Image = bitmap;
        }

        private void PaintForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StreamReader file = new StreamReader(@"Paint\paintlist.txt", Encoding.GetEncoding("UTF-8"));
            while (!file.EndOfStream)
            {
                MediaItem item = new MediaItem();
                item.Load(file);
                listBox1.Items.Add(item);
            }
            file.Close();
            _graphics.Clear(Color.White);
        }

        private void PaintForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void paintBox_MouseDown(object sender, MouseEventArgs e)
        {
            Drawing = true;
        }

        private void paintBox_MouseUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
        }

        private void paintBox_MouseMove(object sender, MouseEventArgs e)
        {
            float dx = (e.X - oldPoint.X);
            float dy = (e.Y - oldPoint.Y);
            int count = (int)Math.Sqrt(dx * dx + dy * dy);
            if (Drawing == true)
            {
                if (count > 0)
                {
                    for (int i = 0; i < count + 1; i++)
                    {
                        _graphics.FillEllipse(_pen.Brush, oldPoint.X + dx / count * i, oldPoint.Y + dy / count * i, sizew, sizeh);
                    }
                }
                
            }
            oldPoint = new PointF(e.X, e.Y);
            paintBox.Image = bitmap;
        }


        private void cbPenColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = Color.White;

            switch (cbPenColor.SelectedIndex)
            {
                case 0: color = Color.FromArgb(255, 255, 255); break;
                case 1: color = Color.FromArgb(0, 0, 0); break;
                case 2: color = Color.FromArgb(0, 0, 255); break;
                case 3: color = Color.FromArgb(0, 255, 0); break;
                case 4: color = Color.FromArgb(255, 0, 0); break;
            }
            _pen = new Pen(color);
        }

        private void cbPenSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbPenSize.SelectedIndex)
            {
                case 0: sizew = 5; sizeh = 5; break;
                case 1: sizew = 6; sizeh = 6; break;
                case 2: sizew = 8; sizeh = 8; break;
                case 3: sizew = 10; sizeh = 10; break;
                case 4: sizew = 12; sizeh = 12; break;
                case 5: sizew = 14; sizeh = 14; break;
            }
        }

        private void cbFill_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = Color.White;

            switch (cbFill.SelectedIndex)
            {
                case 0: color = Color.White; break;
                case 1: color = Color.Black; break;
                case 2: color = Color.Blue; break;
                case 3: color = Color.Green; break;
                case 4: color = Color.Red; break;
            }

            _graphics.Clear(color);
            paintBox.Image = bitmap;

        }

        private void bClear_Click(object sender, EventArgs e)
        {
            _graphics.Clear(Color.White);
            pictureBox2.Image = null;
            cbFill.Text = null;
            cbPenSize.Text = null;
            cbPenColor.Text = null;
            sizew = 10;
            sizeh = 10;
            paintBox.Image = bitmap;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            
            if (SaveZam.Text == "" || SaveZam.Text == " ")
                return;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == (SaveZam.Text + ".jpg"))
                {
                    MessageBox.Show("Заметка уже существует");
                    SaveZam.Text = null;
                    return;
                }
            }

            MediaItem item = new MediaItem(String.Format(@"Paint\{0}.jpg", SaveZam.Text));
            
            bitmap.Save(String.Format(@"Paint\{0}.jpg", SaveZam.Text), System.Drawing.Imaging.ImageFormat.Jpeg);

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.Save();
            }

            listBox1.Items.Add(item);
            StreamWriter streamwriter = new System.IO.StreamWriter(@"Paint\paintlist.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
            item.Save(streamwriter);
            streamwriter.Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            MediaItem item = (MediaItem)listBox1.SelectedItem;

            if (File.Exists(item.path))
            {
                using (var fs = File.OpenRead(item.path))
                using (var img = Image.FromStream(fs))
                {
                    pictureBox2.Image = new Bitmap(img);
                }
            }
            else
            {
                MessageBox.Show(String.Format("Файл {0} не найден", item.path));
            }
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;

            MediaItem item = (MediaItem)listBox1.SelectedItem;
            pictureBox2.Image = null;
            listBox1.Items.Remove(item);

            System.IO.File.Delete(item.path);
            StreamWriter streamwriter = new StreamWriter(@"Paint\paintlist.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (MediaItem item2 in listBox1.Items)
            {
                item2.Save(streamwriter);
            }
            streamwriter.Close();
        }

        private void SaveZam_TextChanged(object sender, EventArgs e)
        {
            SaveZam.MaxLength = 14;
        }
    }
}
