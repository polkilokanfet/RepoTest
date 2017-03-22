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
        }

        [TestMethod]
        public void NavigationPropertiesTest()
        {
            TestEntity1 entity1 = new TestEntity1() {Id = 1};
            TestEntity2 entity2 = new TestEntity2() {Id = 2};

            entity2.TestEntity1 = entity1;
            entity1.TestEntity2 = entity2;

            TestEntity1Wrapper entity1Wrapper = new TestEntity1Wrapper(entity1);

            bool fired = false;
            entity1Wrapper.PropertyChanged += (sender, args) => { fired = true; };

            Assert.IsFalse(entity1Wrapper.IsChanged);
            var te2 = entity1Wrapper.TestEntity2;
            te2.N = 10;
            Assert.IsTrue(entity1Wrapper.IsChanged);
            Assert.IsTrue(fired);

            TestEntity2 entity22 = new TestEntity2() {Id = 22};
            TestEntity2Wrapper entity2Wrapper = new TestEntity2Wrapper(entity22);
            entity1Wrapper.TestEntity2 = entity2Wrapper;
            fired = false;
            te2.N = 33;
            Assert.IsFalse(fired);
        }
    }
}
