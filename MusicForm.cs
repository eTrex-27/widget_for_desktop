using System;
using WMPLib;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Number_2C
{
    public partial class MusicForm : Form
    {
        WindowsMediaPlayer wmp;
        int trackInfo = 0;
        bool left = true;
        bool pausePlay;
        int sel;
        bool coll = false;
        public MusicForm()
        {
            
            InitializeComponent();
            
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
            openFileDialog1.Filter = "MP3 files(*.mp3)|*.mp3|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            label1.Visible = false;
            label2.Text = "";
            wmp = new WindowsMediaPlayer();
            LenghtMus.Enabled = false;
            bPause.Enabled = false;
            bStop.Enabled = false;
            Next.Enabled = false;
            Back.Enabled = false;
            ChekRandom.Enabled = false;
            wmp.PlayStateChange += wmplayer_PlayStateChange;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            List<string> list1 = new List<string>();
            listBox1.Items.Clear();
            foreach (string o in System.IO.File.ReadAllLines(@"Music\Albums.txt", System.Text.Encoding.GetEncoding("UTF-8")))
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
                        System.IO.StreamReader file = new System.IO.StreamReader(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
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
                wmp.URL = itemm.path;
                wmp.controls.stop();
                wmp.controls.play();
                LenghtMus.Enabled = true;
                pausePlay = true;
                timer2.Enabled = true;
                timer2.Interval = 1000;
                sel = listBox1.SelectedIndex;
                bPause.Enabled = true;
                bStop.Enabled = true;
                Next.Enabled = true;
                Back.Enabled = true;
                ChekRandom.Enabled = true;
                remalb = listBox2.SelectedIndex;
            }
            else
            {
                MessageBox.Show(String.Format("Файл {0} не найден", itemm.name));
            }
        }

        public void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            play();
        }

        public void bAddAlbum_Click(object sender, EventArgs e)
        {
            if (AddTextAlbum.Text == "" || AddTextAlbum.Text == " ")
                return;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if(listBox2.Items[i].ToString() == AddTextAlbum.Text)
                {
                    MessageBox.Show("Такой альбом уже есть");
                    AddTextAlbum.Text = null;
                    return;
                }
            }

            listBox2.Items.Add(AddTextAlbum.Text);
            StreamWriter addalb = new System.IO.StreamWriter(@"Music\Albums.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
            addalb.WriteLine(AddTextAlbum.Text);
            addalb.Close();
            StreamWriter addplayl = new System.IO.StreamWriter(String.Format(@"Music\{0}.txt", AddTextAlbum.Text), true, System.Text.Encoding.GetEncoding("UTF-8"));
            addplayl.Close();

            AddTextAlbum.Text = null;
        }

        private void bDelAlbum_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
                return;

            System.IO.File.Delete(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()));
            listBox2.Items.Remove(listBox2.SelectedItem);
            listBox1.Items.Clear();
            if (listBox2.Items.Count != 0)
                listBox2.SetSelected(0, true);
            StreamWriter delalbum = new System.IO.StreamWriter(@"Music\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (var item in listBox2.Items)
            {
                delalbum.WriteLine(item.ToString());
            }
            delalbum.Close();
            Stop();
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
                    StreamWriter makealbum = new System.IO.StreamWriter(String.Format(@"Music\{0}.txt", ren), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (MediaItem item in listBox1.Items)
                    {
                        item.Save(makealbum);
                    }
                    makealbum.Close();
                }

                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    System.IO.File.Delete(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()));
                    listBox2.Items[listBox2.SelectedIndex] = RenameAlbum.Text;
                }

                StreamWriter renalbum = new System.IO.StreamWriter(@"Music\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
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
                if (ChekRandom.Checked == true)
                {
                    listBox1.ClearSelected();
                    Random rnd = new Random();
                    listBox1.SelectedIndex = rnd.Next(0, listBox1.Items.Count);
                }
                else
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
            }
            catch
            {
                listBox1.SetSelected(0, true);
            }

            play();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            listBox2.SetSelected(remalb, true);
            try
            {
                if (ChekRandom.Checked == true)
                {
                    listBox1.ClearSelected();
                    Random rnd = new Random();
                    listBox1.SelectedIndex = rnd.Next(0, listBox1.Items.Count);
                }
                else
                {
                    listBox1.ClearSelected();
                    if (listBox1.SelectedIndex != listBox1.Items.Count - 1)
                    {
                        listBox1.SetSelected(sel + 1, true);
                    }
                    else
                    {
                        listBox1.SetSelected(sel - listBox1.Items.Count + 1, true);
                    }
                }
            }
            catch
            {
                listBox1.SetSelected(0, true);
            }

            play();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            listBox2.SetSelected(remalb, true);
            try
            {
                if (ChekRandom.Checked == true)
                {
                    listBox1.ClearSelected();
                    Random rnd = new Random();
                    listBox1.SelectedIndex = rnd.Next(0, listBox1.Items.Count);
                }
                else
                {
                    listBox1.ClearSelected();
                    if (listBox1.SelectedIndex != 0)
                    {
                        listBox1.SetSelected(sel - 1, true);
                    }
                    else
                    {
                        listBox1.SetSelected(sel + listBox1.Items.Count - 1, true);
                    }
                }
            }
            catch
            {
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }

            play();
        }


        private void diseneble()
        {
            bPause.Enabled = false;
            bStop.Enabled = false;
            Next.Enabled = false;
            Back.Enabled = false;
            ChekRandom.Enabled = false;
        }

        private void Stop()
        {
            wmp.controls.stop();
            LenghtMus.Enabled = false;
            LenghtMus.Value = 0;
            timer2.Enabled = false;
            label2.Text = "";
            pausePlay = false;
            label3.Text = "0:00:00";
            label4.Text = "0:00:00";
            diseneble();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            Stop();
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

            wmp.controls.stop();
            timer2.Enabled = false;
            label2.Text = "";
            label3.Text = "0:00:00";
            label4.Text = "0:00:00";
            LenghtMus.Value = 0;
            LenghtMus.Enabled = false;
            diseneble();
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            wmp.controls.stop();
            timer2.Enabled = false;
            label2.Text = "";
            label3.Text = "0:00:00";
            label4.Text = "0:00:00";
            LenghtMus.Value = 0;
            LenghtMus.Enabled = false;
            diseneble();
        }

        private void bPause_Click(object sender, EventArgs e)
        {
            if (pausePlay == false)
            {
                bPause.Text = "Пауза";
            }
            else
            {
                if (bPause.Text == "Пауза" || bPause.Text == "▐ ▌")
                {

                    wmp.controls.pause();
                    if(coll == false)
                    {
                        bPause.Text = "Next";
                    }
                    else
                    {
                        bPause.Text = "▶";
                    }
                }
                else
                {
                    wmp.controls.play();
                    if(coll == false)
                    {
                        bPause.Text = "Пауза";
                    }
                    else
                    {
                        bPause.Text = "▐ ▌";
                    }
                }
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                if (listBox2.SelectedItem == listBox2.Items[listBox2.SelectedIndex])
                {
                    StreamWriter openmedia = new System.IO.StreamWriter(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()), true, System.Text.Encoding.GetEncoding("UTF-8"));
                    foreach (var md in openFileDialog1.FileNames)
                    {
                        openmedia.WriteLine(md);
                    }
                    openmedia.Close();

                    listBox1.Items.Clear();
                    System.IO.StreamReader addmediaalbum = new System.IO.StreamReader(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
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
                    StreamWriter streamwriter = new System.IO.StreamWriter(String.Format(@"Music\{0}.txt", listBox2.SelectedItem.ToString()), false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void LenghtMus_Scroll(object sender, EventArgs e)
        {
            wmp.controls.currentPosition = LenghtMus.Value;
        }

        private void Volume_Scroll(object sender, EventArgs e)
        {
            if (Volume.Value == 10)
                wmp.settings.volume = 100;
            if (Volume.Value == 9)
                wmp.settings.volume = 90;
            if (Volume.Value == 8)
                wmp.settings.volume = 80;
            if (Volume.Value == 7)
                wmp.settings.volume = 70;
            if (Volume.Value == 6)
                wmp.settings.volume = 60;
            if (Volume.Value == 5)
                wmp.settings.volume = 50;
            if (Volume.Value == 4)
                wmp.settings.volume = 40;
            if (Volume.Value == 3)
                wmp.settings.volume = 30;
            if (Volume.Value == 2)
                wmp.settings.volume = 20;
            if (Volume.Value == 1)
                wmp.settings.volume = 10;
            if (Volume.Value == 0)
                wmp.settings.volume = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                LenghtMus.Maximum = Convert.ToInt32(wmp.currentMedia.duration);
                LenghtMus.Value = Convert.ToInt32(wmp.controls.currentPosition);
            }
            catch
            {
                wmp.controls.stop();
            }
            

            if (wmp != null)
            {
                int s = (int)wmp.currentMedia.duration;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                label3.Text = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, s);

                s = (int)wmp.controls.currentPosition;
                h = s / 3600;
                m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);
                label4.Text = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, s);
            }
            else
            {
                label3.Text = "0:00:00";
                label4.Text = "0:00:00";
            }

            if (trackInfo <= itemm.name.Length)
            {
                timer2.Interval = 200;
                if(bPause.Text == "Пауза")
                {
                    if (left)
                        label2.Text = "Играет: " + itemm.name.Substring(trackInfo, itemm.name.Length - trackInfo);
                    else
                        label2.Text = "Играет: " + itemm.name.Substring(itemm.name.Length - trackInfo, trackInfo);
                }
                else
                {
                    if (left)
                        label2.Text = "Пауза: " + itemm.name.Substring(trackInfo, itemm.name.Length - trackInfo);
                    else
                        label2.Text = "Пауза: " + itemm.name.Substring(itemm.name.Length - trackInfo, trackInfo);
                }
                
                trackInfo++;
            }
            else
            {
                trackInfo = 0;
                if (left) left = false;
                else left = true;
            }

            
                       
        }
        public void wmplayer_PlayStateChange(int NewState)
        {
            if (wmp.playState == WMPPlayState.wmppsMediaEnded)
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

        private void bUp_Click(object sender, EventArgs e)
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
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Music\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void bDown_Click(object sender, EventArgs e)
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
                    StreamWriter rewriter = new System.IO.StreamWriter(@"Music\Albums.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
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

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (listBox2.SelectedItem == listBox2.SelectedValue)
                return;
            lists();
            tSearch.Text = "";
        }

        private void size()
        {
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
        }

        private void collapseform_Click(object sender, EventArgs e)
        {
            if (coll == true)
            {
                this.Size = new Size(556, 440);
                size();
                this.BackgroundImage = Image.FromFile("MusicForm.png");
                TopMost = false;
                collapseform.BackgroundImage = Image.FromFile("collapse1.png");
                listBox1.Visible = true;
                listBox2.Visible = true;
                bAddAlbum.Visible = true;
                bDelAlbum.Visible = true;
                bRenameAlbum.Visible = true;
                AddTextAlbum.Visible = true;
                bClear.Visible = true;
                bDown.Visible = true;
                bOpen.Visible = true;
                bRemove.Visible = true;
                bSave.Visible = true;
                bUp.Visible = true;
                label10.Visible = true;
                label5.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                RenameAlbum.Visible = true;
                tSearch.Visible = true;
                collapseform.Location = new Point(509, 9);
                Volume.Orientation = Orientation.Vertical;
                Volume.TickStyle = TickStyle.Both;
                Volume.Location = new Point(353, 54);
                label6.Location = new Point(335, 55);
                label7.Location = new Point(340, 210);
                bPause.Location = new Point(345, 254);
                bPause.Size = new Size(57, 25);
                if(bPause.Text == "▶")
                {
                    bPause.Text = "Next";
                }
                if(bPause.Text == "▐ ▌")
                {
                    bPause.Text = "Пауза";
                }
                bStop.Location = new Point(345, 285);
                bStop.Size = new Size(57, 25);
                bStop.Text = "Стоп";
                Next.Location = new Point(345, 316);
                Next.Size = new Size(57, 25);
                Next.TextAlign = ContentAlignment.MiddleCenter;
                Next.Text = "Вперёд";
                Next.Font = new Font("Microsoft Sans Serif.ttf", 8);
                Back.Location = new Point(345, 347);
                Back.TextAlign = ContentAlignment.MiddleCenter;
                Back.Size = new Size(57, 25);
                Back.Text = "Назад";
                Back.Font = new Font("Microsoft Sans Serif.ttf", 8);
                labelRandom.Location = new Point(347, 377);
                ChekRandom.Location = new Point(366, 399);
                LenghtMus.Location = new Point(8, 320);
                LenghtMus.Size = new Size(326, 45);
                label3.Location = new Point(283, 349);
                label4.Location = new Point(18, 349);
                label2.Location = new Point(99, 18);
                label2.Size = new Size(257, 13);
                coll = false;
            }
            else
            {
                this.Size = new Size(480, 100);
                size();
                this.BackgroundImage = Image.FromFile("MusicForm2.png");
                TopMost = true;
                collapseform.BackgroundImage = Image.FromFile("collapse2.png");
                listBox1.Visible = false;
                listBox2.Visible = false;
                bAddAlbum.Visible = false;
                bDelAlbum.Visible = false;
                bRenameAlbum.Visible = false;
                AddTextAlbum.Visible = false;
                bClear.Visible = false;
                bDown.Visible = false;
                bOpen.Visible = false;
                bRemove.Visible = false;
                bSave.Visible = false;
                bUp.Visible = false;
                label10.Visible = false;
                label5.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                RenameAlbum.Visible = false;
                tSearch.Visible = false;
                collapseform.Location = new Point(432, 54);
                Volume.Orientation = Orientation.Horizontal;
                Volume.TickStyle = TickStyle.TopLeft;
                Volume.Location = new Point(275, 8);
                label6.Location = new Point(450, 17);
                label7.Location = new Point(267, 14);
                bPause.Location = new Point(13, 64);
                bPause.Size = new Size(43, 25);
                if (bPause.Text == "Next")
                {
                    bPause.Text = "▶";
                }
                if (bPause.Text == "Пауза")
                {
                    bPause.Text = "▐ ▌";
                }
                bStop.Location = new Point(56, 64);
                bStop.Size = new Size(43, 25);
                bStop.Text = "■";
                Next.Location = new Point(56, 39);
                Next.Size = new Size(43, 25);
                Next.Text = "→";
                Next.TextAlign = ContentAlignment.TopCenter;
                Next.Font = new Font("Arial.ttf", 12, FontStyle.Bold);
                Back.Location = new Point(13, 39);
                Back.Size = new Size(43, 25);
                Back.Text = "←";
                Back.TextAlign = ContentAlignment.TopCenter;
                Back.Font = new Font("Arial.ttf", 12, FontStyle.Bold);
                labelRandom.Location = new Point(377, 49);
                ChekRandom.Location = new Point(398, 73);
                LenghtMus.Location = new Point(102, 39);
                LenghtMus.Size = new Size(269, 45);
                label3.Location = new Point(320, 71);
                label4.Location = new Point(112, 71);
                label2.Location = new Point(17, 19);
                label2.Size = new Size(246, 16);
                coll = true;
            }
        }
    }
}