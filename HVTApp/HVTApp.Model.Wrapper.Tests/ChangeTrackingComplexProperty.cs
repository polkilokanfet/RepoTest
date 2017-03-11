using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeTrackingComplexProperty
    {
        private FriendTest _friendTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest { City = "Müllheim" },
                FriendGroupTest = new FriendGroupTest() { FriendTests = new List<FriendTest>() },
                Emails = new List<FriendEmailTest>()
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendTestWrapper()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.FriendAddressTest.City = "Müllheim";
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

            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.AreEqual("Müllheim", wrapper.FriendAddressTest.CityOriginalValue);

            wrapper.AcceptChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Salt Lake City", wrapper.FriendAddressTest.City);
            Assert.AreEqual("Salt Lake City", wrapper.FriendAddressTest.CityOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.AreEqual("Müllheim", wrapper.FriendAddressTest.CityOriginalValue);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Müllheim", wrapper.FriendAddressTest.City);
            Assert.AreEqual("Müllheim", wrapper.FriendAddressTest.CityOriginalValue);
        }
    }
}
