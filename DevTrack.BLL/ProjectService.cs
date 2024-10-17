// ProjectService.cs
using DevTrack.DAL.Models;
using DevTrack.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace DevTrack.BLL
{
    public class ProjectService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly MilestoneRepository _milestoneRepository;
        private readonly DocumentRepository _documentRepository;

        public ProjectService(
            ProjectRepository projectRepository,
            TaskRepository taskRepository,
            MilestoneRepository milestoneRepository,
            DocumentRepository documentRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _milestoneRepository = milestoneRepository;
            _documentRepository = documentRepository;
        }

        public TaskRepository Get_taskRepository()
        {
            return _taskRepository;
        }

        public async System.Threading.Tasks.Task<ProjectDashboardData> GetProjectDashboardDataAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            var milestones = await _milestoneRepository.GetAllMilestonesAsync();
            var documents = await _documentRepository.GetAllDocumentsAsync();

            return new ProjectDashboardData
            {
                Project = project,
                Tasks = tasks,
                Milestones = milestones,
                Documents = documents
            };
        }

        public async System.Threading.Tasks.Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        public async System.Threading.Tasks.Task CreateProjectAsync(Project project)
        {
            if (IsProjectNameUnique(project.ProjectName))
            {
                await _projectRepository.CreateProjectAsync(project);
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
                var existingProject = await _projectRepository.GetProjectByIdAsync(project.ProjectID);
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                await _projectRepository.UpdateProjectAsync(project);
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
                var existingProject = await _projectRepository.GetProjectByIdAsync(projectId);
                if (existingProject == null)
                {
                    throw new Exception("Project not found.");
                }

                await _projectRepository.DeleteProjectAsync(projectId);
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