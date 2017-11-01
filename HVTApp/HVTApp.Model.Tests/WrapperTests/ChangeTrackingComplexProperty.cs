using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
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
                TestFriendGroup = new TestFriendGroup { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendTestWrapper()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.TestFriendAddress.City = "Müllheim";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendTestWrapper()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.TestFriendAddress.City = "Salt Lake City"));

            var originAddress = wrapper.TestFriendAddress;
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.TestFriendAddress.City = "Yekaterinburg"));
            Assert.IsFalse(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => originAddress.City = "Yekaterinburg"));
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
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
            var wrapper = new TestFriendWrapper(_testFriend);
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.CityOriginalValue);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.City);
            Assert.AreEqual("Müllheim", wrapper.TestFriendAddress.CityOriginalValue);
        }

        [TestMethod]
        public void ShouldInitializeComplexProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.AreEqual(wrapper.TestFriendAddress.Model, _testFriend.TestFriendAddress);

            _testFriend.TestFriendAddress = null;
            wrapper = new TestFriendWrapper(_testFriend);
            Assert.AreEqual(wrapper.TestFriendAddress, null);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForChangeOrRemovePropertyOfFriendTestWrapper()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var originAddress = wrapper.TestFriendAddress;
            Assert.IsFalse(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.TestFriendAddress = originAddress));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress())));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsChanged), () => wrapper.TestFriendAddress = null));
        }

        [TestMethod]
        public void ModelAndWrapperComplexPropertiesInSync()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.AreEqual(wrapper.TestFriendAddress.Model, _testFriend.TestFriendAddress);

            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            Assert.AreEqual(wrapper.TestFriendAddress.Model, _testFriend.TestFriendAddress);

            wrapper.TestFriendAddress = null;
            Assert.AreEqual(null, _testFriend.TestFriendAddress);
        }

        [TestMethod]
        public void WrapperChangedByComplexProperty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.IsChanged);
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            Assert.IsTrue(wrapper.IsChanged);

            wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.IsChanged);
            wrapper.TestFriendAddress = null;
            Assert.IsTrue(wrapper.IsChanged);

            _testFriend.TestFriendAddress = null;
            wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.IsChanged);
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            Assert.IsTrue(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldAcceptChangesOfComplexProperties()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var address = new TestFriendAddress();
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(address);
            wrapper.AcceptChanges();
            Assert.AreEqual(wrapper.Model.TestFriendAddress, address);
        }

        [TestMethod]
        public void ShouldRejectChangesOfComplexProperties()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var address = _testFriend.TestFriendAddress;
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            wrapper.RejectChanges();
            Assert.AreEqual(wrapper.Model.TestFriendAddress, address);
            Assert.AreEqual(wrapper.TestFriendAddress.Model, address);

            _testFriend.TestFriendAddress = null;
            wrapper = new TestFriendWrapper(_testFriend);
            address = _testFriend.TestFriendAddress;
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress());
            wrapper.RejectChanges();
            Assert.AreEqual(wrapper.Model.TestFriendAddress, address);
            Assert.AreEqual(wrapper.TestFriendAddress?.Model, address);
        }

    }

}
