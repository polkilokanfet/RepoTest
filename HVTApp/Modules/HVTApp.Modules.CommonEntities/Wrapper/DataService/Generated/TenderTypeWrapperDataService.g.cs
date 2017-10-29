using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderTypeWrapperDataService : WrapperDataService<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TenderTypeWrapper GenerateWrapper(TenderType model)
        {
            return new TenderTypeWrapper(model);
        }
    }
}
