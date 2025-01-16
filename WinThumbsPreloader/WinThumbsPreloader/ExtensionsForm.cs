using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinThumbsPreloader.Properties;

namespace WinThumbsPreloader
{
    public partial class ExtensionsForm : Form
    {
        public ExtensionsForm()
        {
            InitializeComponent();
            ExtensionsTextBox.DragEnter += new DragEventHandler(ExtensionsTextBox_DragEnter);
            ExtensionsTextBox.DragDrop += new DragEventHandler(ExtensionsTextBox_DragDrop);
        }

        private void ExtensionsForm_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.MainIcon;
            ExtensionsTextBox.Text = Settings.Default.ExtensionsText;
        }

        private void ExtensionsTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                Settings.Default.ExtensionsText = textBox.Text;
                Settings.Default.Save();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            AddUndoableTextChange(ExtensionsTextBox, "");
            Settings.Default.ExtensionsText = "";
            Settings.Default.Save();
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            Settings.Default.ExtensionsText = string.Join(",", DirectoryScanner.defaultExtensions);
            AddUndoableTextChange(ExtensionsTextBox, Settings.Default.ExtensionsText);
            Settings.Default.Save();
        }

        private void AddUndoableTextChange(TextBox textBox, string newText)
        {
            if (textBox.Text == newText)
                return;

            textBox.SelectAll();
            textBox.SelectedText = newText;
            textBox.SelectionStart = textBox.Text.Length;
        }

        private void ExtensionsTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ExtensionsTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files != null && files.Length > 0)
                    {
                        string fileContent = File.ReadAllText(files[0]);
                        ExtensionsTextBox.Text = fileContent.Length > 32767 ? fileContent.Substring(0, 32767) : fileContent;
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    string text = (string)e.Data.GetData(DataFormats.Text);
                    ExtensionsTextBox.Text = text.Length > 32767 ? text.Substring(0, 32767) : text;
                }

                Settings.Default.ExtensionsText = ExtensionsTextBox.Text;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing text: " + ex.Message);
            }
        }

        private async void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(string.Join(",", ExtensionsTextBox.Text
                    .Split(new[] { ',', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(ext => ext.Trim())));

                CopyButton.Text = "Copied!";
                await Task.Delay(3000);
                CopyButton.Text = "CLI Copy";
            }
            catch
            {
                CopyButton.Text = "Copy Failed";
                await Task.Delay(3000);
                CopyButton.Text = "CLI Copy";
            }
        }
    }
}
