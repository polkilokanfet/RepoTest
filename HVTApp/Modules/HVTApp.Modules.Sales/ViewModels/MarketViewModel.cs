using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetProductService _getProductService;

        public ObservableCollection<ProjectWrapper> Projects { get; } = new ObservableCollection<ProjectWrapper>();

        public ObservableCollection<OfferWrapper> Offers { get; } = new ObservableCollection<OfferWrapper>();
        public ObservableCollection<TenderLookup> Tenders { get; } = new ObservableCollection<TenderLookup>();

        public ICommand GetProductCommand { get; }

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

        public MarketViewModel(IUnitOfWork unitOfWork, IGetProductService getProductService)
        {
            _unitOfWork = unitOfWork;
            _getProductService = getProductService;

            GetProductCommand = new DelegateCommand(GetProductCommandExecute);
        }

        private async void GetProductCommandExecute()
        {
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            var i = new Random().Next(products.Count);
            await _getProductService.GetProductAsync(products[i]);
        }

        public async Task LoadAsync()
        {
            var projects = await _unitOfWork.GetRepository<Project>().GetAllAsync();
            var projectWrappers = projects.Select(x => new ProjectWrapper(x));

            _offers.AddRange(_unitOfWork.GetRepository<Offer>().Find(x => projects.Contains(x.Project)).Select(x => new OfferWrapper(x)));
            _tenders.AddRange(_unitOfWork.GetRepository<Tender>().Find(x => projects.Contains(x.Project)).Select(x => new TenderLookup(x)));

            Projects.Clear();
            Projects.AddRange(projectWrappers);
        }
    }
}
