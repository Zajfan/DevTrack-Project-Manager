using DevTrack.BLL;
using DevTrack.DAL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DevTrack.ViewModels
{
    public class DocumentViewModel : ViewModelBase
    {
        private readonly DocumentService _documentService; // You'll need to create this service
        private ObservableCollection<Document> _documents;

        public ObservableCollection<Document> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged(nameof(Documents));
            }
        }

        public DocumentViewModel(DocumentService documentService)
        {
            _documentService = documentService;
            Documents = new ObservableCollection<Document>();
        }

        public async Task LoadDocumentsAsync()
        {
            var documents = await _documentService.GetAllDocumentsAsync(); // Implement in DocumentService
            Documents.Clear();
            foreach (var document in documents)
            {
                Documents.Add(document);
            }
        }

        // Add methods for CreateDocumentAsync, UpdateDocumentAsync, DeleteDocumentAsync
    }
}