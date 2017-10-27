using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ActivityFieldWrapperDataService : WrapperDataService<ActivityField, ActivityFieldWrapper>
    {
        public ActivityFieldWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ActivityFieldWrapper GenerateWrapper(ActivityField model)
        {
            return new ActivityFieldWrapper(model);
        }
    }
}
