using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private List<ParameterGroup> _groups;
        private List<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;
        private Parameter _breaker, _transformator, _v110, _v220 ;
        private RequiredDependentEquipmentsParameters _requiredDependentEquipmentsParameters;

        [TestInitialize]
        public void Init()
        {
            ParameterGroup eqType = new ParameterGroup();
            _breaker = new Parameter {Group = eqType};
            _transformator = new Parameter {Group = eqType};
            eqType.Parameters.AddRange(new[] {_breaker, _transformator});

            ParameterGroup voltage = new ParameterGroup();
            _v110 = new Parameter { Group = voltage }; _v110.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> {_breaker} });
            _v220 = new Parameter { Group = voltage }; _v220.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { _breaker } });
            voltage.Parameters.AddRange(new[] {_v110, _v220});

            _groups = new List<ParameterGroup> {eqType, voltage};

            _requiredDependentEquipmentsParameters = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator }
            };
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentEquipmentsParameters> {_requiredDependentEquipmentsParameters};
        }

        [TestMethod]
        public void TestMethod1()
        {
            var equipmentSelector = new EquipmentSelector(_groups, new List<Product>(), new List<Equipment>(), 
                _requiredDependentEquipmentsParametersList);

            Assert.IsTrue(equipmentSelector.ProductSelector.ParameterSelectors.Count == _groups.Count);

            //должен иметь зависимые продукты
            Assert.IsTrue(equipmentSelector.DependetEquipmentSelectors.Count == 1);
        }
    }
}
