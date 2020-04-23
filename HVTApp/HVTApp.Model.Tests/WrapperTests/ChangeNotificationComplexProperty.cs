using System.Collections.Generic;
using HVTApp.Model.POCOs.Test;
using HVTApp.Model.Wrapper.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ChangeNotificationComplexProperty
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

        [TestMethod]
        public void ShouldInitializeAddressProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsNotNull(wrapper.TestFriendAddress);
            Assert.AreEqual(_testFriend.TestFriendAddress, wrapper.TestFriendAddress.Model);
        }
    }
}
