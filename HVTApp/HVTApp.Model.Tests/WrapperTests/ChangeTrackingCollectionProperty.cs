using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ChangeTrackingCollectionProperty
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress(),
                TestFriendGroup = new TestFriendGroup {FriendTests = new List<TestFriend>()},
                Emails = new List<TestFriendEmail>
                {
                  new TestFriendEmail { Email="thomas@thomasclaudiushuber.com" },
                  new TestFriendEmail { Email="julia@juhu-design.com" }
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendTestWrapper()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.IsTrue(wrapper.IsChanged);

            emailToModify.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendTestWrapper()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.Emails.First().Email = "modified@thomasclaudiushuber.com"));
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new TestFriendWrapper(_testFriend);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("modified@thomasclaudiushuber.com", emailToModify.Email);
            Assert.AreEqual("modified@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new TestFriendWrapper(_testFriend);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("thomas@thomasclaudiushuber.com", emailToModify.Email);
            Assert.AreEqual("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);
        }

    }
}
