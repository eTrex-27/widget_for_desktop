using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Number_2C
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);

            label1.Font = new Font("Segoe Script", (int)11.5, FontStyle.Italic);
            label2.Font = new Font("Segoe Script", (int)11.5, FontStyle.Italic);
            label4.Font = new Font("Segoe Script", (int)11.5, FontStyle.Italic);
            label3.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = Color.White;
            switch (comboBox1.SelectedIndex)
            {
                case 0: color = Color.FromArgb(220, 0, 0);
                        break;

                case 1: color = Color.FromArgb(13, 165, 0);
                        break;

                case 2: color = Color.FromArgb(0, 0, 0);
                        break;

                case 3: color = Color.FromArgb(168, 0, 217);
                        break;

                case 4: color = Color.FromArgb(0, 0, 255);
                        break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }

            ((TopForm)this.Owner).label1.ForeColor = color;
            ((TopForm)this.Owner).label2.ForeColor = color;
            ((TopForm)this.Owner).label3.ForeColor = color;
            ((TopForm)this.Owner).label4.ForeColor = color;
            ((TopForm)this.Owner).label5.ForeColor = color;
            ((TopForm)this.Owner).label6.ForeColor = color;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int width = Screen.GetWorkingArea(this).Width;
            int height = Screen.GetWorkingArea(this).Height;
            switch (comboBox2.SelectedIndex)
            {
                case 0: 
                    Settings.Location = new Point(0, 0);
                    Settings.DisableMooving = true;
                    break;

                case 1: 
                     Settings.Location = new Point(width - 340, 0);
                     Settings.DisableMooving = true;
                     break;

                case 2:
                     Settings.Location = new Point(width / 2 - ((TopForm)this.Owner).Size.Width / 2, height / 2 - ((TopForm)this.Owner).Size.Height / 2);
                     Settings.DisableMooving = true;
                     break;
                    

                case 3:
                     Settings.Location = new Point(width / 2 - ((TopForm)this.Owner).Size.Width / 2, height / 2 - ((TopForm)this.Owner).Size.Height / 2);
                     Settings.DisableMooving = false;
                     break;
            }

            this.Owner.Location = Settings.Location;
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*switch(comboBox3.SelectedIndex)
            {
                case 0: ((TopForm)this.Owner).label5.Text = "  Draw \nзаметки"; break;
                case 1: ((TopForm)this.Owner).label5.Text = "Видео"; break;
                case 2: ((TopForm)this.Owner).label5.Text = "Напоминания"; break;
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter streamwriter = new System.IO.StreamWriter(@"textsettings.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            string a = comboBox1.SelectedItem.ToString();
            string b = comboBox2.SelectedItem.ToString();
            string c = comboBox3.SelectedItem.ToString();
            streamwriter.WriteLine(a);
            streamwriter.WriteLine(b);
            streamwriter.WriteLine(c);
            switch (c)
            {
                case "Draw заметки": ((TopForm)this.Owner).label5.Text = "  Draw \nзаметки"; break;
                case "Видео": ((TopForm)this.Owner).label5.Text = "  Видео"; ((TopForm)this.Owner).label5.Location = new Point(220, 245); break;
                case "Напоминания": ((TopForm)this.Owner).label5.Text = "Напоми-\n нания"; ((TopForm)this.Owner).label5.Location = new Point(224, 238); break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }
            streamwriter.Close();

            timer1.Interval = 3000;
            timer1.Start();
            label3.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        
    }
}
