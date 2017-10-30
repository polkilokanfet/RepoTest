using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class LocalityWrapperDataService : WrapperDataService<Locality, LocalityWrapper>
    {
        public LocalityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override LocalityWrapper GenerateWrapper(Locality model)
        {
            return new LocalityWrapper(model);
        }
    }
}
