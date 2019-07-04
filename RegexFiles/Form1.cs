using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexFiles
{
    public partial class RegexFiles : Form
    {
        private const Int32 BufferSize = 128;
        private Dictionary<String, StreamReader> SearchItemsFiles = new Dictionary<String, StreamReader>();
        private Dictionary<String, StreamReader> ItemsToSearchFiles = new Dictionary<String, StreamReader>();

        public RegexFiles()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            int progressBarText = 0;
            Random rnd = new Random();
            dataGridView1.Rows.Clear();

            if (SearchItemsFiles.Count == 0 || ItemsToSearchFiles.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No files selected", "Error");
                return;
            }

            label3.Text = "Testing 1 lines...";
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 3000;
            this.Refresh();

            foreach (KeyValuePair<string, StreamReader> entry in SearchItemsFiles)
            {
                StreamReader sr = entry.Value;
                String originLine;
                while ((originLine = sr.ReadLine()) != null)
                {
                    progressBarText += 1;
                    Regex originLineRegex = new Regex(originLine);

                    foreach (KeyValuePair<string, StreamReader> entry2 in ItemsToSearchFiles)
                    {
                        StreamReader sr2 = entry2.Value;
                        String coincidenceLine;

                        if (checkBox1.Checked == true)
                        {
                            while ((coincidenceLine = sr2.ReadLine()) != null)
                            {
                                if (originLineRegex.Match(coincidenceLine).Success)
                                {
                                    this.dataGridView1.Rows.Add(originLine, entry.Key, coincidenceLine, entry2.Key);
                                    this.dataGridView1.Update();
                                    this.Refresh();
                                }
                            }
                        }
                        else
                        {
                            while ((coincidenceLine = sr2.ReadLine()) != null)
                            {
                                if (coincidenceLine.Contains(originLine))
                                {
                                    this.dataGridView1.Rows.Add(originLine, entry.Key, coincidenceLine, entry2.Key);
                                    this.dataGridView1.Update();
                                    this.Refresh();
                                }
                            }
                        }
                        sr2.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                    }
                    if (rnd.Next(1, 31) == 1)
                    {
                        label3.Text = "Testing " + progressBarText.ToString() + " lines...";
                        this.Refresh();
                    }
                }
                sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
            label3.Text = "COMPLETED, " + progressBarText.ToString() + " lines have been tested";
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            SearchItemsFiles = new Dictionary<String, StreamReader>();
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string file in Directory.EnumerateFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories))
                    {
                        FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        SearchItemsFiles.Add(Path.GetFileName(file), new StreamReader(fs, Encoding.UTF8, true, BufferSize));
                    }
                    label1.Text = fbd.SelectedPath + " - Files: " + SearchItemsFiles.Count.ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            ItemsToSearchFiles = new Dictionary<String, StreamReader>();
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string file in Directory.EnumerateFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories))
                    {
                        FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        ItemsToSearchFiles.Add(Path.GetFileName(file), new StreamReader(fs, Encoding.UTF8, true, BufferSize));
                    }
                    label2.Text = fbd.SelectedPath + " - Files: " + ItemsToSearchFiles.Count.ToString();
                }
            }
        }
    }
}