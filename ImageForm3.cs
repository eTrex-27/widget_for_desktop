using System;
using System.Drawing;
using System.Windows.Forms;

namespace Number_2C
{
    public partial class ImageForm3 : Form
    {
        public ImageForm3()
        {
            InitializeComponent();
            Size size = new Size();
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Size = new Size(size.Width, size.Height);
            
        }

        private void ImageForm3_Load(object sender, EventArgs e)
        {

        }

        private void ImageForm3_Click(object sender, EventArgs e)
        {

        }

        private void ImageForm3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
