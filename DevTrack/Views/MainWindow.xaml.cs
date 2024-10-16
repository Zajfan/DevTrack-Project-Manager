// MainWindow.xaml.cs
using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Windows;

namespace DevTrack.Views
{
    public partial class MainWindow : Window
    {
        private ProjectService projectService;
        private TaskService taskService; // Add other services as needed

        public MainWindow(ProjectService projectService, TaskService taskService /*, ... other services */)
        {
            InitializeComponent();
            this.projectService = projectService;
            this.taskService = taskService;
            // ... initialize other services
            DataContext = this;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            projectsDataGrid.ItemsSource = await projectService.GetAllProjectsAsync();
            tasksDataGrid.ItemsSource = await taskService.GetAllTasksAsync();
            // ... load data for other DataGrids
        }

        private async void createProjectButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Create a new Project object (potentially get data from input fields)
            var newProject = new Project
            {
                ProjectName = "New Project",
                ProjectStage = "Planning",
                // ... set other properties
            };

            try
            {
                await projectService.CreateProjectAsync(newProject);
                projectsDataGrid.ItemsSource = await projectService.GetAllProjectsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating project: {ex.Message}");
            }
        }

        private async void updateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Get the selected project from the DataGrid
            var selectedProject = projectsDataGrid.SelectedItem as Project;
            if (selectedProject == null)
            {
                MessageBox.Show("Please select a project to update.");
                return;
            }

            // 2. (Optional) Open a window or dialog to edit the project details

            try
            {
                await projectService.UpdateProjectAsync(selectedProject);
                projectsDataGrid.ItemsSource = await projectService.GetAllProjectsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating project: {ex.Message}");
            }
        }

        private async void deleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Get the selected project from the DataGrid
            var selectedProject = projectsDataGrid.SelectedItem as Project;
            if (selectedProject == null)
            {
                MessageBox.Show("Please select a project to delete.");
                return;
            }

            // 2. (Optional) Ask for confirmation before deleting

            try
            {
                await projectService.DeleteProjectAsync(selectedProject.ProjectID);
                projectsDataGrid.ItemsSource = await projectService.GetAllProjectsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting project: {ex.Message}");
            }
        }

        // ... (Add event handlers for Task buttons: createTaskButton_Click, updateTaskButton_Click, deleteTaskButton_Click) ...
    }
}