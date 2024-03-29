// <copyright file="ProductWrapperTest.cs" company="Microsoft">Copyright © Microsoft 2016</copyright>
using System;
using HVTApp.Model.Wrappers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrappers.Tests
{
    /// <summary>This class contains parameterized unit tests for ProductWrapper</summary>
    [PexClass(typeof(ProductWrapper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ProductWrapperTest
    {
        /// <summary>Test stub for GetTotalPrice(Nullable`1&lt;DateTime&gt;)</summary>
        [PexMethod]
        public double GetTotalPriceTest(
            [PexAssumeUnderTest]ProductWrapper target,
            DateTime? date
        )
        {
            double result = target.GetTotalPrice(date);
            return result;
            // TODO: add assertions to method ProductWrapperTest.GetTotalPriceTest(ProductWrapper, Nullable`1<DateTime>)
        }
    }
}
