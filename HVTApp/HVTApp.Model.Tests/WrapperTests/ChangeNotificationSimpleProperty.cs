using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ChangeNotificationSimpleProperty
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
        public void ShouldRaisePropertyChangedEventOnPropertyChange()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.FirstName), () => wrapper.FirstName = "Julia"));
        }

        [TestMethod]
        public void ShouldNotRaisePropertyChangedEventIfPropertyIsSetToSameValue()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.PropertyChangedEventRised(nameof(wrapper.FirstName), () => wrapper.FirstName = "Thomas"));
        }
    }
}
