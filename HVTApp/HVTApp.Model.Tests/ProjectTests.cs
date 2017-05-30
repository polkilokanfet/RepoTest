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
        public void ProjectSalesUnitsGroups()
        {
            Parameter p1 = new Parameter();
            Parameter p2 = new Parameter();

            SalesUnit su10 = new SalesUnit { ProductionUnit = new ProductionUnit { Product = new Product { Parameters = new List<Parameter>(new[] { p1 }) } }, CostSingle = new SumAndVat() };
            SalesUnit su11 = new SalesUnit { ProductionUnit = new ProductionUnit { Product = new Product { Parameters = new List<Parameter>(new[] { p1 }) } }, CostSingle = new SumAndVat() };
            SalesUnit su12 = new SalesUnit { ProductionUnit = new ProductionUnit { Product = new Product { Parameters = new List<Parameter>(new[] { p1 }) } }, CostSingle = new SumAndVat() };
            SalesUnit su20 = new SalesUnit { ProductionUnit = new ProductionUnit { Product = new Product { Parameters = new List<Parameter>(new[] { p2 }) } }, CostSingle = new SumAndVat() };
            SalesUnit su21 = new SalesUnit { ProductionUnit = new ProductionUnit { Product = new Product { Parameters = new List<Parameter>(new[] { p2 }) } }, CostSingle = new SumAndVat() };

            Project project = new Project() {SalesUnits = new List<SalesUnit>(new []{su10, su11, su12, su20, su21})};
            ProjectWrapper projectWrapper = new ProjectWrapper(project);

            Assert.AreEqual(projectWrapper.SalesUnitsGroups.Count, 2);
            Assert.AreEqual(projectWrapper.SalesUnitsGroups.First().SalesUnits.Count, 3);
        }
    }
}
