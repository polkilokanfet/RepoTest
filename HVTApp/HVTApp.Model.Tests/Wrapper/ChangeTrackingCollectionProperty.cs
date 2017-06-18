using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.Factory;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.Wrapper
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
                TestFriendGroup = new TestFriendGroup() {Id = 1, FriendTests = new List<TestFriend>()},
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
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.IsTrue(wrapper.IsChanged);

            emailToModify.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendTestWrapper()
        {
            var fired = false;
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
              {
                  if (e.PropertyName == nameof(wrapper.IsChanged))
                  {
                      fired = true;
                  }
              };

            wrapper.Emails.First().Email = "modified@thomasclaudiushuber.com";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);

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
            var wrapper = WrappersFactory.GetWrapper <TestFriend, TestFriendWrapper>(_testFriend);

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
