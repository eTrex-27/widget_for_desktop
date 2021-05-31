using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WMPLib;

namespace Number_2C
{
    public partial class VideoForm : Form
    {
        int sel;
        public VideoForm()
        {
            InitializeComponent();
            int width = Screen.GetWorkingArea(this).Width;
            int height = Screen.GetWorkingArea(this).Height;
            this.Location = new Point(width / 2 - this.Size.Width / 2, height / 2 - this.Size.Height / 2);
            openFileDialog1.Filter = "Video files(*.avi)|*.avi|Video files(*.mp4)|*.mp4|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            axWindowsMediaPlayer1.enableContextMenu = false;
            label1.BackColor = Color.FromArgb(0, 132, 255);
            label1.ForeColor = Color.Gold;
            label1.Visible = false;
            listBox1.BackColor = Color.FromArgb(0, 132, 255);
            listBox1.ForeColor = Color.Gold;
            listBox2.BackColor = Color.FromArgb(0, 132, 255);
            listBox2.ForeColor = Color.Gold;
            label8.BackColor = Color.FromArgb(0, 132, 255);
            label8.ForeColor = Color.Gold;
            label9.BackColor = Color.FromArgb(0, 132, 255);
            label9.ForeColor = Color.Gold;
            label2.BackColor = Color.FromArgb(0, 132, 255);
            label2.ForeColor = Color.Gold;
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            List<string> list1 = new List<string>();
            listBox1.Items.Clear();
            foreach (string o in System.IO.File.ReadAllLines(@"Video\Albums.txt", System.Text.Encoding.GetEncoding("UTF-8")))
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
                        System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
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

        MediaItem itemm;
        int remalb;
        private void play()
        {
            itemm = (MediaItem)listBox1.SelectedItem;
            if (System.IO.File.Exists(itemm.path))
            {
                axWindowsMediaPlayer1.URL = itemm.path;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                axWindowsMediaPlayer1.Ctlcontrols.play();
                sel = listBox1.SelectedIndex;
                remalb = listBox2.SelectedIndex;
            }
            else
            {
                MessageBox.Show(String.Format("Файл {0} не найден", itemm.name));
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            play();
        }

        private void bAddAlbum_Click(object sender, EventArgs e)
        {
            if (AddTextAlbum.Text == "" || AddTextAlbum.Text == " ")
                return;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == AddTextAlbum.Text)
                {
                    MessageBox.Show("Такой альбом уже есть");
                    AddTextAlbum.Text = null;
                    return;
                }
            }

            listBox2.Items.Add(AddTextAlbum.Text);
            StreamWriter addalb = new System.IO.StreamWriter(@"Video\Albums.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
            addalb.WriteLine(AddTextAlbum.Text);
            addalb.Close();
            StreamWriter addplayl = new System.IO.StreamWriter(String.Format(@"Video\{0}.txt", AddTextAlbum.Text), true, System.Text.Encoding.GetEncoding("UTF-8"));
            addplayl.Close();

            AddTextAlbum.Text = null;
        }

        private void bDelAlbum_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
                return;

            System.IO.File.Delete(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()));
            listBox2.Items.Remove(listBox2.SelectedItem);
            if (listBox2.Items.Count != 0)
                listBox2.SetSelected(0, true);
            StreamWriter delalbum = new System.IO.StreamWriter(@"Video\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (var item in listBox2.Items)
            {
                delalbum.WriteLine(item.ToString());
            }
            delalbum.Close();
        }

        private void bRenameAlbum_Click(object sender, EventArgs e)
        {
            if (RenameAlbum.Text == "" || RenameAlbum.Text == " ")
                return;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == RenameAlbum.Text)
                {
                    MessageBox.Show("Такой альбом уже есть");
                    RenameAlbum.Text = null;
                    return;
                }
            }

            string ren = "";
            ren = RenameAlbum.Text;

            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter makealbum = new System.IO.StreamWriter(String.Format(@"Video\{0}.txt", ren), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (MediaItem item in listBox1.Items)
                    {
                        item.Save(makealbum);
                    }
                    makealbum.Close();
                }

                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    System.IO.File.Delete(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()));
                    listBox2.Items[listBox2.SelectedIndex] = RenameAlbum.Text;
                }

                StreamWriter renalbum = new System.IO.StreamWriter(@"Video\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
                foreach (var item in listBox2.Items)
                {
                    renalbum.WriteLine(item.ToString());
                }
                renalbum.Close();
                RenameAlbum.Text = null;
            }
            catch
            {
                RenameAlbum.Text = null;
                return;
            }
        }

        int nlist;
        private void playNext()
        {
            listBox2.SetSelected(remalb, true);
            lists();
            try
            {
                listBox1.ClearSelected();
                if (listBox1.SelectedIndex != listBox1.Items.Count - 1)
                {
                    listBox1.SetSelected(nlist + 1, true);

                }
                else
                {
                    listBox1.SetSelected(nlist - listBox1.Items.Count + 1, true);
                }
            }
            catch
            {
                listBox1.SetSelected(0, true);
            }

            play();
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

            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter openmedia = new System.IO.StreamWriter(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()), true, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var md in openFileDialog1.FileNames)
                    {
                        openmedia.WriteLine(md);
                    }
                    openmedia.Close();

                    listBox1.Items.Clear();
                    System.IO.StreamReader addmediaalbum = new System.IO.StreamReader(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void bClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter streamwriter = new System.IO.StreamWriter(String.Format(@"Video\{0}.txt", listBox2.SelectedItem.ToString()), false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsMediaEnded)
            {
                Thread workerThread = new Thread(DoWork);
                workerThread.Start();
                nlist = sel;
            }
        }

        private void DoWork()
        {
            Thread.Sleep(500);
            this.Invoke(new MethodInvoker(() => { playNext(); }));
        }

        private void AddTextAlbum_TextChanged(object sender, EventArgs e)
        {
            AddTextAlbum.MaxLength = 14;
        }

        private void RenameAlbum_TextChanged(object sender, EventArgs e)
        {
            RenameAlbum.MaxLength = 14;
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

        private void bupv_Click(object sender, EventArgs e)
        {
            object k;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    k = listBox2.Items[listBox2.SelectedIndex];
                    listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex - 1];
                    listBox2.Items[listBox2.SelectedIndex - 1] = k;
                    listBox2.SetSelected(listBox2.SelectedIndex - 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Video\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void bdownv_Click(object sender, EventArgs e)
        {
            object k;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    k = listBox2.Items[listBox2.SelectedIndex];
                    listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex + 1];
                    listBox2.Items[listBox2.SelectedIndex + 1] = k;
                    listBox2.SetSelected(listBox2.SelectedIndex + 1, true);
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Video\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
