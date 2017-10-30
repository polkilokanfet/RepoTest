using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ShipmentUnitWrapperDataService : WrapperDataService<ShipmentUnit, ShipmentUnitWrapper>
    {
        public ShipmentUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ShipmentUnitWrapper GenerateWrapper(ShipmentUnit model)
        {
            return new ShipmentUnitWrapper(model);
        }
    }
}
