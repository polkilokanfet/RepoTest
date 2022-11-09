using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class HashTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            var parameter1 = new Parameter();
            var parameter2 = new Parameter();

            Assert.AreNotEqual(parameter1, parameter2);
        }
    }
}