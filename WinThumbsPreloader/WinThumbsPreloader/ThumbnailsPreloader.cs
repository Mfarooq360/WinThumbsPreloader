using System;
using System.Windows.Forms;
using System.IO;
using WinThumbsPreloader.Properties;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace WinThumbsPreloader
{
    public enum ThumbnailsPreloaderState
    {
        New,
        GettingNumberOfItems,
        Processing,
        Canceled,
        Done
    }

    // Preload all thumbnails, show progress dialog
    class ThumbnailsPreloader
    {
        private DirectoryScanner directoryScanner;
        private ProgressDialog progressDialog;
        private Timer progressDialogUpdateTimer;

        public ThumbnailsPreloaderState state = ThumbnailsPreloaderState.GettingNumberOfItems;
        public ThumbnailsPreloaderState prevState = ThumbnailsPreloaderState.New;
        public bool multiThreaded;
        public int totalItemsCount = 0;
        public int processedItemsCount = 0;
        public string currentFile = "";
        private int threadCount;
        private List<int> thumbnailSizes;

        public ThumbnailsPreloader(string path, bool includeNestedDirectories, bool silentMode, bool multiThreaded, List<string> extensions, int threadCount = 0, List<int> thumbnailSizes = null)
        {
            // Set the process priority to Below Normal to prevent system unresponsiveness
            using (Process p = Process.GetCurrentProcess())
                p.PriorityClass = ProcessPriorityClass.BelowNormal;

            this.thumbnailSizes = thumbnailSizes ?? new List<int> { 256 }; // Default to 256 if no sizes provided

            // Single file mode for when passing a file through the command line
            FileAttributes fAt = File.GetAttributes(path);
            if (!fAt.HasFlag(FileAttributes.Directory)) // The path being passed is a file and not a directory, so we can simply just preload the thumbnail for it
            {
                ThumbnailPreloader.PreloadThumbnail(path, thumbnailSizes); // Default size if none provided
                Environment.Exit(0); // The program can now exit since the file's thumbnail has been preloaded
            }

            this.threadCount = threadCount > 0 ? threadCount : Environment.ProcessorCount; // Use specified or default thread count

            // Normal mode for when passing a directory through the command line
            directoryScanner = new DirectoryScanner(path, includeNestedDirectories, multiThreaded, extensions, this.threadCount);
            if (!silentMode)
            {
                InitProgressDialog();
                InitProgressDialogUpdateTimer();
            }
            this.multiThreaded = multiThreaded;
            Run();
        }

        private void InitProgressDialog()
        {
            progressDialog = new ProgressDialog();
            progressDialog.AutoClose = false;
            progressDialog.ShowTimeRemaining = false;
            progressDialog.Title = "WinThumbsPreloader";
            progressDialog.CancelMessage = Resources.ThumbnailsPreloader_CancelMessage;
            progressDialog.Maximum = 100;
            progressDialog.Value = 0;
            progressDialog.Show();
            UpdateProgressDialog(null, null);
        }

        private void InitProgressDialogUpdateTimer()
        {
            progressDialogUpdateTimer = new Timer();
            progressDialogUpdateTimer.Interval = 250;
            progressDialogUpdateTimer.Tick += new EventHandler(UpdateProgressDialog);
            progressDialogUpdateTimer.Start();
        }

        private void UpdateProgressDialog(object sender, EventArgs e)
        {
            if (progressDialog.HasUserCancelled)
            {
                state = ThumbnailsPreloaderState.Canceled;
                directoryScanner.cancelled = true;
                progressDialog.Close();
                progressDialog?.Dispose();
                progressDialogUpdateTimer.Stop();
                progressDialogUpdateTimer?.Dispose();
                return;
            }
            else if (state == ThumbnailsPreloaderState.GettingNumberOfItems)
            {
                if (prevState != state)
                {
                    prevState = state;
                    progressDialog.Line1 = Resources.ThumbnailsPreloader_PreloadingThumbnails;
                    progressDialog.Line3 = Resources.ThumbnailsPreloader_CalculatingNumberOfItems;
                    progressDialog.Marquee = true;
                }
                totalItemsCount = directoryScanner.totalItemsCount; // Placed here to only update when needed by the progress dialog
                progressDialog.Line2 = String.Format(Resources.ThumbnailsPreloader_Discovered0Items, totalItemsCount);
            }
            else if (state == ThumbnailsPreloaderState.Processing)
            {
                if (prevState != state)
                {
                    prevState = state;
                    progressDialog.Line1 = String.Format(Resources.ThumbnailsPreloader_PreloadingThumbnailsFor0Items, totalItemsCount);
                    progressDialog.Maximum = totalItemsCount;
                    progressDialog.Marquee = false;
                }
                progressDialog.Title = String.Format(Resources.ThumbnailsPreloader_Processing, (processedItemsCount * 100) / totalItemsCount);
                progressDialog.Line2 = Resources.ThumbnailsPreloader_Name + ": " + Path.GetFileName(currentFile);
                progressDialog.Line3 = String.Format(Resources.ThumbnailsPreloader_ItemsRemaining, totalItemsCount - processedItemsCount);
                progressDialog.Value = processedItemsCount;
            }
            else if (state == ThumbnailsPreloaderState.Done)
            {
                progressDialog.Close();
                progressDialog?.Dispose();
                progressDialogUpdateTimer.Stop();
                progressDialogUpdateTimer?.Dispose();
                return;
            }
        }

        private async void Run()
        {
            state = ThumbnailsPreloaderState.GettingNumberOfItems;
            List<string> items = new List<string>();

            await Task.Run(() =>
            {
                items = directoryScanner.GetItems();
                totalItemsCount = directoryScanner.totalItemsCount; // This is also here in case the program is run in silent mode
                if (totalItemsCount == 0)
                {
                    state = ThumbnailsPreloaderState.Done;
                    return;
                }
                else if (state == ThumbnailsPreloaderState.Canceled)
                {
                    return;
                }

                // Start processing
                state = ThumbnailsPreloaderState.Processing;
                if (!multiThreaded)
                {
                    foreach (string item in items)
                    {
                        try
                        {
                            currentFile = item;
                            ThumbnailPreloader.PreloadThumbnail(item, thumbnailSizes);
                            processedItemsCount++;
                            if (processedItemsCount == totalItemsCount) state = ThumbnailsPreloaderState.Done;
                            if (state == ThumbnailsPreloaderState.Canceled) Application.Exit();
                        }
                        catch (Exception) { } // Do nothing
                    }
                }
                else
                {
                    Parallel.ForEach(
                        items,
                        new ParallelOptions { MaxDegreeOfParallelism = threadCount },
                        (item, parallelLoopState) =>
                        {
                            try
                            {
                                currentFile = item;
                                ThumbnailPreloader.PreloadThumbnail(item, thumbnailSizes);
                                processedItemsCount++; 
                                if (processedItemsCount == totalItemsCount) state = ThumbnailsPreloaderState.Done;
                                if (state == ThumbnailsPreloaderState.Canceled)
                                {
                                    parallelLoopState.Break();
                                    return;
                                }
                            }
                            catch (Exception) { } // Do nothing
                        });
                }
            });
            Application.Exit();
        }
    }
}