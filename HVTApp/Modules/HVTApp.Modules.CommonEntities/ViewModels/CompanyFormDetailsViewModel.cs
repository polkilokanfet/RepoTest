using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm>
    {
        public CompanyFormDetailsViewModel(CompanyFormWrapper item) : base(item)
        {
        }
    }
}
