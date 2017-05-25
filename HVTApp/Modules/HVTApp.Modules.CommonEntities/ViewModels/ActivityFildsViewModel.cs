using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildsViewModel : EditableSelectableBindableBase<ActivityFieldWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityFildsViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _unitOfWork.ActivityFields.GetAll().ForEach(Items.Add);
        }

        #region Commands

        protected override bool RemoveItemCommand_CanExecute()
        {
            return true;
        }

        protected override void RemoveItemCommand_Execute()
        {
            
        }

        protected override void EditItemCommand_Execute()
        {
            
        }

        protected override bool NewItemCommand_CanExecute()
        {
            return true;
        }

        protected override void NewItemCommand_Execute()
        {
            
        }
        

        #endregion

    }
}
