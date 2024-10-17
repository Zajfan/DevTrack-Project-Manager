using System;

namespace DevTrack.DAL.Models
{
    public class OverdueTaskReport
    {
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }
        public string ProjectName { get; set; }
    }
}