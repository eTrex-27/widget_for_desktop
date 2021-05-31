using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Number_2C
{
    public partial class TopForm : Form
    {
        
        public Point mouseOffset;
        int iFormX, iFormY, iMouseX, iMouseY;
        SettingsForm form2 = new SettingsForm();
        MusicForm form3 = new MusicForm();
        ImageForm form4 = new ImageForm();
        TextForm form5 = new TextForm();
        ImageForm2 form4_1 = new ImageForm2();
        AboutBox1 infoform = new AboutBox1();
        PaintForm painttext = new PaintForm();
        ImageForm3 imageback = new ImageForm3();
        CollapseForm collapse = new CollapseForm();
        VideoForm videof = new VideoForm();
        RemindersForm reminds = new RemindersForm();
        public TopForm()
        {
            InitializeComponent();
            form2.Owner = this;
            form4.Owner = form4_1;
            form4_1.Owner = imageback;
            collapse.Owner = this;
            reminds.Owner = this;
            label1.BackColor = Color.FromArgb(161, 161, 161);
            label1.Font = new Font("Arial.ttf", 11);
            label2.BackColor = Color.FromArgb(161, 161, 161);
            label2.Font = new Font("Arial.ttf", 11);
            label3.BackColor = Color.FromArgb(161, 161, 161);
            label3.Font = new Font("Arial.ttf", 11);
            label4.BackColor = Color.FromArgb(161, 161, 161);
            label4.Font = new Font("Arial.ttf", 11);
            label5.BackColor = Color.FromArgb(161, 161, 161);
            label5.Font = new Font("Arial.ttf", 11);
            label5.Text = "  Draw \nзаметки";
            label7.ForeColor = Color.FromArgb(0, 0, 255);
            label7.Text = "Сегодня нет никаких событий";
            label8.ForeColor = Color.FromArgb(255, 77, 0);
            label8.Text = "Завтра нет никаких событий";

            // labels //

            label1.MouseEnter += pictureBox1_MouseEnter;
            label1.MouseLeave += pictureBox1_MouseLeave;
            label1.MouseClick += pictureBox1_MouseClick;

            label2.MouseEnter += pictureBox2_MouseEnter;
            label2.MouseLeave += pictureBox2_MouseLeave;
            label2.MouseClick += pictureBox2_MouseClick;

            label3.MouseEnter += pictureBox3_MouseEnter;
            label3.MouseLeave += pictureBox3_MouseLeave;
            label3.MouseClick += pictureBox3_MouseClick;

            label4.MouseEnter += pictureBox4_MouseEnter;
            label4.MouseLeave += pictureBox4_MouseLeave;
            label4.MouseClick += pictureBox4_MouseClick;

            label5.MouseEnter += pictureBox5_MouseEnter;
            label5.MouseLeave += pictureBox5_MouseLeave;
            label5.MouseClick += pictureBox5_MouseClick;

            label7.MouseDown += Form1_MouseDown;
            label7.MouseMove += Form1_MouseMove;
            label7.MouseUp += Form1_MouseUp;

            label8.MouseDown += Form1_MouseDown;
            label8.MouseMove += Form1_MouseMove;
            label8.MouseUp += Form1_MouseUp;


            ///////////
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamWriter streamwriter = new StreamWriter(@"D:\\textsettings.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            streamwriter.Close();


            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            pictureBox1.Image = Image.FromFile("img1a.png");
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.Image = Image.FromFile("img2a.png");
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.Image = Image.FromFile("img3a.png");
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.Image = Image.FromFile("img4a.png");
            pictureBox4.BackColor = Color.Transparent;
            pictureBox5.Image = Image.FromFile("img5a.png");
            pictureBox5.BackColor = Color.Transparent;

            form2.Hide();
            form3.Hide();
            form4.Hide();
            form5.Hide();
            form4_1.Visible = false;
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            collapse.Hide();

            string a;
            
            System.IO.StreamReader filesettings = new System.IO.StreamReader(@"textsettings.txt", System.Text.Encoding.GetEncoding("UTF-8"));
            a = filesettings.ReadLine();

            Color color = Color.White;

            switch (a)
            {
                case "Красный": 
                    color = Color.FromArgb(220, 0, 0);
                    break;

                case "Зелёный":
                    color = Color.FromArgb(13, 165, 0);
                    break;

                case "Чёрный": 
                    color = Color.FromArgb(0, 0, 0);
                    break;

                case "Фиолетовый": 
                    color = Color.FromArgb(168, 0, 217);
                    break;

                case "Синий": 
                    color = Color.FromArgb(0, 0, 255);
                    break;

                default:
                    MessageBox.Show("Default case");
                    break;
            }

            form2.comboBox1.Text = a;

            label1.ForeColor = color;
            label2.ForeColor = color;
            label3.ForeColor = color;
            label4.ForeColor = color;
            label5.ForeColor = color;
            label6.ForeColor = color;

            string b;
            b = filesettings.ReadLine();

            int width = Screen.GetWorkingArea(this).Width;
            int height = Screen.GetWorkingArea(this).Height;

            //collapse.Location = new Point(width - 140, 0);

            switch (b)
            {
                case "Левый верхний угол":
                    Settings.Location = new Point(0, 0);
                    Settings.DisableMooving = true;
                    form2.comboBox2.Text = b;
                    break;

                case "Правый верхний угол":
                    Settings.Location = new Point(width - 340, 0);
                    Settings.DisableMooving = true;
                    form2.comboBox2.Text = b;
                    break;

                case "По центру":
                    Settings.Location = new Point(width / 2 - this.Size.Width / 2, height / 2 - this.Size.Height / 2);
                    Settings.DisableMooving = true;
                    form2.comboBox2.Text = b;
                    break;

                case "Отвязать":
                    Settings.Location = new Point(width / 2 - this.Size.Width / 2, height / 2 - this.Size.Height / 2);
                    Settings.DisableMooving = false;
                    form2.comboBox2.Text = b;
                    break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }

            string c;
            c = filesettings.ReadLine();
            switch(c)
            {
                case "Draw заметки": label5.Text = "  Draw \nзаметки"; form2.comboBox3.Text = c; break;
                case "Видео": label5.Text = "  Видео"; label5.Location = new Point(220, 245); form2.comboBox3.Text = c; break;
                case "Напоминания": label5.Text = "Напоми-\n нания"; label5.Location = new Point(224, 238); form2.comboBox3.Text = c; break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }

            filesettings.Close();

            this.Location = Settings.Location;

            List<string> list1 = new List<string>();
            reminds.listReminders.Items.Clear();
            foreach (string o in System.IO.File.ReadAllLines(@"Reminders\Dates.txt", System.Text.Encoding.GetEncoding("UTF-8")))
            {
                list1.Add(o);
            }
            foreach (var dat in list1)
            {
                reminds.listReminders.Items.Add(dat);
            }

            if (reminds.listReminders.Items.Count != 0)
            {
                reminds.listReminders.SetSelected(0, true);
            }

            string dateString;
            string dateString2;

            for(int z = 0; z < reminds.listReminders.Items.Count; z++)
            {
                dateString = reminds.listReminders.Items[z].ToString();
                DateTime date1 = DateTime.Parse(dateString);
                DateTime date2 = DateTime.Now.Date.AddDays(+1);

                if (date2.ToShortDateString() == date1.ToShortDateString())
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Reminders\{0}.txt", reminds.listReminders.Items[z].ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
                    label8.Text = "Завтра: " + file.ReadToEnd();
                    file.Close();
                }
            }

            for (int y = 0; y < reminds.listReminders.Items.Count; y++)
            {
                dateString2 = reminds.listReminders.Items[y].ToString();
                DateTime datedel = DateTime.Parse(dateString2);
                if (datedel < DateTime.Now.Date)
                {
                    System.IO.File.Delete(String.Format(@"Reminders\{0}.txt", reminds.listReminders.Items[y]));
                    reminds.listReminders.Items.Remove(reminds.listReminders.Items[y]);
                    if (reminds.listReminders.Items.Count != 0)
                        reminds.listReminders.SetSelected(0, true);
                    System.IO.StreamWriter deldate = new System.IO.StreamWriter(@"Reminders\Dates.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in reminds.listReminders.Items)
                    {
                        deldate.WriteLine(item.ToString());
                    }
                    deldate.Close();
                }
            }

            for (int i = 0; i < reminds.listReminders.Items.Count; i++)
            {
                if (reminds.listReminders.Items[i].ToString() == DateTime.Now.Date.ToShortDateString())
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Reminders\{0}.txt", DateTime.Now.Date.ToShortDateString()), System.Text.Encoding.GetEncoding("UTF-8"));
                    label7.Text = "Сегодня: " + file.ReadToEnd();
                    file.Close();
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////*** Forma ***///////////////////////////////////////////////

        public void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Settings.DisableMooving)
                return;

            iFormX = this.Location.X;
            iFormY = this.Location.Y;
            iMouseX = MousePosition.X;
            iMouseY = MousePosition.Y;
            this.Opacity = 0.5;
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(contextMenuStrip1.Location = new Point(iMouseX, iMouseY));
                this.Opacity = 1;
            }
            
        }

        public void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Settings.DisableMooving)
                return;

            int iMouseX2 = MousePosition.X;
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(iFormX + (iMouseX2 - iMouseX), iFormY + (iMouseY2 - iMouseY));
        }

        public void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Settings.DisableMooving)
                return;

            this.Opacity = 1;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }


        ////////////////////////////////////////////////////////////////////////////*** Pictures ***///////////////////////////////////////////////


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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox1.Image = Image.FromFile("img1c.png");
            label1.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox1.Image = Image.FromFile("img1b.png");
            label1.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Image.FromFile("img2b.png");
            label2.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Image.FromFile("img2a.png");
            label2.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox2.Image = Image.FromFile("img2c.png");
            label2.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox2.Image = Image.FromFile("img2b.png");
            label2.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Image.FromFile("img3b.png");
            label3.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Image.FromFile("img3a.png");
            label3.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox3.Image = Image.FromFile("img3c.png");
            label3.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox3.Image = Image.FromFile("img3b.png");
            label3.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile("img4b.png");
            label4.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile("img4a.png");
            label4.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox4.Image = Image.FromFile("img4c.png");
            label4.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox4.Image = Image.FromFile("img4b.png");
            label4.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Image.FromFile("img5b.png");
            label5.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Image.FromFile("img5a.png");
            label5.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox5.Image = Image.FromFile("img5c.png");
            label5.BackColor = Color.FromArgb(161, 161, 161);
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            pictureBox5.Image = Image.FromFile("img5b.png");
            label5.BackColor = Color.FromArgb(210, 203, 0);
        }

        private void vp1()
        {
            form5.Hide();
            form4.Hide();
            form4_1.Visible = false;
            form3.Hide();
            form2.Show();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp2()
        {
            form2.Hide();
            form3.Show();
            form4.Hide();
            form4_1.Visible = false;
            form5.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp3()
        {
            form5.Hide();
            form4.Show();
            form4_1.Visible = false;
            form2.Hide();
            form3.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp4()
        {
            form5.Show();
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form4_1.Visible = false;
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp5()
        {
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form4_1.Visible = false;
            form5.Hide();
            infoform.Hide();
            painttext.Show();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp6()
        {
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form4_1.Visible = false;
            form5.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Show();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void vp7()
        {
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form4_1.Visible = false;
            form5.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Show();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                return;
            }
            vp1();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            vp2();
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            vp3();
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            vp4();
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            switch (label5.Text)
            {
                case "  Draw \nзаметки": vp5(); break;
                case "  Видео": vp6(); break;
                case "Напоми-\n нания": vp7(); break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form5.Hide();
            form4_1.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
        }

        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть?", "Внимание!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            infoform.Show();
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            infoform.Hide();
        }

        private void TopForm_LocationChanged(object sender, EventArgs e)
        {
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            if (this.Location.X < 0)
            {
                this.Location = new Point(0, this.Location.Y);
            }
            if (this.Location.Y < 0)
            {
                this.Location = new Point(this.Location.X, 0);
            }
            if (this.Location.X + this.Size.Width > size.Width)
            {
                this.Location = new Point(size.Width - this.Size.Width, this.Location.Y);
            }
            if (this.Location.Y + this.Size.Height > size.Height)
            {
                this.Location = new Point(this.Location.X, size.Height - this.Size.Height);
            }
        }

        private void label1_ForeColorChanged(object sender, EventArgs e)
        {
            collapse.label1.ForeColor = label1.ForeColor;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            vp2();
        }

        private void картинкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp3();
        }

        private void заметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp4();
        }

        private void drawЗаметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp5();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp1();
        }

        private void видеоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp6();
        }

        private void напоминанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vp7();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void свернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.Hide();
            form3.Hide();
            form4.Hide();
            form4_1.Visible = false;
            form5.Hide();
            infoform.Hide();
            painttext.Hide();
            imageback.Hide();
            videof.Hide();
            reminds.Hide();
            this.Hide();
            collapse.Show();
            collapse.Location = collapse.formXY;
        }
    }
}
