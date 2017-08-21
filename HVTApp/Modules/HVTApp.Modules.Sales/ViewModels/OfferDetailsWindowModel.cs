using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferDetailsWindowModel : BaseDetailsViewModel<OfferWrapper>
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
        }

        private void RemoveTenderCommand_Execute()
        {
        }

        private void AddProjectCommand_Execute()
        {
        }

        private void RemoveProjectCommand_Execute()
        {
        }
    }
}
