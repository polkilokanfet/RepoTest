using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.Wrapper
{
    [TestClass()]
    public class BasicTests
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress(),
                TestFriendGroup = new TestFriendGroup() { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod()]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
            Assert.AreEqual(_testFriend, wrapper.Model);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        //{
        //    try
        //    {
        //        var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(null);
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        Assert.AreEqual("Model", ex.ParamName);
        //        throw;
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ShouldThrowArgumentExceptionIfAddressIsNull()
        //{
        //    try
        //    {
        //        _testFriend.Test_FriendAddress = null;
        //        var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Assert.AreEqual("Test_FriendAddress cannot be null", ex.Message);
        //        throw;
        //    }
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
        {
            try
            {
                _testFriend.Emails = null;
                var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
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
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
            Assert.AreEqual(_testFriend.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", _testFriend.FirstName);
        }
    }
}