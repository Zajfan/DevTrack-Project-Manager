// ProjectService.cs
using DevTrack.BLL.Models;
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using System.Threading.Tasks;

namespace DevTrack.BLL
{
    public class ProjectService(ProjectRepository projectRepository, TaskRepository taskRepository, MilestoneRepository milestoneRepository, DocumentRepository documentRepository)
    {
        public DocumentRepository GetDocumentRepository()
        {
            return documentRepository;
        }

        public TaskRepository GetTaskRepository()
        {
            return taskRepository;
        }

        public static List<Milestone> GetMilestones()
        {
            List<Milestone>? Milestones = null;
            return Milestones;
        }

        public async System.Threading.Tasks.Task<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId, List<Milestone> milestones)
        {
            var project = await projectRepository.GetProjectByIdAsync(projectId);
            var tasks = await taskRepository.GetTasksByProjectIdAsync(projectId);
            var allMilestones = await milestoneRepository.GetAllMilestonesAsync();
            var documents = await documentRepository.GetAllDocumentsAsync();

            return new ProjectDashboardData
            {
                Project = project,
                Tasks = tasks.Cast<DevTrack.DAL.Models.Task>().ToList(),
                Milestones = allMilestones,
                Documents = documents.Cast<Document>().ToList()
            };
        }

        public async System.Threading.Tasks.Task<List<Project>> GetAllProjectsAsync() => await projectRepository.GetAllProjectsAsync();

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
        {
            if (IsProjectNameUnique(project.ProjectName))
            {
                await projectRepository.CreateProjectAsync(project);
            }
            else
            {
                throw new Exception("Project name already exists.");
            }
        }

        public async System.Threading.Tasks.Task UpdateProjectAsync(Project project)
        {
            try
            {
                var existingProject = await projectRepository.GetProjectByIdAsync(project.ProjectID);
                if (existingProject != null)
                {
                    await projectRepository.UpdateProjectAsync(project);
                }
                else
                {
                    throw new Exception("Project not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteProjectAsync(int projectId)
        {
            try
            {
                var existingProject = await projectRepository.GetProjectByIdAsync(projectId) ?? throw new Exception("Project not found.");
                await projectRepository.DeleteProjectAsync(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }

        private static bool IsProjectNameUnique(string projectName)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentException($"'{nameof(projectName)}' cannot be null or empty.", nameof(projectName));
            }
            // ... (Your implementation to check for uniqueness) ...
            return true; // Or false if the name is not unique
        }
    }
 
}