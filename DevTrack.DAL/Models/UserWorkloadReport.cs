// UserWorkloadReport.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTrack.DAL.Models
{
    public class UserWorkloadReport
    {
        public string UserName { get; set; }
        public int TotalTasks { get; set; }
    }
}