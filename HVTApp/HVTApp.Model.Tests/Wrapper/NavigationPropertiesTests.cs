using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.Wrapper
{
    [TestClass]
    public class NavigationPropertiesTests
    {
        [TestMethod]
        public void NavigationCollectionPropertiesTest()
        {
            TestFriendGroup group = new TestFriendGroup {Id = 1, Name = "group1", FriendTests = new List<TestFriend>()};
            TestFriend testFriend = new TestFriend
            {
                FirstName = "Thomas",
                TestFriendAddress = new TestFriendAddress { City = "CityOld"},
                TestFriendGroup = group,
                Emails = new List<TestFriendEmail>()
            };
            group.FriendTests.Add(testFriend);

            TestFriendWrapper wrapper = new TestFriendWrapper(testFriend);
            Assert.IsFalse(wrapper.IsChanged);

            var old = wrapper.TestFriendAddress;
            wrapper.TestFriendAddress = new TestFriendAddressWrapper(new TestFriendAddress {City = "CityNew"});
            Assert.IsTrue(wrapper.IsChanged);

            //wrapper.Test_FriendAddress = old;
            //Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
        public void NavigationPropertiesTest()
        {
            TestHusband husband = new TestHusband { Id = 1 };
            TestWife wife = new TestWife { Id = 2 };

            wife.Husband = husband;
            husband.Wife = wife;

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);

            bool fired = false;
            husbandWrapper.PropertyChanged += (sender, args) => { fired = true; };

            Assert.IsFalse(husbandWrapper.IsChanged);
            var wifeWrapper = husbandWrapper.Wife;
            wifeWrapper.N = 10;
            Assert.IsTrue(husbandWrapper.IsChanged);
            Assert.IsTrue(fired);

            TestWife otherTestWife = new TestWife { Id = 22 };
            TestWifeWrapper otherTestWifeWrapper = new TestWifeWrapper(otherTestWife);
            Assert.IsFalse(husbandWrapper.WifeIsChanged);
            husbandWrapper.Wife = otherTestWifeWrapper;
            Assert.IsTrue(husbandWrapper.WifeIsChanged);
            Assert.IsTrue(husbandWrapper.IsChanged);
            Assert.AreEqual(wifeWrapper, husbandWrapper.WifeOriginalValue);

            fired = false;
            wifeWrapper.N = 33;
            Assert.IsFalse(fired);

            fired = false;
            husbandWrapper.Wife = null;
            Assert.AreEqual(husbandWrapper.Wife, null);
            Assert.IsTrue(fired);
            Assert.IsTrue(husbandWrapper.WifeIsChanged);
            Assert.IsTrue(husbandWrapper.IsChanged);
            Assert.AreEqual(wifeWrapper, husbandWrapper.WifeOriginalValue);

            fired = false;
            TestChildWrapper childWrapper = new TestChildWrapper(new TestChild { Husband = husband });
            husbandWrapper.Children.Add(childWrapper);
            Assert.IsTrue(fired);

            var husbandWrp = new TestHusbandWrapper(husbandWrapper.Model);

            fired = false;
            childWrapper.Id = 1;
            Assert.IsTrue(fired);

            husbandWrapper.Children.Remove(childWrapper);
            fired = false;
            childWrapper.Id++;
            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void ComplexWrapperAcceptAndReject()
        {
            TestHusband husband = new TestHusband();
            TestWife wife1 = new TestWife();
            TestChild child1 = new TestChild {Id = 1, Husband = husband, Wife = wife1};
            TestChild child2 = new TestChild {Id = 2, Husband = husband, Wife = wife1};

            wife1.Husband = husband;
            husband.Wife = wife1;
            husband.Children.Add(child1);
            husband.Children.Add(child2);

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);
            Assert.IsFalse(husbandWrapper.IsChanged);

            TestChildWrapper childWrapper1 = husbandWrapper.Children.First(x => Equals(x.Model, child1));

            string oldChildsName = childWrapper1.Name;
            childWrapper1.Name = oldChildsName + "NEW";
            husbandWrapper.RejectChanges();
            Assert.AreEqual(childWrapper1.Name, oldChildsName);

            childWrapper1.Name = oldChildsName + "NEW";
            husbandWrapper.AcceptChanges();
            Assert.AreEqual(childWrapper1.Name, oldChildsName + "NEW");
            Assert.AreEqual(childWrapper1.NameOriginalValue, oldChildsName + "NEW");

            TestWifeWrapper wifeWrapper = husbandWrapper.Wife;
            husbandWrapper.Wife = null;
            Assert.AreEqual(husbandWrapper.Wife, null);
            husbandWrapper.RejectChanges();
            Assert.AreEqual(husbandWrapper.Wife, wifeWrapper);

            TestWifeWrapper wifeWrapper2 = new TestWifeWrapper(new TestWife());
            husbandWrapper.Wife = wifeWrapper2;
            husbandWrapper.AcceptChanges();
            Assert.AreEqual(wifeWrapper2.Model, husbandWrapper.Model.Wife);

            husbandWrapper.Children.Remove(childWrapper1);
            husbandWrapper.RejectChanges();
            Assert.IsTrue(husbandWrapper.Children.Contains(childWrapper1));
            Assert.IsFalse(husbandWrapper.IsChanged);
        }

        [TestMethod]
        public void SimpleWrapperTest()
        {
            TestHusband husband = new TestHusband();
            TestChild child1 = new TestChild { Id = 1, Husband = husband };
            //TestChild child2 = new TestChild { Id = 2, Husband = husband };

            husband.Children.Add(child1);
            //husband.Children.Add(child2);

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);
            TestChildWrapper childWrapper1 = husbandWrapper.Children.First(x => Equals(x.Model, child1));
            Assert.IsFalse(husbandWrapper.IsChanged);


            string oldChildsName = childWrapper1.Name;
            childWrapper1.Name = oldChildsName + "NewName";
            Assert.IsTrue(husbandWrapper.IsChanged);

            childWrapper1.Name = oldChildsName;
            Assert.IsFalse(husbandWrapper.IsChanged);


            husbandWrapper.Children.Remove(childWrapper1);
            Assert.IsTrue(husbandWrapper.IsChanged);
            husbandWrapper.Children.Add(childWrapper1);
            Assert.IsFalse(husbandWrapper.IsChanged);
        }

        [TestMethod]
        public void ComplexWrapperTest()
        {
            TestHusband husband = new TestHusband();
            TestWife wife = new TestWife();
            TestChild child1 = new TestChild { Id = 1, Husband = husband, Wife = wife };
            TestChild child2 = new TestChild { Id = 2, Husband = husband, Wife = wife };

            wife.Husband = husband;
            husband.Wife = wife;
            husband.Children.Add(child1);
            husband.Children.Add(child2);

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);
            Assert.IsFalse(husbandWrapper.IsChanged);

            TestChildWrapper childWrapper1 = husbandWrapper.Children.First(x => Equals(x.Model, child1));

            string oldChildsName = childWrapper1.Name;
            childWrapper1.Name = oldChildsName + "NEW";
            Assert.IsTrue(husbandWrapper.IsChanged);

            childWrapper1.Name = oldChildsName;
            Assert.IsFalse(husbandWrapper.IsChanged);


            husbandWrapper.Children.Remove(childWrapper1);
            Assert.IsTrue(husbandWrapper.IsChanged);
            husbandWrapper.Children.Add(childWrapper1);
            Assert.IsFalse(husbandWrapper.IsChanged);
        }

        [TestMethod]
        public void IsChangedNavigationProperty()
        {
            TestHusband husband = new TestHusband { Id = 1 };
            TestWife wife1 = new TestWife { Id = 2 };

            wife1.Husband = husband;
            husband.Wife = wife1;

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);
            Assert.IsFalse(husbandWrapper.IsChanged);

            var wife1Wrapper = husbandWrapper.Wife;
            husbandWrapper.Wife = new TestWifeWrapper(new TestWife { Id = 3 });
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

            TestHusbandWrapper husbandWrapper = new TestHusbandWrapper(husband);
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

            TestChildWrapper childWrapper = new TestChildWrapper(new TestChild { Husband = husband });
            husbandWrapper.Children.Add(childWrapper);
            Assert.IsTrue(husbandWrapper.IsChanged);

        }
    }
}
