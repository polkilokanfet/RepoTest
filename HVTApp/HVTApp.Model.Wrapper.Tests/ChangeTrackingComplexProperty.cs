using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeTrackingComplexProperty
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress { City = "Müllheim" },
                TestFriendGroup = new TestFriendGroup() { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendTestWrapper()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.TestFriendAddress.City = "Müllheim";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendTestWrapper()
        {
            var fired = false;
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
              {
                  if (e.PropertyName == nameof(wrapper.IsChanged))
                  {
                      fired = true;
                  }
              };

            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.CityOriginalValue);

            wrapper.AcceptChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Salt Lake City", wrapper.TestFriendAddress.City);
            Assert.AreEqual("Salt Lake City", wrapper.TestFriendAddress.CityOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.CityOriginalValue);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.City);
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.CityOriginalValue);
        }
    }
}
