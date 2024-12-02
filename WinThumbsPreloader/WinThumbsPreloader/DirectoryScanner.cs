using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinThumbsPreloader
{
    class DirectoryScanner
    {
        private string path;
        private bool includeNestedDirectories;
        string[] thumbnailExtensions = ThumbnailExtensions();
        public static string[] defaultExtensions = { "avif", "bmp", "gif", "heic", "jpg", "jpeg", "mkv", "mov", "mp4", "png", "svg", "tif", "tiff", "webp" };

        public DirectoryScanner(string path, bool includeNestedDirectories)
        {
            this.path = path;
            this.includeNestedDirectories = includeNestedDirectories;
        }

        public static string[] ThumbnailExtensions()
        {
            string[] thumbnailExtensions;
            try
            {
                thumbnailExtensions = File.ReadAllLines("ThumbnailExtensions.txt")
                                          .SelectMany(line => line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)) // Ignore commas and spaces between extensions
                                          .Where(ext => !string.IsNullOrWhiteSpace(ext)) // Ignore blank lines or lines with only spaces
                                          .Select(ext => ext.Trim()).ToArray(); // Trim spaces around each extension
                if (thumbnailExtensions == null || thumbnailExtensions.Length == 0)
                {
                    thumbnailExtensions = defaultExtensions;
                }
            }
            catch (FileNotFoundException)
            {
                thumbnailExtensions = defaultExtensions;
            }
            return thumbnailExtensions;
        }

        public IEnumerable<string> GetItems()
        {
            return includeNestedDirectories ? GetItemsNested() : GetItemsOnlyFirstLevel();
        }

        private IEnumerable<string> GetItemsOnlyFirstLevel()
        {
            IEnumerable<string> files = Enumerable.Empty<string>();
            try
            {
                files = Directory.GetFileSystemEntries(path)
                                 .Where(file => thumbnailExtensions.Contains(Path.GetExtension(file).TrimStart('.'), StringComparer.OrdinalIgnoreCase));
            }
            catch (Exception) { } // Do nothing

            foreach (var file in files)
            {
                yield return file;
            }
        }

        private IEnumerable<string> GetItemsNested()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);

            while (queue.Count > 0)
            {
                string currentPath = queue.Dequeue();

                try
                {
                    foreach (string subDir in Directory.GetDirectories(currentPath))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception) { } // Do nothing

                IEnumerable<string> files = Enumerable.Empty<string>();
                try
                {
                    files = Directory.GetFiles(currentPath)
                                     .Where(file => thumbnailExtensions.Contains(Path.GetExtension(file).TrimStart('.'), StringComparer.OrdinalIgnoreCase));
                }
                catch (Exception) { } // Do nothing

                foreach (var file in files)
                {
                    yield return file;
                }
            }
        }
    }
}