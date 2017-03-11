using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass()]
    public class BasicTests
    {
        private FriendTest _friendTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest(),
                FriendGroupTest = new FriendGroupTest() { FriendTests = new List<FriendTest>() },
                Emails = new List<FriendEmailTest>()
            };
        }

        [TestMethod()]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.AreEqual(_friendTest, wrapper.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new FriendTestWrapper(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Model", ex.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfAddressIsNull()
        {
            try
            {
                _friendTest.FriendAddressTest = null;
                var wrapper = new FriendTestWrapper(_friendTest);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("FriendAddressTest cannot be null", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
        {
            try
            {
                _friendTest.Emails = null;
                var wrapper = new FriendTestWrapper(_friendTest);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Emails cannot be null", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.AreEqual(_friendTest.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", _friendTest.FirstName);
        }
    }
}