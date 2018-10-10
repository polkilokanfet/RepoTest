using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests.POCOs
{
    [TestClass]
    public class ParameterTests
    {
        [TestMethod]
        public void PathsTest()
        {
            var testData = new TestData();
            var parameters = testData.GetAll<Parameter>().ToList();

            var rr = testData.ParameterClimatU1Z.Paths();

            var result = new List<PathToOrigin>();
            foreach (var parameter in parameters)
            {
                var paths = parameter.Paths();
                result.AddRange(paths);
            }
        }
    }
}