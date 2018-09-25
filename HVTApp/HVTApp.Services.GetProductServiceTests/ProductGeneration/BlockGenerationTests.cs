using Microsoft.VisualStudio.TestTools.UnitTesting;
using HVTApp.Services.GetProductService.ProductGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.TestDataGenerator;

namespace HVTApp.Services.GetProductService.ProductGeneration.Tests
{
    [TestClass]
    public class BlockGenerationTests
    {
        [TestMethod]
        public void GetFullPathsTest()
        {
            var blockGeneration = new BlockGeneration(new UnitOfWorkTest(new TestData()));
            var list = blockGeneration.GetFullPaths().ToList();
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllBlocksTest()
        {
            var blockGeneration = new BlockGeneration(new UnitOfWorkTest(new TestData()));
            var blocks = blockGeneration.GetAllBlocks().ToList();
            Assert.Fail();
        }
    }
}