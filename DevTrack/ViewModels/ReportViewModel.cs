using System.Collections.ObjectModel;

namespace DevTrack.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly ReportingService _reportingService; // You'll need to create this service
        private ObservableCollection<ProjectProgressReport> _projectProgressReports;
        private ObservableCollection<UserWorkloadReport> _userWorkloadReports;
        // ... add other report collections

        public ObservableCollection<ProjectProgressReport> ProjectProgressReports
        {
            get => _projectProgressReports;
            set
            {
                _projectProgressReports = value;
                OnPropertyChanged(nameof(ProjectProgressReports));
            }
        }

        public ObservableCollection<UserWorkloadReport> UserWorkloadReports
        {
            get => _userWorkloadReports;
            set
            {
                _userWorkloadReports = value;
                OnPropertyChanged(nameof(UserWorkloadReports));
            }
        }

        // ... add other report properties

        public ReportViewModel(ReportingService reportingService)
        {
            _reportingService = reportingService;
            ProjectProgressReports = new ObservableCollection<ProjectProgressReport>();
            UserWorkloadReports = new ObservableCollection<UserWorkloadReport>();
            // ... initialize other report collections
        }

        public async Task LoadProjectProgressReportAsync()
        {
            var reports = await _reportingService.GetProjectProgressReportAsync(); // Implement in ReportingService
            ProjectProgressReports.Clear();
            foreach (var report in reports)
            {
                ProjectProgressReports.Add(report);
            }
        }

        public async Task LoadUserWorkloadReportAsync()
        {
            var reports = await _reportingService.GetUserWorkloadReportAsync(); // Implement in ReportingService
            UserWorkloadReports.Clear();
            foreach (var report in reports)
            {
                UserWorkloadReports.Add(report);
            }
        }

        // ... add methods to load other reports
    }
}