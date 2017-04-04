using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
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
            var fired = false;
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FirstName")
                {
                    fired = true;
                }
            };
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyChangedEventIfPropertyIsSetToSameValue()
        {
            var fired = false;
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FirstName")
                {
                    fired = true;
                }
            };
            wrapper.FirstName = "Thomas";
            Assert.IsFalse(fired);
        }
    }
}
