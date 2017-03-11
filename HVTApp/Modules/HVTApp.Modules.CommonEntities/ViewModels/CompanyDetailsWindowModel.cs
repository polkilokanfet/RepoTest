using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrapper;

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
        }

        public CompanyWrapper CompanyWrapper { get; set; }

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}
