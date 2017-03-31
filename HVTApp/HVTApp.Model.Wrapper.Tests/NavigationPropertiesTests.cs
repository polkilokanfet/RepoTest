using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Wrapper.Tests
{
    [TestClass]
    public class NavigationPropertiesTests
    {
        [TestMethod]
        public void NavigationCollectionPropertiesTest()
        {
            FriendGroupTest group = new FriendGroupTest {Id = 1, Name = "group1", FriendTests = new List<FriendTest>()};
            FriendTest friend = new FriendTest
            {
                FirstName = "Thomas",
                FriendAddressTest = new FriendAddressTest(),
                FriendGroupTest = group,
                Emails = new List<FriendEmailTest>()
            };
            group.FriendTests.Add(friend);

            FriendTestWrapper wrapper = new FriendTestWrapper(friend);
            Assert.IsFalse(wrapper.IsChanged);

            var old = wrapper.FriendAddressTest;
            wrapper.FriendAddressTest = new FriendAddressTestWrapper(new FriendAddressTest());
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.FriendAddressTest = old;
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void NavigationPropertiesTest()
        {
            Parent parent = new Parent {Id = 1};
            Child child = new Child {Id = 2};

            child.Parent = parent;
            parent.Child = child;

            ParentWrapper parentWrapper = new ParentWrapper(parent);

            bool fired = false;
            parentWrapper.PropertyChanged += (sender, args) => { fired = true; };

            Assert.IsFalse(parentWrapper.IsChanged);
            var childWrapper = parentWrapper.Child;
            childWrapper.N = 10;
            Assert.IsTrue(parentWrapper.IsChanged);
            Assert.IsTrue(fired);

            Child otherChild = new Child {Id = 22};
            ChildWrapper otherChildWrapper = new ChildWrapper(otherChild);
            parentWrapper.Child = otherChildWrapper;
            Assert.IsTrue(parentWrapper.IsChanged);

            fired = false;
            childWrapper.N = 33;
            Assert.IsFalse(fired);

            parentWrapper.Child = null;
            Assert.AreEqual(parentWrapper.Child, null);
        }

        [TestMethod]
        public void CyclingDependensies()
        {
            Company parent = new Company { FullName = "Parent" };
            Company child = new Company { FullName = "Child"};

            CompanyWrapper parentWrapper = new CompanyWrapper(parent);
            CompanyWrapper childWrapper = new CompanyWrapper(child);

            childWrapper.ParentCompany = parentWrapper;
            parentWrapper.ChildCompanies.Add(childWrapper);
        }
    }
}
