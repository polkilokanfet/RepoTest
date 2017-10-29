using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderWrapperDataService : WrapperDataService<Tender, TenderWrapper>
    {
        public TenderWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TenderWrapper GenerateWrapper(Tender model)
        {
            return new TenderWrapper(model);
        }
    }
}
