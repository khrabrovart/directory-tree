using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileTree
{
    class Program
    {
        const string VerticalSign = "│ ";
        const string IntermediateNodeSign = "├─";
        const string TerminalNodeSign = "└─";

        static void Main(string[] args)
        {
            Console.Write("Enter directory path: ");
            var path = Console.ReadLine();
            PrintDirectoryTree(path);

            Console.ReadKey();
        }

        static void PrintDirectoryTree(string path, int level = 0, List<int> intermediateLevels = null)
        {
            intermediateLevels = intermediateLevels ?? new List<int>();
            intermediateLevels.Add(level);

            if (level == 0)
            {
                ColorPrint(path, ConsoleColor.Yellow);
                Console.WriteLine();
            }

            if (!Directory.Exists(path))
            {
                return;
            }

            try
            {
                var files = Directory.GetFiles(path);
                var directories = Directory.GetDirectories(path);

                for (int i = 0; i < files.Length; i++)
                {
                    var fileName = Path.GetFileName(files[i]);
                    var isTerminal = i == files.Length - 1 && directories.Length == 0;
                    var nodeLabel = isTerminal ? TerminalNodeSign : IntermediateNodeSign;

                    Console.Write($"{CreateIndent(level, intermediateLevels)}{nodeLabel}");
                    ColorPrint(fileName, ConsoleColor.White);
                    Console.WriteLine();
                }

                for (int i = 0; i < directories.Length; i++)
                {
                    var directoryName = Path.GetFileName(directories[i]);
                    var isTerminal = i == directories.Length - 1;
                    var nodeLabel = isTerminal ? TerminalNodeSign : IntermediateNodeSign;

                    Console.Write($"{CreateIndent(level, intermediateLevels)}{nodeLabel}");
                    ColorPrint(directoryName, ConsoleColor.Yellow);
                    Console.WriteLine();

                    if (isTerminal)
                    {
                        intermediateLevels.Remove(level);
                    }

                    PrintDirectoryTree(directories[i], level + 1, intermediateLevels);
                }

                intermediateLevels.Remove(level);
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
        }

        static string CreateIndent(int level, List<int> intermediateLevels)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < level; i++)
            {
                if (intermediateLevels.Contains(i))
                {
                    sb.Append(VerticalSign);
                }
                else
                {
                    sb.Append("   ");
                }
            }

            return sb.ToString();
        }

        static void ColorPrint(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
