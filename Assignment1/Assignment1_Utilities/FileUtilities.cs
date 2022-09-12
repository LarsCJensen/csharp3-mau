using System.Collections.ObjectModel;

namespace Assignment1_Utilities
{
    public static class FileUtilities
    {
        /// <summary>
        /// Helper method to get logical drives 
        /// </summary>
        public static string[] AddLogicalDrives()
        {
            return Directory.GetLogicalDrives();            
        }
        /// <summary>
        /// Helper method to get directories 
        /// </summary>
        public static string[] GetDirectories(string chosenDir)
        {
            return Directory.GetDirectories(chosenDir);
        }

        public static List<string> GetFilesInDirectory(string chosenDir, string[]? extensions = null)
        {
            if (extensions == null) {
                return new List<string>(Directory.GetFiles(chosenDir));
            }
            else
            {
                List<string> files = new List<string>();
                foreach(string ext in extensions)
                {
                    files.AddRange(Directory.GetFiles(chosenDir, ext));
                }
                return files; 
            }
        }

        public static List<FileInfo> GetFileInfoFromDirectory(string? chosenDir, string[]? extensions = null)
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach(string file in GetFilesInDirectory(chosenDir, extensions)) {
                files.Add(new FileInfo(file));
            }
            return files;
        }
    }
}