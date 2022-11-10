using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.OfferToDocServiceTests
{
    [TestClass]
    public class HashTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            var parameter1 = new Parameter();
            var parameter2 = new Parameter();

            Assert.AreNotEqual(parameter1, parameter2);
        }

        private int _setLength = 5000000;
        [TestMethod]
        public void BigHashSetTest()
        {
            var set = new HashSet<Parameter>();
            for (int i = 0; i < _setLength; i++)
            {
                set.Add(new Parameter());
            }

            var parameter = set.Last();
            var r = set.Contains(parameter);
        }

        [TestMethod]
        public void BigListTest()
        {
            var set = new List<Parameter>();
            for (int i = 0; i < _setLength; i++)
            {
                set.Add(new Parameter());
            }

            var parameter = set.Last();
            var r = set.Contains(parameter);
        }

        [TestMethod]
        public void EqualBlock()
        {
            var set1 = new List<Parameter>();
            var set2 = new List<Parameter>();
            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                set1.Add(new Parameter { Id = id, Value = i.ToString() });
                set2.Add(new Parameter { Id = id, Value = i.ToString() });
                Assert.AreEqual(set1[i], set2[i]);
                Assert.AreEqual(set1[i].GetHashCode(), set2[i].GetHashCode());
            }

            set1.Reverse();
            var block1 = new ProductBlock { Parameters = set1 };
            var block2 = new ProductBlock { Parameters = set2 };
            Assert.AreEqual(block1, block2);
            Assert.AreEqual(block1.GetHashCode(), block2.GetHashCode());
        }
    }
}