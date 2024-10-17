// ProjectDashboardData.cs
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using Task = DevTrack.DAL.Models.Task;

namespace DevTrack.BLL
{
    public class ProjectDashboardData
    {
        public required Project Project { get; set; }
        public required List<Task> Tasks { get; set; } // Remove nullable operator (?)
        public required List<Milestone> Milestones { get; set; } // Remove nullable operator (?)
        public required List<DocumentRepository> Documents { get; set; } // Remove nullable operator (?)
        // ... add other properties for the dashboard data as needed
    }
}