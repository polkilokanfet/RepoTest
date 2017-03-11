using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class ValidationCollectionProperty
    {
        private FriendTest _friendTest;

        [TestInitialize]
        public void Initialize()
        {
            _friendTest = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest {City = "Müllheim"},
                FriendGroupTest = new FriendGroupTest() { FriendTests = new List<FriendTest>() },
                Emails = new List<FriendEmailTest>
                {
                    new FriendEmailTest {Email = "thomas@thomasclaudiushuber.com"},
                    new FriendEmailTest {Email = "julia@juhu-design.com"}
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenInitializing()
        {
            _friendTest.Emails.First().Email = "";
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsFalse(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
            Assert.IsTrue(wrapper.Emails.First().HasErrors);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
        {
            var emailToAdd = new FriendEmailTestWrapper(new FriendEmailTest());
            var wrapper = new FriendTestWrapper(_friendTest);
            Assert.IsTrue(wrapper.IsValid); ;
            wrapper.Emails.Add(emailToAdd);
            Assert.IsFalse(wrapper.IsValid);
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid")
                {
                    fired = true;
                }
            };
            wrapper.Emails.First().Email = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid")
                {
                    fired = true;
                }
            };
            wrapper.Emails.First().Email = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenAddingInvalidItem()
        {
            var fired = false;
            var wrapper = new FriendTestWrapper(_friendTest);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid")
                {
                    fired = true;
                }
            };

            var emailToAdd = new FriendEmailTestWrapper(new FriendEmailTest());
            wrapper.Emails.Add(emailToAdd);
            Assert.IsTrue(fired);

            fired = false;
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(fired);
        }
    }
}
