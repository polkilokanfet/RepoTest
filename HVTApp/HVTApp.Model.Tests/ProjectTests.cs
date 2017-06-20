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
        [TestMethod]
        public void ProjectUnitsGroups()
        {
            Parameter p1 = new Parameter();
            Parameter p2 = new Parameter();

            Unit u10 = new Unit { ProductionsUnit = new ProductionsUnit { Product = new Product { ProductItem = new ProductItem { Parameters = new List<Parameter>(new[] { p1 }) } } }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u11 = new Unit { ProductionsUnit = new ProductionsUnit { Product = new Product { ProductItem = new ProductItem { Parameters = new List<Parameter>(new[] { p1 }) } } }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u12 = new Unit { ProductionsUnit = new ProductionsUnit { Product = new Product { ProductItem = new ProductItem { Parameters = new List<Parameter>(new[] { p1 }) } } }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u20 = new Unit { ProductionsUnit = new ProductionsUnit { Product = new Product { ProductItem = new ProductItem { Parameters = new List<Parameter>(new[] { p2 }) } } }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };
            Unit u21 = new Unit { ProductionsUnit = new ProductionsUnit { Product = new Product { ProductItem = new ProductItem { Parameters = new List<Parameter>(new[] { p2 }) } } }, SalesUnit = new SalesUnit { Cost = new SumAndVat { Sum = 10, Vat = 1 } } };

            Project project = new Project {Units = new List<Unit>(new[] {u10, u11, u12, u20, u21})};
            ProjectWrapper projectWrapper = WrappersFactory.GetWrapper <Project, ProjectWrapper> (project);

            Assert.AreEqual(projectWrapper.UnitsGroups.Count, 2);
            Assert.AreEqual(projectWrapper.UnitsGroups.First().Count, 3);
        }
    }
}
