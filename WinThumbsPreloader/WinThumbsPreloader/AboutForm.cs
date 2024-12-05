using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinThumbsPreloader.Properties;

namespace WinThumbsPreloader
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += AboutForm_KeyDown;
            this.KeyUp += AboutForm_KeyUp;
            this.Activated += AboutForm_Activated;
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                ExtensionsButton.Text = "Open Folder";
            }
        }

        private void AboutForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                ExtensionsButton.Text = "Extensions";
            }
        }

        private void AboutForm_Activated(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                ExtensionsButton.Text = "Open Folder";
            }
            else
            {
                ExtensionsButton.Text = "Extensions";
            }
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            AppNameLabel.Text += " " + Application.ProductVersion;
            this.Icon = Resources.MainIcon;
            AppIconPictureBox.Image = new Icon(Resources.MainIcon, 48, 48).ToBitmap();
            CheckForUpdates();
        }

        private enum UpdateState
        {
            Updated,
            NotUpdated,
            Error
        }

        private async void CheckForUpdates()
        {
            UpdateState updateState = await Task.Run(async () =>
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("WinThumbPreloader");
                        string GitHubApiResponse = await client.GetStringAsync("https://api.github.com/repos/Mfarooq360/WinThumbsPreloader/releases/latest");
                        string latestVersionString = Regex.Match(GitHubApiResponse, @"""tag_name"":\s*""v([\d\.]+)").Groups[1].Captures[0].ToString();
                        Version currentVersion = new Version(Application.ProductVersion);
                        Version latestVersion = new Version(latestVersionString);
                        return (currentVersion >= latestVersion ? UpdateState.Updated : UpdateState.NotUpdated);
                    }
                }
                catch (Exception)
                {
                    return UpdateState.Error;
                }
            });
            switch (updateState)
            {
                case UpdateState.Updated:
                    UpdateLabel.Text = Resources.AboutForm_WinThumbsPreloader_IsUpToDate;
                    break;
                case UpdateState.Error:
                    UpdateLabel.Text = Resources.AboutForm_WinThumbsPreloader_UpdateCheckFailed;
                    break;
                case UpdateState.NotUpdated:
                    UpdateLabel.Text = Resources.AboutForm_WinThumbsPreloader_NewVersionAvailable;
                    UpdateLabel.ForeColor = Color.FromArgb(0, 102, 204);
                    UpdateLabel.Font = new Font(UpdateLabel.Font.Name, UpdateLabel.Font.SizeInPoints, FontStyle.Underline);
                    UpdateLabel.Cursor = Cursors.Hand;
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LicenceButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "LICENSE.txt");
            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception) { } // Do nothing
        }

        string[] defaultExtensions = DirectoryScanner.defaultExtensions;
        private void ExtensionsButton_Click(object sender, EventArgs e)
        {
            string executablePath = AppContext.BaseDirectory;
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programFilesThumbsPreloaderFolder = Path.Combine(programFilesFolder, "WinThumbsPreloader");
            string programFilesExtensionsPath = Path.Combine(programFilesThumbsPreloaderFolder, "ThumbnailExtensions.txt");
            string programDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WinThumbsPreloader");
            string programDataPath = Path.Combine(programDataFolder, "ThumbnailExtensions.txt");
            string filePath;

            if (executablePath.StartsWith(programFilesFolder, StringComparison.OrdinalIgnoreCase) &&
                executablePath.IndexOf("WinThumbsPreloader", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (!Directory.Exists(programDataFolder))
                {
                    Directory.CreateDirectory(programDataFolder);
                }

                if (File.Exists(programFilesExtensionsPath) && !File.Exists(programDataPath))
                {
                    File.Copy(programFilesExtensionsPath, programDataPath);
                }

                filePath = programDataPath;
            }
            else
            {
                filePath = Path.Combine(executablePath, "ThumbnailExtensions.txt");
            }

            try
            {
                if (ExtensionsButton.Text == "Open Folder")
                {
                    string folderPath = Path.GetDirectoryName(filePath);
                    Process.Start(new ProcessStartInfo(folderPath) { UseShellExecute = true });
                }
                else
                {
                    if (!File.Exists(filePath))
                    {
                        File.WriteAllLines(filePath, defaultExtensions);
                    }

                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Failed to open or create ThumbnailExtensions.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ResetButton_Click(object sender, EventArgs e)
        {
            string executablePath = AppContext.BaseDirectory;
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WinThumbsPreloader");
            string programDataPath = Path.Combine(programDataFolder, "ThumbnailExtensions.txt");

            string filePath;

            if (executablePath.StartsWith(programFilesFolder, StringComparison.OrdinalIgnoreCase) &&
                executablePath.IndexOf("WinThumbsPreloader", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (!Directory.Exists(programDataFolder))
                {
                    Directory.CreateDirectory(programDataFolder);
                }
                filePath = programDataPath;
            }
            else
            {
                filePath = Path.Combine(executablePath, "ThumbnailExtensions.txt");
            }

            try
            {
                File.WriteAllLines(filePath, defaultExtensions);
                MessageBox.Show("ThumbnailExtensions.txt has been reset successfully.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Try restarting the program as Admin.", "Failed to reset ThumbnailExtensions.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void UpdateLabel_Click(object sender, EventArgs e)
        {
            if (UpdateLabel.Text == Resources.AboutForm_WinThumbsPreloader_NewVersionAvailable) Process.Start("https://github.com/Mfarooq360/WinThumbsPreloader");
        }
    }
}
