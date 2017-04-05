using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class SpecificationWrapper
    {
        protected override void RunInConstructor()
        {
            PaymentsConditions = new PaymentsConditionWrappersCollection(Model.PaymentsConditions.Select(PaymentsConditionWrapper.GetWrapper));
        }
    }
}
