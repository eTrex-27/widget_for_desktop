using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Number_2C
{
    public partial class TextForm : Form
    {
        
        public TextForm()
        {
            InitializeComponent();
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
            label2.Visible = false;
        }

        private void load()
        {
            List<string> list1 = new List<string>();
            listBox1.Items.Clear();
            foreach (string o in System.IO.File.ReadAllLines(@"Text\FilesText.txt", System.Text.Encoding.GetEncoding("UTF-8")))
            {
                list1.Add(o);
            }
            foreach (var txt in list1)
            {
                listBox1.Items.Add(txt);
            }

            if (listBox1.Items.Count != 0)
            {
                listBox1.SetSelected(0, true);
                lists();
            }

            System.IO.StreamReader file = new System.IO.StreamReader(@"Text\textsettings.txt", System.Text.Encoding.GetEncoding("UTF-8"));

            string comb;

            comb = file.ReadLine();


            switch (comb)
            {
                case "0": comboBox1.Text = "8"; break;
                case "1": comboBox1.Text = "10"; break;
                case "2": comboBox1.Text = "11"; break;
                case "3": comboBox1.Text = "12"; break;
                case "4": comboBox1.Text = "16"; break;
                case "5": comboBox1.Text = "24"; break;
                case "6": comboBox1.Text = "32"; break;
                default:
                    if (file.ReadToEnd() == null)
                        return;
                    break;
            }


            string combcolor;

            combcolor = file.ReadLine();

            switch (combcolor)
            {
                case "0": comboBox3.Text = "Чёрный"; break;
                case "1": comboBox3.Text = "Синий"; break;
                case "2": comboBox3.Text = "Белый"; break;
                case "3": comboBox3.Text = "Жёлтый"; break;
                case "4": comboBox3.Text = "Зелёный"; break;
                default:
                    if (file.ReadToEnd() == null)
                        return;
                    break;
            }

            file.Close();
        }


        private void lists()
        {
            if (listBox1.Items.Count == 0)
            {
                return;
            }
            try
            {
                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    try
                    {
                        richTextBox1.Clear();
                        System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Text\{0}.txt", listBox1.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
                        richTextBox1.Text = file.ReadToEnd();
                        
                        file.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Файл не найден");
                    }
                }
            }
            catch
            {
                return;
            }
        }


        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            lists();
        }


        private void isklload()
        {
            try
            {
                load();
            }
            catch
            {
                StreamWriter streamwriter2 = new System.IO.StreamWriter(@"Text\textsettings.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                streamwriter2.Close();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            isklload();
        }

        private void save()
        {
            if (listBox1.Items.Count == 0)
                return;

            try
            {
                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    StreamWriter streamwriter = new System.IO.StreamWriter(String.Format(@"Text\{0}.txt", listBox1.SelectedItem.ToString()), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    streamwriter.WriteLine(this.richTextBox1.Text);
                    StreamWriter streamwriter2 = new System.IO.StreamWriter(@"Text\textsettings.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    streamwriter2.WriteLine(this.comboBox1.SelectedIndex);
                    streamwriter2.WriteLine(this.comboBox3.SelectedIndex);
                    streamwriter.Close();
                    streamwriter2.Close();

                    timer1.Interval = 3000;
                    timer1.Start();
                    label2.Visible = true;
                    timer1.Tick += new EventHandler(timer1_Tick);
                }
            }
            catch
            {
                MessageBox.Show("Нажмите на файл для сохранения");
                return;
            }
        }


        
        private void bAddFile_Click(object sender, EventArgs e)
        {
            if (AddTextFile.Text == "" || AddTextFile.Text == " ")
                return;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == AddTextFile.Text)
                {
                    MessageBox.Show("Такой файл уже есть");
                    AddTextFile.Text = null;
                    return;
                }
            }

            listBox1.Items.Add(AddTextFile.Text);
            StreamWriter addfile = new System.IO.StreamWriter(@"Text\FilesText.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
            addfile.WriteLine(AddTextFile.Text);
            addfile.Close();
            StreamWriter addfilel = new System.IO.StreamWriter(String.Format(@"Text\{0}.txt", AddTextFile.Text), true, System.Text.Encoding.GetEncoding("UTF-8"));
            addfilel.Close();

            AddTextFile.Text = null;
        }

        private void bDelFile_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;

            System.IO.File.Delete(String.Format(@"Text\{0}.txt", listBox1.SelectedItem.ToString()));
            listBox1.Items.Remove(listBox1.SelectedItem);
            if (listBox1.Items.Count != 0)
                listBox1.SetSelected(0, true);
            StreamWriter delfile = new System.IO.StreamWriter(@"Text\FilesText.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (var item in listBox1.Items)
            {
                delfile.WriteLine(item.ToString());
            }
            delfile.Close();
            richTextBox1.Clear();
        }



        private void bRenameFile_Click(object sender, EventArgs e)
        {
            if (RenameFile.Text == "" || RenameFile.Text == " ")
                return;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == RenameFile.Text)
                {
                    MessageBox.Show("Такой файл уже есть");
                    RenameFile.Text = null;
                    return;
                }
            }


            string ren = "";
            ren = RenameFile.Text;
            try
            {
                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    StreamWriter makefile = new System.IO.StreamWriter(String.Format(@"Text\{0}.txt", ren), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in richTextBox1.Text)
                    {
                        makefile.Write(item);
                    }
                    makefile.Close();
                }

                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    System.IO.File.Delete(String.Format(@"Text\{0}.txt", listBox1.SelectedItem.ToString()));
                    listBox1.Items[listBox1.SelectedIndex] = RenameFile.Text;
                }

                StreamWriter renfile = new System.IO.StreamWriter(@"Text\FilesText.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                foreach (var item in listBox1.Items)
                {
                    renfile.WriteLine(item.ToString());
                }
                renfile.Close();
                RenameFile.Text = null;
            }
            catch
            {
                RenameFile.Text = null;
                return;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void clear()
        {
            richTextBox1.Text = "";
        }

        int a = 8;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (comboBox1.SelectedIndex)
            {
                case 0: a = 8;  break;
                case 1: a = 10; break;
                case 2: a = 11; break;
                case 3: a = 12; break;
                case 4: a = 16; break;
                case 5: a = 24; break;
                case 6: a = 32; break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }
            Font font1 = new Font(comboBox1.Text, a);
            richTextBox1.Font = font1;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox2.SelectedIndex)
            {
                case 0: save(); break;
                case 1: clear(); break;
                default: break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = Color.White;
            switch(comboBox3.SelectedIndex)
            {
                case 0: color = Color.FromArgb(0, 0, 0); break;
                case 1: color = Color.FromArgb(0, 0, 255); break;
                case 2: color = Color.FromArgb(255, 255, 255); break;
                case 3: color = Color.FromArgb(255, 215, 0); break;
                case 4: color = Color.FromArgb(0, 255, 0); break;
                default:
                    MessageBox.Show("Default case");
                    break;
            }
            richTextBox1.ForeColor = color;
        }

        private void AddTextFile_TextChanged(object sender, EventArgs e)
        {
            AddTextFile.MaxLength = 14;
        }

        private void RenameFile_TextChanged(object sender, EventArgs e)
        {
            RenameFile.MaxLength = 14;
        }

        private void bupfile_Click(object sender, EventArgs e)
        {
            object k;
            try
            {
                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    k = listBox1.Items[listBox1.SelectedIndex];
                    listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex - 1];
                    listBox1.Items[listBox1.SelectedIndex - 1] = k;
                    listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Text\FilesText.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in listBox1.Items)
                    {
                        rewriter.WriteLine(item.ToString());
                    }
                    rewriter.Close();
                }
            }
            catch
            {
                return;
            }
        }

        private void bdownfile_Click(object sender, EventArgs e)
        {
            object k;
            try
            {
                if (listBox1.SelectedItem == listBox1.Items[listBox1.SelectedIndex])
                {
                    k = listBox1.Items[listBox1.SelectedIndex];
                    listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex + 1];
                    listBox1.Items[listBox1.SelectedIndex + 1] = k;
                    listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Text\FilesText.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in listBox1.Items)
                    {
                        rewriter.WriteLine(item.ToString());
                    }
                    rewriter.Close();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
