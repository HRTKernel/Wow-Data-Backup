using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Globalization;
using SharpConfig;

namespace Wow_Data_Backup
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            

        }

        public void WoWFolder()
        {
            FolderBrowserDialog wow = new FolderBrowserDialog();
            if (wow.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                string[] Ordner = Directory.GetDirectories(wow.SelectedPath);
                string[] Addons = Directory.GetDirectories(wow.SelectedPath + "/Interface/Addons");
                string[] WTF = Directory.GetDirectories(wow.SelectedPath + "/WTF");
                foreach (string OrdnerItem in Ordner)
                {
                    listBox1.Items.Add(Path.GetFileName(OrdnerItem));
                }
                foreach (string AddonItem in Addons)
                {
                    listBox2.Items.Add(Path.GetFileName(AddonItem));

                }
                foreach (string WTFItem in WTF)
                {
                    listBox3.Items.Add(Path.GetFileName(WTFItem));
                }
                button3.Visible = true;
                textBox1.Text = wow.SelectedPath;

            }

        }

        public void BackFolder()
        {
            FolderBrowserDialog backup = new FolderBrowserDialog();
            if (backup.ShowDialog() == DialogResult.OK)
            {
                listBox4.Items.Clear();
                string[] Backup = Directory.GetDirectories(backup.SelectedPath);
                foreach (string BackupItem in Backup)
                {
                    listBox4.Items.Add(Path.GetFileName(BackupItem));
                }
                textBox2.Text = backup.SelectedPath;
                button4.Visible = true;
                button6.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                label13.Visible = true;

            }
        }

        public void LoadConfig()
        {
            var config = Configuration.LoadFromFile("DataConfig.cfg");
            var section = config["General"];
            string wowSelectedPath = section["wowfolder"].StringValue;
            string backupSelectedPath = section["backupfolder"].StringValue;

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            string[] Ordner = Directory.GetDirectories(wowSelectedPath);
            string[] Addons = Directory.GetDirectories(wowSelectedPath + "/Interface/Addons");
            string[] WTF = Directory.GetDirectories(wowSelectedPath + "/WTF");
            foreach (string OrdnerItem in Ordner)
            {
                listBox1.Items.Add(Path.GetFileName(OrdnerItem));
            }
            foreach (string AddonItem in Addons)
            {
                listBox2.Items.Add(Path.GetFileName(AddonItem));

            }
            foreach (string WTFItem in WTF)
            {
                listBox3.Items.Add(Path.GetFileName(WTFItem));
            }
            button3.Visible = true;
            textBox1.Text = wowSelectedPath;

            listBox4.Items.Clear();
            string[] Backup = Directory.GetDirectories(backupSelectedPath);
            foreach (string BackupItem in Backup)
            {
                listBox4.Items.Add(Path.GetFileName(BackupItem));
            }
            textBox2.Text = backupSelectedPath;
            button4.Visible = true;
            button6.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
            label13.Visible = true;
            MessageBox.Show("Einstellungen geladen!");
        }

        public void makebackup()
        {
            DialogResult dialogResult = MessageBox.Show("Backup erstellen?", "Willst du ein Backup erstellen?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                progressBar1.Visible = true;
                string wowfolder = Convert.ToString(textBox1.Text);
                string backupfolder = Convert.ToString(textBox2.Text);
                string datetime = DateTime.Now.ToString("dd-MM-yyyy_HHmm");
                string foldername = "Backup_";
                string backupfoldername = Convert.ToString(foldername + datetime);
                label8.Visible = true;
                label2.Visible = true;
                label2.Text = backupfoldername;
                string pathString = System.IO.Path.Combine(backupfolder, backupfoldername);
                if (checkBox2.Checked)
                {
                    System.IO.Directory.CreateDirectory(pathString);
                    if (System.IO.Directory.Exists(pathString))
                    {
                        progressBar1.Value = 25;
                        label9.Text = "Backup Ordner erstellt";
                        string startPathInterface = wowfolder + "/Interface";
                        string zipPathInterface = pathString + "./Interface.zip";
                        ZipFile.CreateFromDirectory(startPathInterface, zipPathInterface);
                        if (System.IO.File.Exists(zipPathInterface))
                        {
                            progressBar1.Value = 100;
                            label9.Text = "Interface Backup erstellt";
                            MessageBox.Show("Interface Backup erfolgreich erstellt!");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Backup Ordner konnte nicht erstellt werden!"); ;
                    }
                }
                else
                {
                    MessageBox.Show("Interface Backup nicht erstellt!");
                }
                if (checkBox3.Checked)
                {
                    System.IO.Directory.CreateDirectory(pathString);
                    if (System.IO.Directory.Exists(pathString))
                    {
                        progressBar1.Value = 25;
                        label9.Text = "Backup Ordner erstellt";
                        string startPathWTF = wowfolder + "/WTF";
                        string zipPathWTF = pathString + "./WTF.zip";

                        ZipFile.CreateFromDirectory(startPathWTF, zipPathWTF);
                        if (System.IO.File.Exists(zipPathWTF))
                        {
                            progressBar1.Value = 100;
                            label9.Text = "WTF Backup erfolgreich erstellt";
                        }
                        MessageBox.Show("WTF Backup erfolgreich erstellt!");
                    }
                    else
                    {
                        MessageBox.Show("Backup Ordner konnte nicht erstellt werden!"); ;
                    }
                }
                else
                {
                    MessageBox.Show("WTF Backup nicht erstellt!");
                }
            }
            else
            {
                if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Backup nicht erstellt!");
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WoWFolder();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BackFolder();
        }

        private void button4_Click(object sender, EventArgs e)
        {                
                makebackup();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create the configuration.
            var myConfig = new Configuration();
            string wowfolderconfig = Convert.ToString(textBox1.Text);
            string backupfolderconfig = Convert.ToString(textBox2.Text);
            myConfig["General"]["wowfolder"].StringValue = wowfolderconfig;
            myConfig["General"]["backupfolder"].StringValue = backupfolderconfig;
            if (checkBox1.Checked)
            {
                myConfig["General"]["autoload"].BoolValue = true;

            }
            else
            {
                myConfig["General"]["autoload"].BoolValue = false;
            }
            if (checkBox2.Checked)
            {
                myConfig["General"]["backupaddons"].BoolValue = true;
            }
            else
            {
                myConfig["General"]["backupaddons"].BoolValue = false;
            }
            if (checkBox3.Checked)
            {
                myConfig["General"]["backupwtf"].BoolValue = true;
            }
            else
            {
                myConfig["General"]["backupwtf"].BoolValue = false;
            }
            myConfig.SaveToFile("DataConfig.cfg");
            MessageBox.Show("Einstellungen gespeichert!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("DataConfig.cfg"))
            {
                button5.Visible = true;
                label12.Text = "Einstellungen gefunden!";
                var config = Configuration.LoadFromFile("DataConfig.cfg");
                var section = config["General"];
                bool autoload = section["autoload"].BoolValue;
                bool backupaddons = section["backupaddons"].BoolValue;
                bool backupwtf = section["backupwtf"].BoolValue;
                if (autoload == true)
                {
                    LoadConfig();
                    checkBox1.Checked = true;
                }
                else if (autoload == false)
                {
                    checkBox1.Checked = false;
                }
                if (backupaddons == true)
                {
                    checkBox2.Checked = true;
                }
                else if (backupaddons == false)
                {
                    checkBox2.Checked = false;
                }
                if (backupwtf == true)
                {
                    checkBox3.Checked = true;
                }
                else if (backupwtf == false)
                {
                    checkBox3.Checked = false;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
