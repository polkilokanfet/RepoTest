using System;
using HVTApp.Model.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class SalesProductUnitWrapperTests
    {
        private SalesProductUnitWrapper _unitWrapper;
        [TestInitialize]
        public void InitialMethod()
        {
            var unit = new SalesProductUnit {Payments = new Payments()};
            unit.CostInfo = new CostInfo {Cost = 100, CostPrice = 50, Vat = 10};
            _unitWrapper = SalesProductUnitWrapper.GetWrapper(unit);
        }

        [TestMethod]
        public void SalesProductUnitWrapperReactionOnActualPayment()
        {
        }


    }
}
