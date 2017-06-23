using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class CompanyTests
    {
        [TestMethod]
        public void CompanyParentChildRelations()
        {
            var parent = WrappersFactory.GetWrapper<CompanyWrapper>(new Company {FullName = "ParentCompany"});
            var child1 = WrappersFactory.GetWrapper<CompanyWrapper>(new Company { FullName = "ChildCompany1" });
            var child2 = WrappersFactory.GetWrapper<CompanyWrapper>(new Company { FullName = "ChildCompany2" });

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
