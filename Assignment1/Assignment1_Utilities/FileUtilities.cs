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
        public static string[] GetDirectories(string chosenFolder)
        {
            return Directory.GetDirectories(chosenFolder);
        }
    }
}