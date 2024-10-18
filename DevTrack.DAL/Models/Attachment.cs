// Attachment.cs
using System;

namespace DevTrack.DAL.Models
{
    public class Attachment
    {
        public int AttachmentID { get; set; }
        public int TaskID { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
