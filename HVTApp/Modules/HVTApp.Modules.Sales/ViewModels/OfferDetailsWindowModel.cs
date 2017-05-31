using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferDetailsWindowModel : BaseDetailsViewModel<OfferWrapper, Offer>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;

        public ICommand AddProjectCommand { get; }
        public ICommand RemoveProjectCommand { get; }
        public ICommand AddTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }

        public OfferDetailsWindowModel(OfferWrapper item, IUnitOfWork unitOfWork, ISelectService selectService) : base(item)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;

            AddProjectCommand = new DelegateCommand(AddProjectCommand_Execute);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute);
            AddTenderCommand = new DelegateCommand(AddTenderCommand_Execute);
            RemoveTenderCommand = new DelegateCommand(RemoveTenderCommand_Execute);
        }

        private void AddTenderCommand_Execute()
        {
            var tenders = _unitOfWork.Tenders.GetAll();
            if (Item.Project != null) tenders = tenders.Where(x => x.Project == Item.Project).ToList();
            var tender = _selectService.SelectItem(tenders, Item.Tender);
            if (tender != null) Item.Tender = tender;
        }

        private void RemoveTenderCommand_Execute()
        {
            Item.Tender = null;
        }

        private void AddProjectCommand_Execute()
        {
            var projects = _unitOfWork.Projects.GetAll();
            var project = _selectService.SelectItem(projects, Item.Project);
            if (project != null) Item.Project = project;
        }

        private void RemoveProjectCommand_Execute()
        {
            Item.Project = null;
        }
    }
}
