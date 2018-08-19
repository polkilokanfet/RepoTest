using Microsoft.VisualStudio.TestTools.UnitTesting;
using HVTApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVTApp.UI.Tests
{
    [TestClass()]
    public class GeneratorHelpersTests
    {
        [TestMethod()]
        public void SaveGeneratedCodeAsFileTest()
        {
            GeneratorHelpers.SaveGeneratedCodeAsFile("", "");
            Assert.Fail();
        }
    }
}