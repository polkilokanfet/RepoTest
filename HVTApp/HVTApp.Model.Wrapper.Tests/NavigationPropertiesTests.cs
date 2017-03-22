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
            TestEntity1 entity1 = new TestEntity1();
            TestEntity2 entity2 = new TestEntity2();

            entity2.TestEntity1 = entity1;
            entity1.TestEntity2 = entity2;

            TestEntity1Wrapper entity1Wrapper = new TestEntity1Wrapper(entity1);
        }
    }
}
