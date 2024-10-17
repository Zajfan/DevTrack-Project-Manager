// ProjectDashboardData.cs
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;

namespace DevTrack.BLL
{
    public class ProjectDashboardData
    {
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; } // Remove nullable operator (?)
        public List<Milestone> Milestones { get; set; } // Remove nullable operator (?)
        public List<DocumentRepository> Documents { get; set; } // Remove nullable operator (?)
        // ... add other properties for the dashboard data as needed
    }
}