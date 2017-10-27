using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PersonWrapperDataService : WrapperDataService<Person, PersonWrapper>
    {
        public PersonWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override PersonWrapper GenerateWrapper(Person model)
        {
            return new PersonWrapper(model);
        }
    }
}
