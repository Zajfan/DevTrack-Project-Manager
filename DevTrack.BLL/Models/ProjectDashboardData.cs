// ProjectDashboardData.cs
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using System.Collections.Generic;

namespace DevTrack.BLL.Models
{
    public class ProjectDashboardData
    {
        public required Project Project { get; set; }
        public required List<DevTrack.DAL.Models.Task> Tasks { get; set; }
        public List<Milestone> Milestones { get; set; }
        public required List<DocumentRepository> Documents { get; set; }
        // ... add other properties for the dashboard data as needed
    }
}