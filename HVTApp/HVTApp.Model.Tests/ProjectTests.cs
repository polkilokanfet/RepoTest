using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Tests.Factory;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class ProjectTests
    {
        private Parameter _parameter1, _parameter2, _parameter3, _parameter4, _parameter5, _parameter6, _parameter7;
        private Product _productItem1, _productItem2, _productItem3, _productItem4;
        private Equipment _product1, _product2, _product3, _product4;

        [TestInitialize]
        public void Init()
        {
            _parameter1 = new Parameter { Value = "parameter1" };
            _parameter2 = new Parameter { Value = "parameter2" };
            _parameter3 = new Parameter { Value = "parameter3" };
            _parameter4 = new Parameter { Value = "parameter4" };
            _parameter5 = new Parameter { Value = "parameter5" };
            _parameter6 = new Parameter { Value = "parameter6" };
            _parameter7 = new Parameter { Value = "parameter7" };

            _productItem1 = new Product { Designation = "ProductItem1", Parameters = new List<Parameter> { _parameter1, _parameter2 } };
            _productItem2 = new Product { Designation = "ProductItem2", Parameters = new List<Parameter> { _parameter3, _parameter4 } };
            _productItem3 = new Product { Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter5, _parameter6 } };
            _productItem4 = new Product { Designation = "ProductItem3", Parameters = new List<Parameter> { _parameter7 } };

            _product1 = new Equipment { Product = _productItem1 };
            _product2 = new Equipment { Product = _productItem2 };
            _product3 = new Equipment { Product = _productItem3, DependentEquipments = new List<Equipment> { _product1, _product2 } };
            _product4 = new Equipment { Product = _productItem3, DependentEquipments = new List<Equipment> { _product2, _product1 } };
        }

        [TestMethod]
        public void ProjectUnitsGroups()
        {
            //Currency rub = new Currency();
            //Currency usd = new Currency();

            //ProductComplexUnit u10 = new ProductComplexUnit { Equipment = _product3, Cost = new Cost { Sum = 10, Currency = rub } };
            //ProductComplexUnit u11 = new ProductComplexUnit { Equipment = _product3, Cost = new Cost { Sum = 20, Currency = rub } };
            //ProductComplexUnit u12 = new ProductComplexUnit { Equipment = _product3, Cost = new Cost { Sum = 10, Currency = rub } };
            //ProductComplexUnit u20 = new ProductComplexUnit { Equipment = _product4, Cost = new Cost { Sum = 10, Currency = rub } };
            //ProductComplexUnit u21 = new ProductComplexUnit { Equipment = _product4, Cost = new Cost { Sum = 10, Currency = rub } };
            //ProductComplexUnit u22 = new ProductComplexUnit { Equipment = _product4, Cost = new Cost { Sum = 10, Currency = usd } };

            //Project project = new Project { ProjectUnits = new List<ProductComplexUnit>(new[] { u10, u11, u12, u20, u21, u22 }) };
            //ProjectWrapper projectWrapper = TestWrappersFactory.GetWrapper<ProjectWrapper>(project);

            //Assert.AreEqual(projectWrapper.ProductsUnitsGroups.Count, 4);
            //Assert.AreEqual(projectWrapper.ProductsUnitsGroups.Where(x => x.Equipment.Model.Equals(_product3)).ToList().Count, 2);
            //Assert.AreEqual(projectWrapper.ProductsUnitsGroups.Where(x => x.Cost.Currency.Model.Equals(usd)).ToList().Count, 1);
        }
    }
}
