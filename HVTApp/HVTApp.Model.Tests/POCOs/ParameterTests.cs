using Microsoft.VisualStudio.TestTools.UnitTesting;
using HVTApp.Model.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.TestDataGenerator;

namespace HVTApp.Model.POCOs.Tests
{
    [TestClass()]
    public class ParameterTests
    {
        [TestMethod()]
        public void PathsTest()
        {
            var testData = new TestData();
            var parameters = testData.GetAll<Parameter>().ToList();


            foreach (var parameter in parameters)
            {
                var paths = parameter.Paths().ToList();
            }
        }
    }
}