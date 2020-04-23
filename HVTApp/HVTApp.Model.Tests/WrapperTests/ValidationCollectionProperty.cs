using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs.Test;
using HVTApp.Model.Wrapper.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.WrapperTests
{
    [TestClass]
    public class ValidationCollectionProperty
    {
        private TestFriend _testFriend;

        [TestInitialize]
        public void Initialize()
        {
            _testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress {City = "Müllheim"},
                TestFriendGroup = new TestFriendGroup() { FriendTests = new List<TestFriend>() },
                Emails = new List<TestFriendEmail>
                {
                    new TestFriendEmail {Email = "thomas@thomasclaudiushuber.com"},
                    new TestFriendEmail {Email = "julia@juhu-design.com"}
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenInitializing()
        {
            _testFriend.Emails.First().Email = "";
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsFalse(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
            Assert.IsTrue(wrapper.Emails.First().HasErrors);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
        {
            var emailToAdd = new TestFriendEmailWrapper(new TestFriendEmail());
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.IsValid); ;
            wrapper.Emails.Add(emailToAdd);
            Assert.IsFalse(wrapper.IsValid);
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.Emails.First().Email = ""));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com"));
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.Emails.First().Email = ""));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.Emails.Remove(wrapper.Emails.First())));
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenAddingInvalidItem()
        {
            var wrapper = new TestFriendWrapper(_testFriend);
            var emailToAdd = new TestFriendEmailWrapper(new TestFriendEmail());
            
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => wrapper.Emails.Add(emailToAdd)));
            Assert.IsTrue(wrapper.PropertyChangedEventRised(nameof(wrapper.IsValid), () => emailToAdd.Email = "thomas@thomasclaudiushuber.com"));
        }
    }
}
