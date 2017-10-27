using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CostOnDateWrapperDataService : WrapperDataService<CostOnDate, CostOnDateWrapper>
    {
        public CostOnDateWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override CostOnDateWrapper GenerateWrapper(CostOnDate model)
        {
            return new CostOnDateWrapper(model);
        }
    }
}
