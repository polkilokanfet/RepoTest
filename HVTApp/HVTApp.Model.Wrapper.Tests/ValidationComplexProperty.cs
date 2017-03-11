using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ValidationComplexProperty
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
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.FriendAddressTest.City = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootAfterInitialization()
        {
            _friendTest.FriendAddressTest.City = "";
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsFalse(wrapper.IsValid);

            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
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
            wrapper.FriendAddressTest.City = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.FriendAddressTest.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }
    }
}
