using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class SalesUnitEmptyWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitEmptyWrapper(SalesUnit model) : base(model) { }
    }
}