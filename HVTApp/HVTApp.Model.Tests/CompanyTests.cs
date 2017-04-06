using System;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class CompanyTests
    {
        [TestMethod]
        public void CompanyParentChildRelations()
        {
            var parent = CompanyWrapper.GetWrapper();
            var child1 = CompanyWrapper.GetWrapper();
            var child2 = CompanyWrapper.GetWrapper();

            child1.ParentCompany = parent;
            Assert.IsTrue(parent.ChildCompanies.Contains(child1));

            child2.ParentCompany = parent;
            Assert.IsTrue(parent.ChildCompanies.Contains(child2));

            child1.ParentCompany = null;
            Assert.IsFalse(parent.ChildCompanies.Contains(child1));

            parent.ChildCompanies.Add(child1);
            Assert.AreEqual(parent, child1.ParentCompany);
        }
    }
}
