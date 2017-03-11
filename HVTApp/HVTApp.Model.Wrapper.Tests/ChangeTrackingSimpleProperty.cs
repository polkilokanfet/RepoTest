using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeTrackingSimpleProperty
    {
        private FriendTest _friendTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest(),
                FriendGroupTest = new FriendGroupTest() { FriendTests = new List<FriendTest>()},
                Emails = new List<FriendEmailTest>()
            };
        }

        [TestMethod]
        public void ShouldStoreOriginalValue()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);

            wrapper.FirstName = "Julia";
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
        }

        [TestMethod]
        public void ShouldSetIsChanged()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);

            wrapper.FirstName = "Julia";
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.FirstName = "Thomas";
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForFirstNameIsChanged()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstNameIsChanged))
                {
                    fired = true;
                }
            };
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChanged()
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
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Julia", wrapper.FirstNameOriginalValue);
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.AreEqual("Thomas", wrapper.FirstName);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }
    }
}
