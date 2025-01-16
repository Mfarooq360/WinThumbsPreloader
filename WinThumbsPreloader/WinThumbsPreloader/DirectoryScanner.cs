using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WinThumbsPreloader
{
    class DirectoryScanner
    {
        public int totalItemsCount = 0;
        private string path;
        public bool cancelled = false;
        private bool includeNestedDirectories;
        private bool multiThreaded;
        private int threadCount;
        private string[] thumbnailExtensions;
        public static string[] defaultExtensions = { "avif", "bmp", "gif", "heic", "heif", "jpg", "jpeg", "mkv", "mov", "mp4", "png", "svg", "tif", "tiff", "webp" };

        public DirectoryScanner(string path, bool includeNestedDirectories, bool multiThreaded, IEnumerable<string> commandLineExtensions = null, int threadCount = 0)
        {
            this.path = path;
            this.includeNestedDirectories = includeNestedDirectories;
            this.multiThreaded = multiThreaded;
            this.threadCount = threadCount > 0 ? threadCount : Environment.ProcessorCount;

            thumbnailExtensions = GetThumbnailExtensions(commandLineExtensions);
        }

        private string[] GetThumbnailExtensions(IEnumerable<string> commandLineExtensions)
        {
            if (commandLineExtensions != null && commandLineExtensions.Any())
            {
                return commandLineExtensions.ToArray();
            }

            try
            {
                var settingsExtensions = Properties.Settings.Default.ExtensionsText
                    .Split(new[] { ',', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(ext => !string.IsNullOrWhiteSpace(ext))
                    .Select(ext => ext.Trim())
                    .ToArray();

                if (settingsExtensions.Length > 0)
                {
                    return settingsExtensions;
                }
            }
            catch (Exception) { }

            return defaultExtensions;
        }

        public List<string> GetItems()
        {
            return includeNestedDirectories
                ? (multiThreaded ? GetItemsNestedParallel() : GetItemsNested())
                : GetItemsOnlyFirstLevel();
        }

        private bool ShouldIncludeFile(string file)
        {
            return thumbnailExtensions.Contains(Path.GetExtension(file).TrimStart('.'), StringComparer.OrdinalIgnoreCase) || thumbnailExtensions.Length == 0;
        }

        private List<string> GetItemsOnlyFirstLevel()
        {
            var scannedFiles = new List<string>();
            try
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    if (ShouldIncludeFile(file))
                    {
                        scannedFiles.Add(file);
                        totalItemsCount++;
                    }
                    if (cancelled) break;
                }
            }
            catch (Exception) { }
            return scannedFiles;
        }

        private List<string> GetItemsNested()
        {
            var scannedFiles = new List<string>();
            var queue = new Queue<string>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                string currentPath = queue.Dequeue();

                try
                {
                    foreach (string subDir in Directory.EnumerateDirectories(currentPath))
                    {
                        queue.Enqueue(subDir);
                        if (cancelled) break;
                    }
                }
                catch (Exception) { }

                try
                {
                    foreach (string file in Directory.EnumerateFiles(currentPath))
                    {
                        if (ShouldIncludeFile(file))
                        {
                            scannedFiles.Add(file);
                            totalItemsCount++;
                        }
                        if (cancelled) break;
                    }
                }
                catch (Exception) { }
                if (cancelled) break;
            }
            return scannedFiles;
        }

        private List<string> GetItemsNestedParallel()
        {
            var scannedFiles = new ConcurrentBag<string>();
            var directoriesToProcess = new BlockingCollection<string> { path };
            int activeThreads = 0;

            Parallel.ForEach(
                directoriesToProcess.GetConsumingEnumerable(),
                new ParallelOptions { MaxDegreeOfParallelism = threadCount },
                currentPath =>
                {
                    Interlocked.Increment(ref activeThreads);

                    try
                    {
                        foreach (string subDir in Directory.EnumerateDirectories(currentPath))
                        {
                            if (cancelled)
                            {
                                directoriesToProcess.CompleteAdding();
                                foreach (string dir in directoriesToProcess.GetConsumingEnumerable()) { }
                                break;
                            }

                            directoriesToProcess.Add(subDir);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        foreach (string file in Directory.EnumerateFiles(currentPath))
                        {
                            if (cancelled)
                            {
                                directoriesToProcess.CompleteAdding();
                                foreach (string dir in directoriesToProcess.GetConsumingEnumerable()) { }
                                break;
                            }

                            if (ShouldIncludeFile(file))
                            {
                                scannedFiles.Add(file);
                                totalItemsCount++;
                            }
                        }
                    }
                    catch (Exception) { }

                    if (Interlocked.Decrement(ref activeThreads) == 0 && directoriesToProcess.Count == 0)
                    {
                        directoriesToProcess.CompleteAdding();
                    }
                });

            return scannedFiles.OrderBy(file => file, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }

}