// ProjectService.cs
using DevTrack.BLL.Models;
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using Task = DevTrack.DAL.Models.Task;

namespace DevTrack.BLL
{
    public class ProjectService
    {
        private readonly ProjectRepository projectRepository; // No underscore
        private readonly TaskRepository taskRepository;
        private readonly MilestoneRepository milestoneRepository;
        private readonly DocumentRepository documentRepository;

        public ProjectService(
            ProjectRepository projectRepository,
            TaskRepository taskRepository,
            MilestoneRepository milestoneRepository,
            DocumentRepository documentRepository)
        {
            this.projectRepository = projectRepository; // No underscore
            this.taskRepository = taskRepository;
            this.milestoneRepository = milestoneRepository;
            this.documentRepository = documentRepository;
        }

        public DocumentRepository GetDocumentRepository()
        {
            return documentRepository;
        }

        public
 TaskRepository GetTaskRepository()
        {
            return taskRepository;
        }

        public async System.Threading.Tasks.Task<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId)
        {
            var project = await projectRepository.GetProjectByIdAsync(projectId); // No underscore
            var tasks = await taskRepository.GetTasksByProjectIdAsync(projectId);
            var milestones = await milestoneRepository.GetAllMilestonesAsync();
            var documents = await documentRepository.GetAllDocumentsAsync();

            return new ProjectDashboardData
            {
                Project = project,
                Tasks = tasks.Cast<DevTrack.DAL.Models.Task>().ToList(),
                Milestones = milestones,
                Documents = documents.Cast<DevTrack.DAL.Models.Document>().ToList()
            };
        }

        public async System.Threading.Tasks.Task<List<Project>> GetAllProjectsAsync()
        {
            return await projectRepository.GetAllProjectsAsync(); // No underscore
        }

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
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                await projectRepository.UpdateProjectAsync(project);
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
                var existingProject = await projectRepository.GetProjectByIdAsync(projectId);
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                await projectRepository.DeleteProjectAsync(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }

        private bool IsProjectNameUnique(string projectName)
        {
            // ... (Your implementation to check for uniqueness) ...
            return true; // Or false if the name is not unique
        }
    }
}