using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class EquipmentSelectorTests
    {
        private List<ParameterGroup> _groups;
        private List<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;
        private Parameter _breaker, _transformator, _v110, _v220;
        private RequiredDependentEquipmentsParameters _requiredDependentEquipmentsParameters;

        [TestInitialize]
        public void Init()
        {
            
            _breaker = new Parameter();
            _transformator = new Parameter();
            ParameterGroup eqType = new ParameterGroup().AddParameters(new[] { _breaker, _transformator });

            _v110 = new Parameter(); _v110.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { _breaker } });
            _v220 = new Parameter(); _v220.RequiredPreviousParameters.Add(new RequiredPreviousParameters { RequiredParameters = new List<Parameter> { _breaker } });
            ParameterGroup voltage = new ParameterGroup().AddParameters(new[] { _v110, _v220 });

            _groups = new List<ParameterGroup> { eqType, voltage };

            _requiredDependentEquipmentsParameters = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator }
            };
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentEquipmentsParameters> { _requiredDependentEquipmentsParameters };
        }

        [TestMethod]
        public void EquipmentSelectorHasSelectedEquipment()
        {
            var equipmentSelector = new EquipmentSelector(_groups, _requiredDependentEquipmentsParametersList);

            Assert.IsTrue(equipmentSelector.SelectedEquipment != null);
        }


        [TestMethod]
        public void EquipmentSelectorIncludesAllGroups()
        {
            var equipmentSelector = new EquipmentSelector(_groups, _requiredDependentEquipmentsParametersList);

            Assert.IsTrue(equipmentSelector.ProductSelector.ParameterSelectors.Count == _groups.Count);
        }

        [TestMethod]
        public void EquipmentSelectorIncludesDependentEquipmentSelectors()
        {
            var equipmentSelector = new EquipmentSelector(_groups, _requiredDependentEquipmentsParametersList);

            //должен иметь зависимые продукты
            Assert.IsTrue(equipmentSelector.DependentEquipmentSelectors.Count == 1);
        }
    }
}
