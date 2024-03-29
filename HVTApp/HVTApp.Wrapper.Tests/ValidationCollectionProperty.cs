﻿using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.Wrapper
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
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
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
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.IsFalse(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
            Assert.IsTrue(wrapper.Emails.First().HasErrors);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
        {
            TestWrappersFactory testWrappersFactory = new TestWrappersFactory();

            var emailToAdd = testWrappersFactory.GetWrapper<TestFriendEmailWrapper>(new TestFriendEmail());
            var wrapper = testWrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
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
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
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
            var wrapper = new TestWrappersFactory().GetWrapper<TestFriendWrapper>(_testFriend);
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
            TestWrappersFactory testWrappersFactory = new TestWrappersFactory();

            var fired = false;
            var wrapper = testWrappersFactory.GetWrapper<TestFriendWrapper>(_testFriend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid")
                {
                    fired = true;
                }
            };

            var emailToAdd = testWrappersFactory.GetWrapper<TestFriendEmailWrapper>(new TestFriendEmail());
            wrapper.Emails.Add(emailToAdd);
            Assert.IsTrue(fired);

            fired = false;
            emailToAdd.Email = "thomas@thomasclaudiushuber.com";
            Assert.IsTrue(fired);
        }
    }
}
