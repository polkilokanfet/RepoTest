using System;
using System.Linq;
using HVTApp.Model.Factory;
using HVTApp.Model.POCOs;
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
            _specification = WrappersFactory.GetWrapper <Specification, SpecificationWrapper> ();
        }

    }
}
