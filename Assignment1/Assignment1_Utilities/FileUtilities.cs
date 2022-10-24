using System.Collections.ObjectModel;
using System.Windows;

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
        /// <summary>
        /// Helper method to get files in directory. Uses distinct to get unique extensions.
        /// </summary>
        /// <param name="chosenDir">Folder to list files in</param>
        /// <param name="extensions">List of extensions to check for</param>
        /// <returns>List of strings</returns>
        public static List<string> GetFilesInDirectory(string chosenDir, List<string> extensions = null)
        {            
            if (extensions == null)
            {
                try
                {
                    return new List<string>(Directory.GetFiles(chosenDir));
                }
                catch (UnauthorizedAccessException ex)
                {
                    string errorMessage = $"Unable to access files because of unauthorized exception: {ex.Message}";
                    MessageBox.Show(errorMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Return empty list
                    return new List<string>();
                }
            }
            else
            {                    
                List<string> files = new List<string>();
                foreach (string ext in extensions.Distinct().ToList())
                {
                    try
                    {
                        files.AddRange(Directory.GetFiles(chosenDir, ext));
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        string errorMessage = $"Unable to access files because of unauthorized exception: {ex.Message}";
                        MessageBox.Show(errorMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                }
                return files;
            }
                        
        }

        public static List<FileInfo> GetFileInfoFromDirectory(string? chosenDir, List<string> extensions = null)
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach(string file in GetFilesInDirectory(chosenDir, extensions)) {
                files.Add(new FileInfo(file));
            }
            return files;
        }
    }
}