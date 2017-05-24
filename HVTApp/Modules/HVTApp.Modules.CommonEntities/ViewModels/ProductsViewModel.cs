using System;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductsViewModel : EditableSelectableBindableBase<ProductWrapper>
    {
        public ProductsViewModel(IUnitOfWork unitOfWork)
        {
           unitOfWork.Products.GetAll().ForEach(Items.Add);
        }

        protected override void RemoveItemCommand_Execute()
        {
            throw new NotImplementedException();
        }

        protected override void EditItemCommand_Execute()
        {
            throw new NotImplementedException();
        }

        protected override void NewItemCommand_Execute()
        {
            throw new NotImplementedException();
        }
    }
}
