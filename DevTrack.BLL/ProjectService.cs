// ProjectService.cs
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using Task = DevTrack.DAL.Models.Task;

namespace DevTrack.BLL
{
    public class ProjectService
    {
        private readonly ProjectRepository projectRepository;
        private readonly TaskRepository taskRepository;
        private readonly MilestoneRepository milestoneRepository;
        private readonly DocumentRepository documentRepository;
        // ... add other repositories as needed

        public ProjectService(
            ProjectRepository projectRepository,
            TaskRepository taskRepository,
            MilestoneRepository milestoneRepository,
            DocumentRepository documentRepository
            /*, ... other repositories */)
        {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
            this.milestoneRepository = milestoneRepository;
            this.documentRepository = documentRepository;
            // ... initialize other repositories
        }

        public TaskRepository GetTaskRepository()
        {
            return taskRepository;
        }

        public async Task<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId, TaskRepository taskRepository)
        {
            var project = await projectRepository.GetProjectByIdAsync(projectId);
            var tasks = await taskRepository.GetTasksByProjectIdAsync(projectId);
            var milestones = await milestoneRepository.GetAllMilestonesAsync(); // Assuming you want all milestones
            var documents = await documentRepository.GetAllDocumentsAsync(); // Assuming you want all documents
            // ... get other relevant data

            return new ProjectDashboardData
            {
                Project = project,
                Tasks = tasks,
                Milestones = milestones,
                Documents = documents
                // ... other data
            };
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await projectRepository.GetAllProjectsAsync();
        }

        public async Task CreateProjectAsync(Project project)
        {
            // 1. Perform validation on the project data (e.g., check if the project name is unique)
            if (IsProjectNameUnique(project.ProjectName))
            {
                // 2. If valid, call the repository method to create the project
                await projectRepository.CreateProjectAsync(project);
            }
            else
            {
                // Handle validation error (e.g., throw an exception or return an error message)
                throw new Exception("Project name already exists.");
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            try
            {
                // 1. Perform validation (e.g., check if the project exists, validate properties)
                var existingProject = await projectRepository.GetProjectByIdAsync(project.ProjectID);
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                // (Add more validation if needed) ...

                // 2. If valid, call the repository to update
                await projectRepository.UpdateProjectAsync(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
                throw; // Or handle the exception appropriately
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            try
            {
                // 1. (Optional) Perform checks (e.g., if the project exists, if it has associated tasks, etc.)
                var existingProject = await projectRepository.GetProjectByIdAsync(projectId);
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                // (Add more checks if needed) ...

                // 2. Call the repository to delete
                await projectRepository.DeleteProjectAsync(projectId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                throw; // Or handle the exception appropriately
            }
        }

        // ... (Add other methods for project management) ...

        // Example validation method (replace with your actual validation logic)
        private bool IsProjectNameUnique(string projectName)
        {
            // Check if a project with the given name already exists in the database
            // ... (Your implementation to check for uniqueness) ...
            return true; // Or false if the name is not unique
        }
    }
}