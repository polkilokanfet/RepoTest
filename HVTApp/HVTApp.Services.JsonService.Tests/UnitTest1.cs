using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.JsonService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private TestObj _testObj1;
        private TestObj _testObj2;
        private List<TestObj> _testList;

        private string _path = @"C:\Users\Zver\Desktop\test.json";

        [TestInitialize]
        public void Initialize()
        {
            _testObj1 = new TestObj { S1 = "s1", I1 = 1 };
            _testObj2 = new TestObj { S1 = "s2", I1 = 2 };
            _testList = new List<TestObj> { _testObj1, _testObj2 };
        }


        [TestMethod]
        public void TestSerializeList()
        {
            new ServiceJson().Serialize(new List<TestObj> { _testObj1, _testObj2 });
        }

        [TestMethod]
        public void TestDeserializeObject()
        {
            var serviceJson = new ServiceJson();
            serviceJson.Deserialize<TestObj>(serviceJson.Serialize(_testObj1));
        }

        [TestMethod]
        public void TestDeserializeList()
        {
            var serviceJson = new ServiceJson();
            serviceJson.Deserialize<List<TestObj>>(serviceJson.Serialize(_testList));
        }

        [TestMethod]
        public void TestWriteJsonFileObject()
        {
            new ServiceJson().WriteJsonFile(_testObj1, _path);
        }

        [TestMethod]
        public void TestWriteJsonFileList()
        {
            new ServiceJson().WriteJsonFile(_testList, _path);
        }


        [TestMethod]
        public void TestReadJsonFileObject()
        {
            new ServiceJson().WriteJsonFile(_testObj1, _path);
            var result = new ServiceJson().ReadJsonFile<TestObj>(_path);
        }

        [TestMethod]
        public void TestReadJsonFileList()
        {
            new ServiceJson().WriteJsonFile(_testList, _path);
            var result = new ServiceJson().ReadJsonFile<List<TestObj>>(_path);
        }

    }
}
