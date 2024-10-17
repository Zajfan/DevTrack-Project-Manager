using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DevTrack.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly TaskService _taskService;
        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskViewModel(TaskService taskService)
        {
            _taskService = taskService;
            Tasks = new ObservableCollection<Task>();
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

        // Add methods for CreateTaskAsync, UpdateTaskAsync, DeleteTaskAsync
    }
}