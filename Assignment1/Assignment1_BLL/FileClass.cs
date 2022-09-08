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
        public string CheckSum { get; set; }
        public int DuplicateId { get; set; }

        public FileClass((FileInfo FileInfo, string Checksum, int DuplicateId) duplicateFile)
        {
            FileInfo = duplicateFile.FileInfo;
            CheckSum = duplicateFile.Checksum;
            DuplicateId = duplicateFile.DuplicateId;
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