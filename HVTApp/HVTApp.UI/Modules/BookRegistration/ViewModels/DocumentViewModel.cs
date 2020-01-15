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
        
        public void Load2(Document document)
        {
            this.Load(new Document());

            if (document.Author != null)
                Item.Author = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.Author.Id));

            if (document.SenderEmployee != null)
                Item.SenderEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.SenderEmployee.Id));

            if (document.RecipientEmployee != null)
                Item.RecipientEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.RecipientEmployee.Id));
        }

   }
}