namespace Assignment1_BLL
{
    /// <summary>
    /// Class to handle File
    /// </summary>
    public class FileClass
    {
        //Since FileInfo is sealed I cannot inherit it, so using it like this instead 
        public FileInfo FileInfo { get; set; }
        public string? Name
        {
            get
            {
                return FileInfo.Name;
            }
        }
        public string? Path
        {
            get
            {
                return FileInfo.DirectoryName;
            }
        }
        public string? Extension
        {
            get
            {
                return FileInfo.Extension;
            }

        }
        public DateTime DateCreated
        {
            get
            {
                return FileInfo.CreationTimeUtc;
            }
        }
        public DateTime DateModified
        {
            get
            {
                return FileInfo.LastWriteTimeUtc;
            }
        }
        public string? Size
        {
            get
            {
                return FileInfo.Length.ToString();
            }
        }
        public string? Image
        {
            get { 
                return FileInfo.FullName; 
            }
        }
        public FileClass(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
        /// <summary>
        /// Method for delete, delegated to FileInfo
        /// </summary>
        public void Delete()
        {
            FileInfo.Delete();
        }
    }
}