using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ChangeTrackingSimpleProperty
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress(),
                TestFriendGroup = new TestFriendGroup() { FriendTests = new List<TestFriend>()},
                Emails = new List<TestFriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldStoreOriginalValue()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);

            wrapper.FirstName = "Julia";
            Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
        }

        [TestMethod]
        public void ShouldSetIsChanged()
        {
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
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
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
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
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
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
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
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
            var wrapper = TestFriendWrapper.GetWrapper(_testFriend);
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
