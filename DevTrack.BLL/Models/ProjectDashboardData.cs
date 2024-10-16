// ProjectDashboardData.cs
using DevTrack.DAL.Models;
using System.Collections.Generic;

namespace DevTrack.BLL.Models
{
    public class ProjectDashboardData
    {
        public Project Project { get; set; }
        public List<DevTrack.DAL.Models.Task>? Tasks { get; set; }
        public List<DAL.Models.Milestone>? Milestones { get; set; }
        public List<Document> Documents { get; set; }
    }

}