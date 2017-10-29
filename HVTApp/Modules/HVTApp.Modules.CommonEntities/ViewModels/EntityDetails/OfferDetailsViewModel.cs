using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;

        public ICommand AddProjectCommand { get; }
        public ICommand RemoveProjectCommand { get; }
        public ICommand AddTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }

        public OfferDetailsViewModel(OfferWrapper item, IUnitOfWork unitOfWork, ISelectService selectService, IUnityContainer container) : base(container)
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
