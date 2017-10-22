using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ValidationComplexProperty
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress { City = "Müllheim" },
                TestFriendGroup = new TestFriendGroup { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootAfterInitialization()
        {
            _testFriend.TestFriendAddress.City = "";
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.TestFriendAddress.City = ""));

            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.TestFriendAddress.City = "Salt Lake City"));
        }
    }
}
