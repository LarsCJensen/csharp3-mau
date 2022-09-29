namespace Assignment2.BLL
{
    /// <summary>
    /// Class to handle File
    /// </summary>
    public class OLDFile
    {
        //Since FileInfo is sealed I cannot inherit it, so using it like this and expand my own File class
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
        public string? FullName
        {
            get
            {
                return FileInfo.FullName;
            }
        }
        public string? Image
        {
            get { 
                return FileInfo.FullName; 
            }
        }
        public OLDFile(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }        
    }
}