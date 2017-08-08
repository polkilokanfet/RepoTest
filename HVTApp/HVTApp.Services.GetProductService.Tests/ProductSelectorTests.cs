using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class ProductSelectorTests
    {
        private List<ParameterGroup> _groups;
        private Parameter _breaker, _transformator, _v110, _v220;
        private ProductSelector _productSelector;

        [TestInitialize]
        public void Init()
        {
            
            _breaker = new Parameter();
            _transformator = new Parameter();
            ParameterGroup eqType = new ParameterGroup().AddParameters(new[] { _breaker, _transformator });

            _v110 = new Parameter().AddRequiredPreviousParameters(new [] {_breaker}); _v110.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { _breaker } });
            _v220 = new Parameter(); _v220.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { _breaker } });
            ParameterGroup voltage = new ParameterGroup().AddParameters(new[] { _v110, _v220 });

            _groups = new List<ParameterGroup> { eqType, voltage };

            _productSelector = new ProductSelector(_groups.Select(x => x.Parameters), new List<Product>());
        }
    }
}