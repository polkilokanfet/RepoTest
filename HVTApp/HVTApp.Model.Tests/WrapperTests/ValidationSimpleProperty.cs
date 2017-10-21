using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ValidationSimpleProperty
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
        public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.HasErrors);

            wrapper.FirstName = "";
            Assert.IsTrue(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Firstname is required", errors.First());

            wrapper.FirstName = "Julia";
            Assert.IsFalse(wrapper.HasErrors);
        }

        [TestMethod]
        public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmptyAndBack()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.FirstName), () => wrapper.FirstName = ""));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.FirstName), () => wrapper.FirstName = "Julia"));
        }

        [TestMethod]
        public void ShouldSetIsValid()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.FirstName = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.FirstName = "Julia";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValid()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.FirstName = ""));

            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.FirstName = "Julia"));
        }

        [TestMethod]
        public void ShouldSetErrorsAndIsValidAfterInitialization()
        {
            _testFriend.FirstName = "";
            var wrapper = new TestFriendWrapper(_testFriend);

            Assert.IsFalse(wrapper.IsValid);
            Assert.IsTrue(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Firstname is required", errors.First());
        }

        [TestMethod]
        public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);

            wrapper.FirstName = "";

            Assert.IsFalse(wrapper.IsValid);
            Assert.IsTrue(wrapper.HasErrors);

            wrapper.RejectChanges();

            Assert.IsTrue(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
        }
    }
}
