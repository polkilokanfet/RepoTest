using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OrderWrapperDataService : WrapperDataService<Order, OrderWrapper>
    {
        public OrderWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OrderWrapper GenerateWrapper(Order model)
        {
            return new OrderWrapper(model);
        }
    }
}
