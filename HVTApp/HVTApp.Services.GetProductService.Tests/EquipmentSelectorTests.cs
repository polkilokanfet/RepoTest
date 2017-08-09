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
        private Parameter _breaker, _transformator;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private ParameterGroup _current, _voltage, _eqType;

        private List<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;
        private RequiredDependentEquipmentsParameters _requiredDependentEquipmentsParameters;
        private EquipmentSelector _equipmentSelector;

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter() { Value = "выключатель" };
            _transformator = new Parameter() { Value = "трансформатор" };
            _eqType = new ParameterGroup().AddParameters(new[] { _breaker, _transformator });

            _v110 = new Parameter() { Value = "110кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v220 = new Parameter() { Value = "220кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v500 = new Parameter() { Value = "500кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _voltage = new ParameterGroup().AddParameters(new[] { _v110, _v220, _v500 });

            _c2500 = new Parameter() { Value = "2500А" }.AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v220 });
            _c3150 = new Parameter() { Value = "3150А" }.AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v220 })
                                                        .AddRequiredPreviousParameters(new[] { _breaker, _v500 });
            _c0001 = new Parameter() { Value = "1А" }.AddRequiredPreviousParameters(new[] { _transformator });
            _c0005 = new Parameter() { Value = "5А" }.AddRequiredPreviousParameters(new[] { _transformator });
            _current = new ParameterGroup().AddParameters(new[] { _c2500, _c3150, _c0001, _c0005 });

            _groups = new List<ParameterGroup> { _eqType, _voltage, _current };

            _requiredDependentEquipmentsParameters = new RequiredDependentEquipmentsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator },
                Count = 6
            };
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentEquipmentsParameters> { _requiredDependentEquipmentsParameters };

            _equipmentSelector = new EquipmentSelector(_groups, new List<Product>(), new List<Equipment>(), _requiredDependentEquipmentsParametersList);
        }

        [TestMethod]
        public void EquipmentSelectorHasSelectedEquipment()
        {
            Assert.IsTrue(_equipmentSelector.SelectedEquipment != null);
        }

        [TestMethod]
        public void EquipmentSelectorIncludesDependentEquipmentSelectors()
        {
            //должен иметь зависимые продукты
            Assert.AreEqual(_equipmentSelector.DependentEquipmentSelectors.Count, _requiredDependentEquipmentsParameters.Count);
        }
    }
}
