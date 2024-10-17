using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using SystemTask = System.Threading.Tasks.Task; // Alias for System.Threading.Tasks.Task

namespace DevTrack.BLL
{
    public class ProjectService
    {
        private readonly ProjectRepository projectRepository;
        private readonly TaskRepository taskRepository;
        private readonly MilestoneRepository milestoneRepository;
        private readonly DocumentRepository documentRepository;

        public ProjectService(
            ProjectRepository projectRepository,
            TaskRepository taskRepository,
            MilestoneRepository milestoneRepository,
            DocumentRepository documentRepository)
        {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
            this.milestoneRepository = milestoneRepository;
            this.documentRepository = documentRepository;
        }

        public DocumentRepository GetDocumentRepository()
        {
            return documentRepository;
        }

        public async SystemTask<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId, DocumentRepository documentRepository)
        {
            var project = await projectRepository.GetProjectByIdAsync(projectId);
            var tasks = await taskRepository.GetTasksByProjectIdAsync(projectId);
            var milestones = await milestoneRepository.GetAllMilestonesAsync();
            var documents = await documentRepository.GetAllDocumentsAsync();

            return new ProjectDashboardData
            {
                Project = project,
                Tasks = tasks,
                Milestones = milestones,
                Documents = documents.Cast<DAL.Models.Document>().ToList()
            };
        }

        public async SystemTask<List<Project>> GetAllProjectsAsync()
        {
            return await projectRepository.GetAllProjectsAsync();
        }

        public async SystemTask CreateProjectAsync(Project project)
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

        public async SystemTask UpdateProjectAsync(Project project)
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

        public async SystemTask DeleteProjectAsync(int projectId)
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