using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.Services.ChooseProductService
{
    class ChooseProductService : IChooseProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ProductParameterWrapper> _productParameterWrappers;

        public ChooseProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _productParameterWrappers = new List<ProductParameterWrapper>(unitOfWork.ProductParameters.GetAll().Select(ProductParameterWrapper.GetWrapper));
        }


        public Product ChooseProduct()
        {
            throw new System.NotImplementedException();
        }
    }
}
