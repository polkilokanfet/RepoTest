using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeTrackingCollectionProperty
    {
        private FriendTest _friendTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest(),
                FriendGroupTest = new FriendGroupTest() {Id = 1, FriendTests = new List<FriendTest>()},
                Emails = new List<FriendEmailTest>
                {
                  new FriendEmailTest { Email="thomas@thomasclaudiushuber.com" },
                  new FriendEmailTest { Email="julia@juhu-design.com" }
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendTestWrapper()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
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
            var wrapper = new FriendTestWrapper(_friendTest);
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
            var wrapper = new FriendTestWrapper(_friendTest);

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
            var wrapper = new FriendTestWrapper(_friendTest);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomasclaudiushuber.com";

            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("thomas@thomasclaudiushuber.com", emailToModify.Email);
            Assert.AreEqual("thomas@thomasclaudiushuber.com", emailToModify.EmailOriginalValue);
        }

        [TestMethod]
        public void CollectionOfSimple()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged))
                {
                    fired = true;
                }
            };

            fired = false;
            wrapper.IntList.Add(1);
            Assert.IsTrue(fired);

            fired = false;
            wrapper.IntList.Remove(1);
            Assert.IsTrue(fired);

            wrapper.IntList.Add(1);
            fired = false;
            wrapper.IntList.Clear();
            Assert.IsTrue(fired);

        }

    }
}
