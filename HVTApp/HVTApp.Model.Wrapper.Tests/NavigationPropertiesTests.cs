using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
        public void NavigationPropertiesTest()
        {
            TestHusband husband = new TestHusband {Id = 1};
            TestWife wife = new TestWife {Id = 2};

            wife.Husband = husband;
            husband.Wife = wife;

            TestHusbandWrapper husbandWrapper = TestHusbandWrapper.GetWrapper(husband);

            bool fired = false;
            husbandWrapper.PropertyChanged += (sender, args) => 
            {
                if (args.PropertyName == "IsChanged")
                    fired = true;
            };

            Assert.IsFalse(husbandWrapper.IsChanged);
            var originWifeWrapper = husbandWrapper.Wife;
            originWifeWrapper.N = 10;
            Assert.IsTrue(husbandWrapper.IsChanged);
            Assert.IsTrue(fired);

            TestWife otherWife = new TestWife {Id = 22};
            TestWifeWrapper otherWifeWrapper = TestWifeWrapper.GetWrapper(otherWife);
            husbandWrapper.Wife = otherWifeWrapper;
            Assert.IsTrue(husbandWrapper.IsChanged);

            fired = false;
            originWifeWrapper.N = 33;
            Assert.IsFalse(fired);

            fired = false;
            husbandWrapper.Wife = null;
            Assert.AreEqual(husbandWrapper.Wife, null);
            Assert.IsTrue(fired);

            fired = false;
            TestChildWrapper childWrapper = TestChildWrapper.GetWrapper(new TestChild { Husband = husband });
            husbandWrapper.Children.Add(childWrapper);
            Assert.IsTrue(fired);

            var husbandWrp = TestHusbandWrapper.GetWrapper(husbandWrapper.Model);

            fired = false;
            childWrapper.Id = 1;
            Assert.IsTrue(fired);

            husbandWrapper.Children.Remove(childWrapper);
            fired = false;
            childWrapper.Id++;
            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void IsChangedNavigationProperty()
        {
            TestHusband husband = new TestHusband { Id = 1 };
            TestWife wife1 = new TestWife { Id = 2 };

            wife1.Husband = husband;
            husband.Wife = wife1;

            TestHusbandWrapper husbandWrapper = TestHusbandWrapper.GetWrapper(husband);
            Assert.IsFalse(husbandWrapper.IsChanged);

            var wife1Wrapper = husbandWrapper.Wife;
            husbandWrapper.Wife = TestWifeWrapper.GetWrapper(new TestWife { Id = 3 });
            Assert.IsTrue(husbandWrapper.IsChanged);

            husbandWrapper.Wife = wife1Wrapper;
            Assert.IsFalse(husbandWrapper.IsChanged);
        }

        [TestMethod]
        public void AcceptAndRejectChangesInObjectsWithNavigationProperty()
        {
            TestHusband husband = new TestHusband { Id = 1 };
            TestWife wife1 = new TestWife { Id = 2 };

            wife1.Husband = husband;
            husband.Wife = wife1;

            TestHusbandWrapper husbandWrapper = TestHusbandWrapper.GetWrapper(husband);
            Assert.IsFalse(husbandWrapper.IsChanged);

            husbandWrapper.Wife.Id++;
            Assert.IsTrue(husbandWrapper.IsChanged);
            husbandWrapper.AcceptChanges();
            Assert.IsFalse(husbandWrapper.IsChanged);

            int oldId = husbandWrapper.Wife.Id;
            husbandWrapper.Wife.Id++;
            Assert.IsTrue(husbandWrapper.IsChanged);
            husbandWrapper.RejectChanges();
            Assert.IsFalse(husbandWrapper.IsChanged);
            Assert.AreEqual(oldId, husbandWrapper.Wife.Id);

            TestChildWrapper childWrapper = TestChildWrapper.GetWrapper(new TestChild { Husband = husband });
            husbandWrapper.Children.Add(childWrapper);
            Assert.IsTrue(husbandWrapper.IsChanged);

        }
    }
}
