using System;
using System.IO;
using System.Text;

namespace FileTreeSimplified
{
    class Program
    {
        const string DirectoryNodeSign = "■─";
        const string FileNodeSign = "└─";
        const int IndentSize = 2;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Enter directory path: ");
            var path = Console.ReadLine();
            PrintDirectory(path);

            Console.ReadKey();
        }

        static void PrintDirectory(string path, int depth = 1)
        {
            if (depth == 1)
            {
                ColorPrint(path, ConsoleColor.Yellow);
            }

            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    ColorPrint($"{GetIndent(depth)}{FileNodeSign}{fileName}", ConsoleColor.White);
                }

                var directories = Directory.GetDirectories(path);

                foreach (var directory in directories)
                {
                    var directoryName = Path.GetFileName(directory);
                    ColorPrint($"{GetIndent(depth)}{DirectoryNodeSign}{directoryName}", ConsoleColor.Yellow);

                    PrintDirectory(directory, depth + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        static string GetIndent(int depth)
        {
            return new string(' ', depth * IndentSize);
        }

        static void ColorPrint(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}