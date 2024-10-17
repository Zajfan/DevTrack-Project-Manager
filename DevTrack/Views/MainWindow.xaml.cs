// MainWindow.xaml.cs
using DevTrack.BLL;
using DevTrack.DAL.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace DevTrack.Views
{
    public partial class MainWindow : ContentPage
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;

        public ObservableCollection<Project> Projects { get; set; } = new();
        public ObservableCollection<DevTrack.DAL.Models.Task> Tasks { get; set; } = new();

        public MainWindow(ProjectService projectService, TaskService taskService)
        {
            InitializeComponent();
            _projectService = projectService;
            _taskService = taskService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProjectsAsync();
            await LoadTasksAsync();
        }

        private async Task LoadProjectsAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        private async Task LoadTasksAsync()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private async void createProjectButton_Click(object sender, EventArgs e)
        {
            // 1. Create a new Project object (get data from input fields)
            var newProject = new Project
            {
                ProjectName = "New Project",
                ProjectStage = "Planning",
                // ... set other properties
            };

            try
            {
                await _projectService.CreateProjectAsync(newProject);
                await LoadProjectsAsync(); // Refresh the project list
            }
            catch (Exception ex)
            {
                // Display an error message to the user
                await DisplayAlert("Error", $"Error creating project: {ex.Message}", "OK");
            }
        }

        private async void updateProjectButton_Click(object sender, EventArgs e)
        {
            // 1. Get the selected project (implementation depends on your UI)
            var selectedProject = projectsDataGrid.SelectedItem as Project; // Assuming you have a DataGrid
            if (selectedProject == null)
            {
                await DisplayAlert("Error", "Please select a project to update.", "OK");
                return;
            }

            // 2. (Optional) Open a window or dialog to edit the project details

            try
            {
                await _projectService.UpdateProjectAsync(selectedProject);
                await LoadProjectsAsync(); // Refresh the project list
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error updating project: {ex.Message}", "OK");
            }
        }

        private async void deleteProjectButton_Click(object sender, EventArgs e)
        {
            // 1. Get the selected project (implementation depends on your UI)
            var selectedProject = projectsDataGrid.SelectedItem as Project; // Assuming you have a DataGrid
            if (selectedProject == null)
            {
                await DisplayAlert("Error", "Please select a project to delete.", "OK");
                return;
            }

            // 2. (Optional) Ask for confirmation before deleting

            try
            {
                await _projectService.DeleteProjectAsync(selectedProject.ProjectID);
                await LoadProjectsAsync(); // Refresh the project list
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error deleting project: {ex.Message}", "OK");
            }
        }

        // ... (Add event handlers for Task buttons: createTaskButton_Click, updateTaskButton_Click, deleteTaskButton_Click) ...
    }
}