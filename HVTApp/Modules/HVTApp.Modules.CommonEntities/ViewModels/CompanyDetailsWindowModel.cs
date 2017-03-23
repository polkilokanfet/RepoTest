using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrapper;
using Prism.Commands;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyDetailsWindowModel : IDialogRequestClose
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyDetailsWindowModel(CompanyWrapper companyWrapper, IUnitOfWork unitOfWork)
        {
            CompanyWrapper = companyWrapper;
            _unitOfWork = unitOfWork;

            Forms = new ObservableCollection<CompanyFormWrapper>(_unitOfWork.CompanyForms.GetAll().Select(x => new CompanyFormWrapper(x)));

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);

            CompanyWrapper.PropertyChanged += CompanyWrapperOnPropertyChanged;
        }

        private void CompanyWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OkCommand.RaiseCanExecuteChanged();
        }

        private void OkCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            CompanyWrapper.AcceptChanges();
            _unitOfWork.Complete();
        }

        private bool OkCommand_CanExecute()
        {
            return CompanyWrapper.IsChanged && CompanyWrapper.IsValid;
        }

        public DelegateCommand OkCommand { get; }

        public CompanyWrapper CompanyWrapper { get; set; }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
