using System;
using System.Collections.Generic;
using System.IO;

namespace HowManyLinesOfCode
{
    class Program
    {
        static bool includeSubFolders = false;
        static List<string> searchDirectories = new List<string>();
        static List<string> fileExtensions = new List<string>();
        static int linesCount = 0;

        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--"))
                    {
                        var key = args[i].Substring(2);

                        if (i + 1 > args.Length)
                            throw new Exception("A CMD PARAM must be followed by it's value. Ex : --command commandValue");

                        var value = args[i + 1];

                        if (!CheckValue(value))
                            throw new Exception("A CMD PARAM must be followed by it's value. Ex : --command commandValue");

                        switch (key)
                        {
                            case "path":
                                SetSearchDirectories(value);
                                break;
                            case "includeSubFolders":
                                if (!bool.TryParse(value, out var val))
                                    throw new Exception("Parameter 'includeSubFolders' can only have values : 'true' or 'false'");

                                includeSubFolders = val;
                                break;
                            case "fileExtensions":
                                SetFileExtensions(value);
                                break;
                            default:
                                throw new Exception($"Unknown parameter : {key}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] ${ex.Message}");
                Console.ResetColor();
            }

            foreach (var searchDirectory in searchDirectories)
            {
                CountFilesLines(searchDirectory, includeSubFolders);
            }

            Console.Write("> Done calculating... You've written ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(linesCount);

            Console.ResetColor();
            Console.Write(" lines of code !");
        }

        static void CountFilesLines(string path, bool searchSubFolders)
        {
            var files = new List<string>();

            if (fileExtensions == null || fileExtensions.Count == 0)
            {
                var extfiles = Directory.GetFiles(path);
                files.AddRange(extfiles);
            }
            else
            {
                foreach (var ext in fileExtensions)
                {
                    var extfiles = Directory.GetFiles(path, $"*.{ext}");
                    files.AddRange(extfiles);
                }
            }

            foreach (var file in files)
                linesCount += File.ReadAllLines(file).Length;

            if (searchSubFolders)
            {
                var subDirectories = Directory.GetDirectories(path);

                foreach (var subDirectory in subDirectories)
                    CountFilesLines(subDirectory, searchSubFolders);
            }
        }

        static void SetFileExtensions(string cmdValue)
        {
            var exts = cmdValue.Split(';');

            foreach (var ext in exts)
            {
                if (ext.StartsWith('.'))
                    throw new Exception($"Invalid File Extension : {ext} | Example of valid input : --fileExtensions js;cs");

                fileExtensions.Add(ext);
            }
        }

        static void SetSearchDirectories(string cmdPathValue)
        {
            var paths = cmdPathValue.Split(';');

            foreach (var path in paths)
            {
                var fullPath = Path.GetFullPath(path);
                searchDirectories.Add(fullPath);
            }
        }

        static bool CheckValue(string value)
        {
            if (value.StartsWith("--"))
                return false;
            else
                return true;
        }
    }
}
