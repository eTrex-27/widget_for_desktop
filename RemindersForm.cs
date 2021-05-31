using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Number_2C
{
    public partial class RemindersForm : Form
    {
        public RemindersForm()
        {
            InitializeComponent();
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);

            bMake.Visible = false;
            bNext.Visible = false;
            textMakeReminder.Visible = false;
            bBack2.Visible = false;
            bBack1.Visible = false;
            dateTimePicker1.Visible = false;
        }

        private void RemindersForm_Load(object sender, EventArgs e)
        {
            
        }

        private void size()
        {
            Size size = new Size();
            size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new System.Drawing.Point(size.Width - this.Size.Width, size.Height - this.Size.Height);
        }


        private void bMakeReminder_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("remindsform2.png");
            this.Size = new Size(300, 100);
            size();
            bNext.Visible = true;
            bBack1.Visible = true;
            dateTimePicker1.Visible = true;

            bMakeReminder.Visible = false;
            bSaveChanges.Visible = false;
            bDel.Visible = false;
            textReminder.Visible = false;
            listReminders.Visible = false;

        }

        string date;

        private void bNext_Click(object sender, EventArgs e)
        {
            
            date = dateTimePicker1.Value.Date.ToShortDateString();
            for (int i = 0; i < listReminders.Items.Count; i++)
            {
                if(date == listReminders.Items[i].ToString())
                {
                    MessageBox.Show("Напоминание с такой датой уже существует. Для изменения напоминания вернитесь на главную страницу.");
                    return;
                }
            }
            this.BackgroundImage = Image.FromFile("remindsform3.png");
            this.Size = new Size(300, 165);
            size();
            bNext.Visible = false;
            bBack1.Visible = false;
            dateTimePicker1.Visible = false;

            bMake.Visible = true;
            textMakeReminder.Visible = true;
            bBack2.Visible = true;
        }

        private void bBack1_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("remindsform1.png");
            this.Size = new Size(370, 265);
            size();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Visible = false;
            bNext.Visible = false;
            bBack1.Visible = false;
            bBack2.Visible = false;

            bMakeReminder.Visible = true;
            bSaveChanges.Visible = true;
            bDel.Visible = true;
            textReminder.Visible = true;
            listReminders.Visible = true;
        }

        private void bBack2_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("remindsform2.png");
            this.Size = new Size(300, 100);
            size();

            bMake.Visible = false;
            textMakeReminder.Visible = false;
            bBack2.Visible = false;

            bNext.Visible = true;
            dateTimePicker1.Visible = true;
            bBack1.Visible = true;
        }

        private void bMake_Click(object sender, EventArgs e)
        {
            if (textMakeReminder.Text == "" || textMakeReminder.Text == " ")
                return;
            else
            {
                this.BackgroundImage = Image.FromFile("remindsform1.png");
                this.Size = new Size(370, 265);
                size();
                dateTimePicker1.Value = DateTime.Now;
                StreamWriter writer = new StreamWriter(@"Reminders\Dates.txt", true, System.Text.Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(date);
                writer.Close();

                StreamWriter writer2 = new StreamWriter(String.Format(@"Reminders\{0}.txt", date), true, System.Text.Encoding.GetEncoding("UTF-8"));
                writer2.WriteLine(textMakeReminder.Text);
                writer2.Close();

                listReminders.Items.Add(date);


                textMakeReminder.Clear();
                bMake.Visible = false;
                textMakeReminder.Visible = false;
                bBack2.Visible = false;

                bMakeReminder.Visible = true;
                bSaveChanges.Visible = true;
                bDel.Visible = true;
                textReminder.Visible = true;
                listReminders.Visible = true;
            }
                

            
        }

        private void listReminders_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (listReminders.SelectedItem == listReminders.Items[listReminders.SelectedIndex])
                {
                    try
                    {
                        textReminder.Clear();
                        StreamReader file = new System.IO.StreamReader(String.Format(@"Reminders\{0}.txt", listReminders.SelectedItem.ToString()), System.Text.Encoding.GetEncoding("UTF-8"));
                        textReminder.Text = file.ReadToEnd();
                        file.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Напоминание не найдено");
                    }
                }
            }
            catch
            {
                return;
            }
            
        }

        private void bSaveChanges_Click(object sender, EventArgs e)
        {
            if (listReminders.Items.Count == 0)
                return;

            try
            {
                if (listReminders.SelectedItem == listReminders.Items[listReminders.SelectedIndex])
                {
                    StreamWriter streamwriter = new System.IO.StreamWriter(String.Format(@"Reminders\{0}.txt", listReminders.SelectedItem.ToString()), false, System.Text.Encoding.GetEncoding("UTF-8"));
                    streamwriter.WriteLine(textReminder.Text);
                    streamwriter.Close();
                }
            }
            catch
            {
                return;
            }
            
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            if (listReminders.Items.Count == 0)
                return;

            System.IO.File.Delete(String.Format(@"Reminders\{0}.txt", listReminders.SelectedItem.ToString()));
            listReminders.Items.Remove(listReminders.SelectedItem);
            if (listReminders.Items.Count != 0)
                listReminders.SetSelected(0, true);
            StreamWriter deldate = new System.IO.StreamWriter(@"Reminders\Dates.txt", false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (var item in listReminders.Items)
            {
                deldate.WriteLine(item.ToString());
            }
            deldate.Close();
            textReminder.Clear();
        }

        private void textMakeReminder_TextChanged(object sender, EventArgs e)
        {
            textMakeReminder.MaxLength = 34;
        }

        private void textReminder_TextChanged(object sender, EventArgs e)
        {
            textReminder.MaxLength = 34;
        }

        private void RemindersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
