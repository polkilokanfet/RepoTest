using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderTypeWrapperDataService : WrapperDataService<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderTypeWrapper GenerateWrapper(TenderType model)
        {
            return new TenderTypeWrapper(model);
        }
    }
}
