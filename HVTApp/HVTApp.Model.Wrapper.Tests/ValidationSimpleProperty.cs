using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ValidationSimpleProperty
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
        public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
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
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);

            wrapper.ErrorsChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstName))
                {
                    fired = true;
                }
            };

            wrapper.FirstName = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldSetIsValid()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.FirstName = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.FirstName = "Julia";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValid()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);

            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsValid))
                {
                    fired = true;
                }
            };

            wrapper.FirstName = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldSetErrorsAndIsValidAfterInitialization()
        {
            _friendTest.FirstName = "";
            var wrapper = new FriendTestWrapper(_friendTest);

            Assert.IsFalse(wrapper.IsValid);
            Assert.IsTrue(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Firstname is required", errors.First());
        }

        [TestMethod]
        public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
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
