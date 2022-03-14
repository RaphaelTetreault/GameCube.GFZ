namespace GameCube.GFZ.LZ
{
    /// <summary>
    /// Metadata struct to indicate file status after IO operation.
    /// </summary>
    public struct FileStatus
    {
        /// <summary>
        /// Indicates the IO operation for was successful.
        /// </summary>
        public bool success;

        /// <summary>
        /// The file path of the IO operation.
        /// </summary>
        public string filePath;

        public FileStatus(bool success, string filePath)
        {
            this.success = success;
            this.filePath = filePath;
        }
    }
}