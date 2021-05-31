using System;
using System.Drawing;
using System.Windows.Forms;

namespace Number_2C
{
    public partial class CollapseForm : Form
    {
        public Point formXY;
        public Point mouseOffset;
        int iFormX, iFormY, iMouseY;
        public CollapseForm()
        {
            InitializeComponent();
            this.TopMost = true;
            label1.MouseEnter += pictureBox1_MouseEnter;
            label1.MouseLeave += pictureBox1_MouseLeave;
            label1.MouseClick += pictureBox1_Click;
            label1.BackColor = Color.FromArgb(161, 161, 161);
            pictureBox1.MouseDown += CollapseForm_MouseDown;
            pictureBox1.MouseUp += CollapseForm_MouseUp;
            pictureBox1.MouseMove += CollapseForm_MouseMove;
        }

        private void CollapseForm_Load(object sender, EventArgs e)
        {
            int width = Screen.GetWorkingArea(this).Width;
            formXY = new Point(width - 140, 0);
        }

        private void CollapseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img1c.png");
            label1.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img1b.png");
            label1.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img1b.png");
            label1.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("img1a.png");
            label1.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ((TopForm)this.Owner).Show();
        }

        private void CollapseForm_LocationChanged(object sender, EventArgs e)
        {
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            if (this.Location.Y < 0)
            {
                this.Location = new Point(this.Location.X, 0);
            }
            if (this.Location.Y + this.Size.Height > size.Height)
            {
                this.Location = new Point(this.Location.X, size.Height - this.Size.Height);
            }
        }

        private void CollapseForm_MouseDown(object sender, MouseEventArgs e)
        {
            iFormY = this.Location.Y;
            iFormX = this.Location.X;
            iMouseY = MousePosition.Y;
            this.Opacity = 0.5;
        }

        private void CollapseForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
            int width = Screen.GetWorkingArea(this).Width;
            formXY = new Point(width - 140, this.Location.Y);
        }

        private void CollapseForm_MouseMove(object sender, MouseEventArgs e)
        {
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(iFormX, iFormY + (iMouseY2 - iMouseY));
        }
    }
}
