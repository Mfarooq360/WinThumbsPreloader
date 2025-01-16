using System;
using System.Collections.Generic;
using System.IO;

namespace WinThumbsPreloader
{
    class Options
    {
        public int threadCount { get; private set; } = Environment.ProcessorCount;
        public List<int> thumbnailSizes { get; private set; } = new List<int>();

        public bool badArguments;
        public bool includeNestedDirectories;
        public bool silentMode;
        public bool multithreaded;
        public List<string> extensions;
        public string path;

        public Options(string[] arguments)
        {
            includeNestedDirectories = false;
            silentMode = false;
            multithreaded = false;
            extensions = new List<string>();

            string extensionsArg = null;

            for (int i = 0; i < arguments.Length; i++)
            {
                string argu = arguments[i];

                switch (argu)
                {
                    case "-r":
                        includeNestedDirectories = true;
                        break;
                    case "-s":
                        silentMode = true;
                        break;
                    case "-m":
                        multithreaded = true;
                        if (i + 1 < arguments.Length && int.TryParse(arguments[i + 1], out int parsedThreadCount))
                        {
                            i++;
                            threadCount = Clamp(parsedThreadCount, 0, 256);
                            if (threadCount == 0)
                            {
                                threadCount = Environment.ProcessorCount;
                            }
                        }
                        break;
                    case "-e":
                        if (i + 1 < arguments.Length)
                        {
                            extensionsArg = arguments[++i];
                            extensions.AddRange(ParseExtensions(extensionsArg));
                        }
                        else
                        {
                            badArguments = true;
                            return;
                        }
                        break;
                    case "-d":
                        if (i + 1 < arguments.Length)
                        {
                            string sizesArg = arguments[++i];
                            ParseThumbnailSizes(sizesArg);
                        }
                        break;
                    default:
                        if (path == null)
                        {
                            path = argu.Trim().Trim('"');
                        }
                        else
                        {
                            badArguments = true;
                        }
                        break;
                }
            }

            if (string.IsNullOrWhiteSpace(path) || !(Directory.Exists(path) || File.Exists(path)))
            {
                badArguments = true;
            }
        }

        private IEnumerable<string> ParseExtensions(string extensionsArg)
        {
            if (string.IsNullOrWhiteSpace(extensionsArg)) return Array.Empty<string>();

            return extensionsArg.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ParseThumbnailSizes(string sizesArg)
        {
            var allowedSizes = new HashSet<int> { 16, 32, 48, 96, 256, 768, 1280, 1920, 2560 };
            var sizes = sizesArg.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sizeStr in sizes)
            {
                if (int.TryParse(sizeStr, out int size) && allowedSizes.Contains(size))
                {
                    thumbnailSizes.Add(size);
                }
            }
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
