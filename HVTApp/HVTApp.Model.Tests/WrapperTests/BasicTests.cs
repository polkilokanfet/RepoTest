using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.AreEqual(_testFriend, wrapper.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new TestFriendWrapper(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Entity", ex.ParamName);
                throw;
            }
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.AreEqual(_testFriend.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", _testFriend.FirstName);
        }
    }
}