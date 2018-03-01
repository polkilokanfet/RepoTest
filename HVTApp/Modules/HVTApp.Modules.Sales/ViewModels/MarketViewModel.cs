using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<ProjectLookup> Projects { get; } = new ObservableCollection<ProjectLookup>();

        public ObservableCollection<UnitGroup> UnitGroups { get; } = new ObservableCollection<UnitGroup>();

        public ObservableCollection<TenderLookup> Tenders { get; } = new ObservableCollection<TenderLookup>();
        public ObservableCollection<OfferLookup> Offers { get; } = new ObservableCollection<OfferLookup>();

        private ProjectLookup _selectedProject;
        public ProjectLookup SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (Equals(_selectedProject, value)) return;
                _selectedProject = value;

                UnitGroups.Clear();
                UnitGroups.AddRange(_selectedProject.Entity.SalesUnits.Select(x => new SalesUnitWrapper(x)).ToUnitGroups());

                Tenders.Clear();
                Tenders.AddRange(_tenders.Where(x => Equals(x.Project.Entity, _selectedProject.Entity)));

                Offers.Clear();
                Offers.AddRange(_offers.Where(x => Equals(x.Project.Entity, _selectedProject.Entity)));
            }
        }

        public TenderLookup SelectedTender { get; set; }
        public OfferLookup SelectedOffer { get; set; }

        public MarketViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly List<SalesUnitLookup> _salesUnits = new List<SalesUnitLookup>();
        private readonly List<OfferLookup> _offers = new List<OfferLookup>();
        private readonly List<TenderLookup> _tenders = new List<TenderLookup>();

        public async Task LoadAsync()
        {
            var projects = await _unitOfWork.GetRepository<Project>().GetAllAsync();
            var projectLookups = projects.Select(x => new ProjectLookup(x)).OrderBy(x => x);

            _salesUnits.AddRange(projects.SelectMany(x => x.SalesUnits).Select(x => new SalesUnitLookup(x)));
            _offers.AddRange(_unitOfWork.GetRepository<Offer>().Find(x => projects.Contains(x.Project)).Select(x => new OfferLookup(x)));
            _tenders.AddRange(_unitOfWork.GetRepository<Tender>().Find(x => projects.Contains(x.Project)).Select(x => new TenderLookup(x)));

            Projects.Clear();
            Projects.AddRange(projectLookups);
            SelectedProject = Projects.FirstOrDefault();
        }
    }
}
