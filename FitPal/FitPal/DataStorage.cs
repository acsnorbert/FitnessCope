using System;
using System.Collections.Generic;
using System.IO;

namespace FitPal
{

    public static class DataStorage
    {
        private static readonly string BaseFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FitPal");

        private static void EnsureFolder()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public static void SaveLines(string fileName, List<string> lines)
        {
            EnsureFolder();
            string path = Path.Combine(BaseFolder, fileName);
            File.WriteAllLines(path, lines);
        }

        public static List<string> LoadLines(string fileName)
        {
            string path = Path.Combine(BaseFolder, fileName);
            if (!File.Exists(path))
                return new List<string>();
            return new List<string>(File.ReadAllLines(path));
        }

        public static void SaveText(string fileName, string text)
        {
            EnsureFolder();
            string path = Path.Combine(BaseFolder, fileName);
            File.WriteAllText(path, text);
        }

        public static string LoadText(string fileName)
        {
            string path = Path.Combine(BaseFolder, fileName);
            if (!File.Exists(path))
                return null;
            return File.ReadAllText(path);
        }
    }
}
