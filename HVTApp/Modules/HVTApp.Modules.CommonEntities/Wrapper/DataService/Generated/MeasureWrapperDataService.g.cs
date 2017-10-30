using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class MeasureWrapperDataService : WrapperDataService<Measure, MeasureWrapper>
    {
        public MeasureWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override MeasureWrapper GenerateWrapper(Measure model)
        {
            return new MeasureWrapper(model);
        }
    }
}
