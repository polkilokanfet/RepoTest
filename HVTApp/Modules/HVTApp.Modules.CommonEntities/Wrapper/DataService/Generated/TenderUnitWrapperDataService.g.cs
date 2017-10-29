using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderUnitWrapperDataService : WrapperDataService<TenderUnit, TenderUnitWrapper>
    {
        public TenderUnitWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TenderUnitWrapper GenerateWrapper(TenderUnit model)
        {
            return new TenderUnitWrapper(model);
        }
    }
}
