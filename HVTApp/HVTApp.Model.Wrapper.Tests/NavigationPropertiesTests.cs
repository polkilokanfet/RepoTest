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
                FriendAddressTest = new FriendAddressTest { City = "CityOld"},
                FriendGroupTest = group,
                Emails = new List<FriendEmailTest>()
            };
            group.FriendTests.Add(friend);

            FriendTestWrapper wrapper = FriendTestWrapper.GetWrapper(friend);
            Assert.IsFalse(wrapper.IsChanged);

            var old = wrapper.FriendAddressTest;
            wrapper.FriendAddressTest = FriendAddressTestWrapper.GetWrapper(new FriendAddressTest {City = "CityNew"});
            Assert.IsTrue(wrapper.IsChanged);

            //wrapper.FriendAddressTest = old;
            //Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void NavigationPropertiesTest()
        {
            Parent parent = new Parent {Id = 1};
            Child child = new Child {Id = 2};

            child.Parent = parent;
            parent.Child = child;

            ParentWrapper parentWrapper = ParentWrapper.GetWrapper(parent);

            bool fired = false;
            parentWrapper.PropertyChanged += (sender, args) => { fired = true; };

            Assert.IsFalse(parentWrapper.IsChanged);
            var childWrapper = parentWrapper.Child;
            childWrapper.N = 10;
            Assert.IsTrue(parentWrapper.IsChanged);
            Assert.IsTrue(fired);

            Child otherChild = new Child {Id = 22};
            ChildWrapper otherChildWrapper = ChildWrapper.GetWrapper(otherChild);
            parentWrapper.Child = otherChildWrapper;
            Assert.IsTrue(parentWrapper.IsChanged);

            fired = false;
            childWrapper.N = 33;
            Assert.IsFalse(fired);

            fired = false;
            parentWrapper.Child = null;
            Assert.AreEqual(parentWrapper.Child, null);
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void IsChangedNavigationProperty()
        {
            Parent parent = new Parent { Id = 1 };
            Child child1 = new Child { Id = 2 };

            child1.Parent = parent;
            parent.Child = child1;

            ParentWrapper parentWrapper = ParentWrapper.GetWrapper(parent);
            Assert.IsFalse(parentWrapper.IsChanged);

            var child1Wrapper = parentWrapper.Child;
            parentWrapper.Child = ChildWrapper.GetWrapper(new Child { Id = 3 });
            Assert.IsTrue(parentWrapper.IsChanged);

            parentWrapper.Child = child1Wrapper;
            Assert.IsFalse(parentWrapper.IsChanged);
        }

        [TestMethod]
        public void AcceptAndRejectChangesInObjectsWithNavigationProperty()
        {
            Parent parent = new Parent { Id = 1 };
            Child child1 = new Child { Id = 2 };

            child1.Parent = parent;
            parent.Child = child1;

            ParentWrapper parentWrapper = ParentWrapper.GetWrapper(parent);
            Assert.IsFalse(parentWrapper.IsChanged);

            parentWrapper.Child.Id++;
            Assert.IsTrue(parentWrapper.IsChanged);
            parentWrapper.AcceptChanges();
            Assert.IsFalse(parentWrapper.IsChanged);

            int oldId = parentWrapper.Child.Id;
            parentWrapper.Child.Id++;
            Assert.IsTrue(parentWrapper.IsChanged);
            parentWrapper.RejectChanges();
            Assert.IsFalse(parentWrapper.IsChanged);
            Assert.AreEqual(oldId, parentWrapper.Child.Id);
        }
    }
}
