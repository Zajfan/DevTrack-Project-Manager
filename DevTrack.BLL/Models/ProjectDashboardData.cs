// ProjectDashboardData.cs
using DevTrack.DAL.Models;
using System.Reflection.Metadata;

namespace DevTrack.BLL.Models
{
    public class ProjectDashboardData
    {
        public required Project Project { get; set; }

        // Use the fully qualified name for the Task type
        public List<DevTrack.DAL.Models.Task> Tasks { get; set; }

        public List<Milestone> Milestones { get; set; }
        public List<Document> Documents { get; set; }
        // ... add other properties for the dashboard data as needed
    }
}