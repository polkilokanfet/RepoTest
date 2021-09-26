using System;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HVTApp.UI;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pi = typeof(Specification).GetProperty(nameof(Specification.Date));
            Assert.AreEqual("Дата", pi.Designation());
            pi = typeof(Specification).GetProperty(nameof(Specification.Contract));
            Assert.AreEqual("Договор", pi.Designation());
        }
    }
}
