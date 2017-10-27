using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class MeasureWrapperDataService : WrapperDataService<Measure, MeasureWrapper>
    {
        public MeasureWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override MeasureWrapper GenerateWrapper(Measure model)
        {
            return new MeasureWrapper(model);
        }
    }
}
