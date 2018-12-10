using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileTree
{
    class Program
    {
        const string VerticalLabel = "│ ";
        const string IntermediateNodeLabel = "├─";
        const string TerminalNodeLabel = "└─";

        static void Main(string[] args)
        {
            PrintDirectoryTree(@"C:\Tests");
            Console.ReadKey();
        }

        static void PrintDirectoryTree(string path, int level = 0, List<int> intermediateLevels = null)
        {
            intermediateLevels = intermediateLevels ?? new List<int>();
            intermediateLevels.Add(level);

            if (level == 0)
            {
                ColorPrint(path, ConsoleColor.Yellow);
            }

            if (!Directory.Exists(path))
            {
                return;
            }

            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);

            for (int i = 0; i < files.Length; i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var nodeLabel = i == files.Length - 1 && directories.Length == 0
                    ? TerminalNodeLabel 
                    : IntermediateNodeLabel;

                ColorPrint($"{FilledIndent(level, intermediateLevels)}{nodeLabel}{fileName}", ConsoleColor.White);
            }

            for (int i = 0; i < directories.Length; i++)
            {
                var directoryName = Path.GetFileName(directories[i]);
                var isLast = i == directories.Length - 1;
                var nodeLabel = isLast ? TerminalNodeLabel : IntermediateNodeLabel;

                ColorPrint($"{FilledIndent(level, intermediateLevels)}{nodeLabel}{directoryName}", ConsoleColor.Yellow);

                if (isLast)
                {
                    intermediateLevels.Remove(level);
                }

                PrintDirectoryTree(directories[i], level + 1, intermediateLevels);
            }

            intermediateLevels.Remove(level);
        }

        static string FilledIndent(int level, List<int> intermediateLevels)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < level; i++)
            {
                if (intermediateLevels.Contains(i))
                {
                    sb.Append(VerticalLabel);
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
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
