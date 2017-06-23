using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.Wrapper
{
    [TestClass]
    public class ValidationComplexProperty
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
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = WrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootAfterInitialization()
        {
            _testFriend.TestFriendAddress.City = "";
            var wrapper = WrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.IsFalse(wrapper.IsValid);

            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var fired = false;
            var wrapper = WrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsValid))
                {
                    fired = true;
                }
            };
            wrapper.TestFriendAddress.City = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.TestFriendAddress.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }
    }
}
