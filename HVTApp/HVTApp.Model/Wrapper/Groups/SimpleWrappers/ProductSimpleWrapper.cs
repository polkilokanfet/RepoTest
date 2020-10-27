using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper.Groups.SimpleWrappers
{
    public class ProductSimpleWrapper : WrapperBase<Product>
    {
        public ProductSimpleWrapper(Product model) : base(model)
        {
        }
    }
}