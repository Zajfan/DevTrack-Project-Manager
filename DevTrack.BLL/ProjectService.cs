// ProjectService.cs
using DevTrack.BLL.Models;
using DevTrack.DAL.Models;
using EntityTask = DevTrack.DAL.Models.Task;
using DevTrack.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTrack.BLL
{
    public class ProjectService(
        ProjectRepository projectRepository,
        TaskRepository taskRepository,
        MilestoneRepository milestoneRepository,
        DocumentRepository documentRepository)
    {
        public DocumentRepository GetDocumentRepository()
        {
            return documentRepository;
        }

        public TaskRepository GetTaskRepository()
        {
            return taskRepository;
        }

        public async System.Threading.Tasks.Task<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId, TaskRepository taskRepository)
        {
            var project = await projectRepository.GetProjectByIdAsync(projectId);
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
            return await projectRepository.GetAllProjectsAsync();
        }

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
        {
            if (ProjectService.IsProjectNameUnique(project.ProjectName))
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

        private static bool IsProjectNameUnique(string projectName)
        {
            // ... (Your implementation to check for uniqueness) ...
            return true; // Or false if the name is not unique
        }
    }
}