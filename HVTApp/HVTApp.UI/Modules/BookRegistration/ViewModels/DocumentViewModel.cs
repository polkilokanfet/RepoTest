using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class DocumentViewModel : DocumentDetailsViewModel
    {
        public DocumentViewModel(IUnityContainer container) : base(container)
        {
        }
        
        public async Task LoadAsync2(Document document)
        {
            await this.LoadAsync(new Document());

            if (document.Author != null)
                Item.Author = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(document.Author.Id));

            if (document.SenderEmployee != null)
                Item.SenderEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(document.SenderEmployee.Id));

            if (document.RecipientEmployee != null)
                Item.RecipientEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(document.RecipientEmployee.Id));
        }

        protected override async Task AfterLoading()
        {

            await base.AfterLoading();
        }
    }
}