using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DevTrack.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;

        public ObservableCollection<Project> Projects { get; set; } = new();
        public ObservableCollection<DevTrack.DAL.Models.Task> Tasks { get; set; } = new();

        public MainWindowViewModel(ProjectService projectService, TaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        public async Task LoadProjectsAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        public async Task LoadTasksAsync()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}