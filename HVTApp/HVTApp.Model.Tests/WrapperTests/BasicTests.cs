using System;
using System.Collections.Generic;
using System.Reflection;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWrappersFactory = HVTApp.Model.Tests.Factory.TestWrappersFactory;

namespace HVTApp.Model.Tests.WrapperTests
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
                TestFriendGroup = new TestFriendGroup { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod()]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.AreEqual(_testFriend, wrapper.Model);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        //{
        //    try
        //    {
        //        var wrapper = TestWrappersFactory.GetWrapper<TestFriendWrapper>(null);
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
        //        var wrapper = TestWrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Assert.AreEqual("Test_FriendAddress cannot be null", ex.Message);
        //        throw;
        //    }
        //}

        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
        {
            try
            {
                _testFriend.Emails = null;
                var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            }
            catch (TargetInvocationException ex)
            {
                Assert.AreEqual("Emails cannot be null", ex.InnerException.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.AreEqual(_testFriend.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", _testFriend.FirstName);
        }
    }
}