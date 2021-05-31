using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Number_2C
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
            
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
            openFileDialog1.Filter = "Image files(*.png)|*.png|Image files(*.jpg)|*.jpg|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            label1.Visible = false;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            List<string> list1 = new List<string>();
            listBox1.Items.Clear();
            foreach (string o in System.IO.File.ReadAllLines(@"Image\Albums.txt", System.Text.Encoding.GetEncoding("UTF-8")))
            {
                list1.Add(o);
            }
            foreach (var md in list1)
            {
                listBox2.Items.Add(md);
            }

            if (listBox2.Items.Count != 0)
            {
                listBox2.SetSelected(0, true);
                lists();
            }     
        }

        private void lists()
        {
            if (listBox2.Items.Count == 0)
            {
                return;
            }
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    try
                    {
                        listBox1.Items.Clear();
                        System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
                        while (!file.EndOfStream)
                        {
                            MediaItem item = new MediaItem();
                            item.Load(file);
                            listBox1.Items.Add(item);
                        }
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

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            lists();
            tSearch.Text = "";
        }


        private void bAddAlbumImage_Click(object sender, EventArgs e)
        {
            if (AddTextAlbumImage.Text == "" || AddTextAlbumImage.Text == " ")
                return;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == AddTextAlbumImage.Text)
                {
                    MessageBox.Show("Такой альбом уже есть");
                    AddTextAlbumImage.Text = null;
                    return;
                }
            }

            listBox2.Items.Add(AddTextAlbumImage.Text);
            StreamWriter addalb = new System.IO.StreamWriter(@"Image\Albums.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
            addalb.WriteLine(AddTextAlbumImage.Text);
            addalb.Close();
            StreamWriter addplayl = new System.IO.StreamWriter(String.Format(@"Image\{0}.txt", AddTextAlbumImage.Text), true, System.Text.Encoding.GetEncoding("UTF-8"));
            addplayl.Close();

            AddTextAlbumImage.Text = null;
        }

        private void bDelAlbumImage_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
                return;

            System.IO.File.Delete(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()));
            listBox2.Items.Remove(listBox2.SelectedItem);
            if (listBox2.Items.Count != 0)
                listBox2.SetSelected(0, true);
            StreamWriter delalbum = new System.IO.StreamWriter(@"Image\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (var item in listBox2.Items)
            {
                delalbum.WriteLine(item.ToString());
            }
            delalbum.Close();
        }

        private void bRenameAlbumImage_Click(object sender, EventArgs e)
        {
            if (RenameAlbumImage.Text == "" || RenameAlbumImage.Text == " ")
                return;


            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == RenameAlbumImage.Text)
                {
                    MessageBox.Show("Такой альбом уже есть");
                    RenameAlbumImage.Text = null;
                    return;
                }
            }


            string ren = "";
            ren = RenameAlbumImage.Text;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter makealbum = new System.IO.StreamWriter(String.Format(@"Image\{0}.txt", ren), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (MediaItem item in listBox1.Items)
                    {
                        item.Save(makealbum);
                    }
                    makealbum.Close();
                }

                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    System.IO.File.Delete(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()));
                    listBox2.Items[listBox2.SelectedIndex] = RenameAlbumImage.Text;
                }

                StreamWriter renalbum = new System.IO.StreamWriter(@"Image\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                foreach (var item in listBox2.Items)
                {
                    renalbum.WriteLine(item.ToString());
                }
                renalbum.Close();
                RenameAlbumImage.Text = null;
            }
            catch
            {
                RenameAlbumImage.Text = null;
                return;
            }
        }


        private void clicklist()
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            ((ImageForm2)this.Owner).Visible = true;
            MediaItem item = (MediaItem)listBox1.SelectedItem;

            if (File.Exists(item.path))
            {
                ((ImageForm2)this.Owner).pictureBox1.Image = Image.FromFile(item.path);
            }
            else
            {
                MessageBox.Show(String.Format("Файл {0} не найден", item.path));
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            clicklist();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            MediaItem item = (MediaItem)listBox1.SelectedItem;

            if (File.Exists(item.path))
            {
                Process.Start(item.path);
            }
            else
            {
                MessageBox.Show(String.Format("Файл {0} не найден", item.path));
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void bOpenImages_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter openmedia = new System.IO.StreamWriter(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()), true, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var md in openFileDialog1.FileNames)
                    {
                        openmedia.WriteLine(md);
                    }
                    openmedia.Close();

                    listBox1.Items.Clear();
                    System.IO.StreamReader addmediaalbum = new System.IO.StreamReader(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
                    while (!addmediaalbum.EndOfStream)
                    {
                        MediaItem item = new MediaItem();
                        item.Load(addmediaalbum);
                        listBox1.Items.Add(item);
                    }
                    addmediaalbum.Close();
                }
            }
            catch
            {
                MessageBox.Show("Создайте альбом для добавления");
                return;
            }
            
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter streamwriter = new System.IO.StreamWriter(String.Format(@"Image\{0}.txt", listBox2.SelectedItem.ToString()), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (MediaItem item in listBox1.Items)
                    {
                        item.Save(streamwriter);
                    }
                    streamwriter.Close();

                    timer1.Interval = 3000;
                    timer1.Start();
                    label1.Visible = true;
                }
            }
            catch
            {
                MessageBox.Show("Нажмите на альбом для сохранения");
                return;
            }   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void ImageForm_Click(object sender, EventArgs e)
        {
            ((ImageForm2)this.Owner).Visible = false;
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            LinkedList<MediaItem> selectedMediaItems = new LinkedList<MediaItem>();
            foreach (int index in listBox1.SelectedIndices)
            {
                selectedMediaItems.AddLast((MediaItem)listBox1.Items[index]);
            }

            foreach (MediaItem selectedItem in selectedMediaItems)
            {
                listBox1.Items.Remove(selectedItem);
            }
            ((ImageForm2)this.Owner).Visible = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ((ImageForm2)this.Owner).Size = new Size(trackBar1.Value, trackBar1.Value);
        }

        private void AddTextAlbumImage_TextChanged(object sender, EventArgs e)
        {
            AddTextAlbumImage.MaxLength = 14;
        }

        private void RenameAlbumImage_TextChanged(object sender, EventArgs e)
        {
            RenameAlbumImage.MaxLength = 14;
        }

        private void tSearch_TextChanged(object sender, EventArgs e)
        {
            lists();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(tSearch.Text))
                {
                }
                else
                    listBox1.Items.RemoveAt(i--);

            }
            if (tSearch.Text == "")
            {
                lists();
            }
        }

        private void bupalb_Click(object sender, EventArgs e)
        {
            object z;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    z = listBox2.Items[listBox2.SelectedIndex];
                    listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex - 1];
                    listBox2.Items[listBox2.SelectedIndex - 1] = z;
                    listBox2.SetSelected(listBox2.SelectedIndex - 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Image\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in listBox2.Items)
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

        private void bdownalb_Click(object sender, EventArgs e)
        {
            object z;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    z = listBox2.Items[listBox2.SelectedIndex];
                    listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex + 1];
                    listBox2.Items[listBox2.SelectedIndex + 1] = z;
                    listBox2.SetSelected(listBox2.SelectedIndex + 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Image\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var item in listBox2.Items)
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
