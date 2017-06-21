using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProjectTests
    {
        private Parameter _parameter1, _parameter2, _parameter3, _parameter4, _parameter5, _parameter6, _parameter7;
        private ProductItem _productItem1, _productItem2, _productItem3, _productItem4;

        [TestInitialize]
        public void Init()
        {
            _parameter1 = new Parameter { Id = 1, Value = "parameter1" };
            _parameter2 = new Parameter { Id = 2, Value = "parameter2" };
            _parameter3 = new Parameter { Id = 3, Value = "parameter3" };
            _parameter4 = new Parameter { Id = 4, Value = "parameter4" };
            _parameter5 = new Parameter { Id = 5, Value = "parameter5" };
            _parameter6 = new Parameter { Id = 6, Value = "parameter6" };
            _parameter7 = new Parameter { Id = 7, Value = "parameter7" };

            _productItem1 = new ProductItem { Id = 1, Designation = "ProductItem1", Parameters = new List<Parameter> { _parameter1, _parameter2 } };
            _productItem2 = new ProductItem { Id = 2, Designation = "ProductItem2", Parameters = new List<Parameter> { _parameter3, _parameter4 } };
            _productItem3 = new ProductItem { Id = 3, Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter5, _parameter6 } };
            _productItem4 = new ProductItem { Id = 4, Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter7 } };
        }

        [TestMethod]
        public void ProjectUnitsGroups()
        {
            Product product1 = new Product { ProductItem = _productItem1 };
            Product product2 = new Product { ProductItem = _productItem2 };
            Product product3 = new Product { ProductItem = _productItem3, ChildProducts = new List<Product> { product1, product2 } };
            Product product4 = new Product { ProductItem = _productItem3, ChildProducts = new List<Product> { product2, product1 } };

            Unit u10 = new Unit { ProductionsUnit = new ProductionsUnit { Product = product3 }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u11 = new Unit { ProductionsUnit = new ProductionsUnit { Product = product3 }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u12 = new Unit { ProductionsUnit = new ProductionsUnit { Product = product3 }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u20 = new Unit { ProductionsUnit = new ProductionsUnit { Product = product4 }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u21 = new Unit { ProductionsUnit = new ProductionsUnit { Product = product4 }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };

            Project project = new Project {Units = new List<Unit>(new[] {u10, u11, u12, u20, u21})};
            ProjectWrapper projectWrapper = WrappersFactory.GetWrapper <Project, ProjectWrapper> (project);

            Assert.AreEqual(projectWrapper.UnitsGroups.Count, 2);
            Assert.AreEqual(projectWrapper.UnitsGroups.First().Count, 3);
        }
    }
}
