using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeNotificationComplexProperty
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

        [TestMethod]
        public void ShouldInitializeAddressProperty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsNotNull(wrapper.FriendAddressTest);
            Assert.AreEqual(_friendTest.FriendAddressTest, wrapper.FriendAddressTest.Model);
        }
    }
}
