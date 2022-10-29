using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoogleDrive;
using System.IO;

namespace Background
{
    public partial class FormGoogleDrive : Form
    {
        public FormGoogleDrive()
        {
            InitializeComponent();
            Contenttypes = new Dictionary<string, string>();
            Contenttypes.Add("txt", "text / plain");
            Contenttypes.Add("html", "text / html");
            Contenttypes.Add("zip", "application/zip");
            Contenttypes.Add("rar", "application/rar");
            Contenttypes.Add("pdf", "application/pdf");
            Contenttypes.Add("docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            Contenttypes.Add("doc", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            Contenttypes.Add("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Contenttypes.Add("xls", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Contenttypes.Add("csv", "text/csv");
            Contenttypes.Add("jpg", "image/jpg");
            Contenttypes.Add("jpeg", "image/jpeg");
            Contenttypes.Add("png", "image/png");
            Contenttypes.Add("pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation ");
            Contenttypes.Add("ppt", "application/vnd.openxmlformats-officedocument.presentationml.presentation ");
            Contenttypes.Add("json", "application/vnd.google-apps.script+json");
        }

        private Dictionary<string, string> Contenttypes;
        private Dictionary<string, string> listdateien;

        private void FormGoogleDrive_Load(object sender, EventArgs e)
        {
            ClassGoogleDrive.InitClassGoogleDrive();
            if (ClassGoogleDrive.IsLogged())
            {
                listdateien = ClassGoogleDrive.GetFiles(checkBoxgelöschteanzeigen.Checked);

                treeView1.Nodes.Clear();
                treeView1.BeginUpdate();
                treeView1.Nodes.Add("BackGround");
                foreach (KeyValuePair<string, string> pair in listdateien)
                    treeView1.Nodes[0].Nodes.Add(pair.Value);
                treeView1.EndUpdate();
                this.Text = "GoogleDrive [Verbunden]";
                buttonupload.Enabled = true;
            }
            else
            {
                this.Text = "GoogleDrive [Nicht verbunden]";
                buttonupload.Enabled = false;
            }     
            
        }

        private void buttonneuverbinden_Click(object sender, EventArgs e)
        {
            FormGoogleDrive_Load(sender, e);
        }

        private void checkBoxgelöschteanzeigen_CheckedChanged(object sender, EventArgs e)
        {
            FormGoogleDrive_Load(sender, e);
        }

        private void buttonupload_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                string dateiname = openFileDialog1.FileName.Split('\\').Last();
                if (Contenttypes.ContainsKey(dateiname.Split('.').Last()))
                {
                    ClassGoogleDrive.UploadFile(openFileDialog1.FileName.Split('\\').Last(), openFileDialog1.FileName, Contenttypes[dateiname.Split('.').Last()]);
                    label2.Visible = true;
                }
                else
                    MessageBox.Show("Dieser Dateityp wird bisher nicht unterstützt!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            if (Directory.Exists(textBoxdownloadpfad.Text))
            {
                int index = treeView1.SelectedNode.Index;
                string key = listdateien.ElementAt(index).Key;

                ClassGoogleDrive.DownloadFile(key, textBoxdownloadpfad.Text + listdateien.ElementAt(index).Value);
                label3.Visible = true;
            }
            else
            {
                MessageBox.Show("Der Downloadpfad ist ungültig!");
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string text = treeView1.SelectedNode.Text;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            buttondownload.Visible = true;
        }
    }
}
