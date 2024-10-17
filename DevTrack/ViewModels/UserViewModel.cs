using Android.SE.Omapi;
using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DevTrack.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly UserService _userService; // You'll need to create this service
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserViewModel(UserService userService)
        {
            _userService = userService;
            Users = new ObservableCollection<User>();
        }

        public async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync(); // Implement in UserService
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        // Add methods for CreateUserAsync, UpdateUserAsync, DeleteUserAsync
    }
}