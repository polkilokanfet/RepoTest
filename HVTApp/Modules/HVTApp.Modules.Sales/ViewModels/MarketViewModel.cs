using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<ProjectWrapper> Projects { get; } = new ObservableCollection<ProjectWrapper>();

        public ObservableCollection<OfferWrapper> Offers { get; } = new ObservableCollection<OfferWrapper>();
        public ObservableCollection<TenderLookup> Tenders { get; } = new ObservableCollection<TenderLookup>();

        private readonly List<OfferWrapper> _offers = new List<OfferWrapper>();
        private readonly List<TenderLookup> _tenders = new List<TenderLookup>();

        private ProjectWrapper _selectedProject;
        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (Equals(_selectedProject, value)) return;
                _selectedProject = value;

                Tenders.Clear();
                Tenders.AddRange(_tenders.Where(x => Equals(x.Project.Entity, _selectedProject.Model)));

                Offers.Clear();
                Offers.AddRange(_offers.Where(x => Equals(x.Project.Model, _selectedProject.Model)));
            }
        }

        public OfferWrapper SelectedOffer { get; set; }
        public TenderLookup SelectedTender { get; set; }

        public MarketViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            var projects = await _unitOfWork.GetRepository<Project>().GetAllAsync();
            var projectWrappers = projects.Select(x => new ProjectWrapper(x));

            _offers.AddRange((await _unitOfWork.GetRepository<Offer>().FindAsync(x => projects.Contains(x.Project))).Select(x => new OfferWrapper(x)));
            _tenders.AddRange((await _unitOfWork.GetRepository<Tender>().FindAsync(x => projects.Contains(x.Project))).Select(x => new TenderLookup(x)));

            Projects.Clear();
            Projects.AddRange(projectWrappers);
        }
    }
}
