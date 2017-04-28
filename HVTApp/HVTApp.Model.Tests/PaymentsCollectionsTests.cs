using System;
using System.Linq;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class PaymentsCollectionsTests
    {
        SpecificationWrapper _specification;
        [TestInitialize]
        public void InitialMethod()
        {
            _specification = new SpecificationWrapper();
        }

    }
}
