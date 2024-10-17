using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DevTrack.ViewModels
{
    public class MilestoneViewModel : ViewModelBase
    {
        private readonly MilestoneService _milestoneService; // You'll need to create this service
        private ObservableCollection<Milestone> _milestones;

        public ObservableCollection<Milestone> Milestones
        {
            get => _milestones;
            set
            {
                _milestones = value;
                OnPropertyChanged(nameof(Milestones));
            }
        }

        public MilestoneViewModel(MilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
            Milestones = new ObservableCollection<Milestone>();
        }

        public async Task LoadMilestonesAsync()
        {
            var milestones = await _milestoneService.GetAllMilestonesAsync(); // Implement in MilestoneService
            Milestones.Clear();
            foreach (var milestone in milestones)
            {
                Milestones.Add(milestone);
            }
        }

        // Add methods for CreateMilestoneAsync, UpdateMilestoneAsync, DeleteMilestoneAsync
    }
}