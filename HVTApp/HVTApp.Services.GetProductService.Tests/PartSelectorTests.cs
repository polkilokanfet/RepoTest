using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class PartSelectorTests
    {
        private List<ParameterGroup> _groups;
        private List<Parameter> _parameters;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private Parameter _transCurrent, _transVoltage;
        private ProductBlockSelector _productBlockSelector;
        private ProductBlock _preSelectedProductBlock;
        private ParameterGroup _current = new ParameterGroup();
        private ParameterGroup _voltage = new ParameterGroup();
        private ParameterGroup _eqType = new ParameterGroup();
        private ParameterGroup _transformatorType = new ParameterGroup();

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter { ParameterGroup = _eqType, Value = "выключатель" };
            _transformator = new Parameter { ParameterGroup = _eqType, Value = "трансформатор" };
            _drive = new Parameter { ParameterGroup = _eqType, Value = "привод выключателя" };
            _drivesReducer = new Parameter { ParameterGroup = _eqType, Value = "редкутор" };

            _v110 = new Parameter { ParameterGroup = _voltage, Value = "110кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v220 = new Parameter { ParameterGroup = _voltage, Value = "220кВ" }.AddRequiredPreviousParameters(new[] { _breaker });
            _v500 = new Parameter { ParameterGroup = _voltage, Value = "500кВ" }.AddRequiredPreviousParameters(new[] { _breaker });

            _c2500 = new Parameter { ParameterGroup = _current, Value = "2500А" }.
                AddRequiredPreviousParameters(new[] { _breaker, _v110 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v220 });
            _c3150 = new Parameter { ParameterGroup = _current, Value = "3150А" }.
                AddRequiredPreviousParameters(new[] { _breaker, _v110 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v220 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v500 });
            _c0001 = new Parameter { ParameterGroup = _current, Value = "1А" }.AddRequiredPreviousParameters(new[] { _transformator });
            _c0005 = new Parameter { ParameterGroup = _current, Value = "5А" }.AddRequiredPreviousParameters(new[] { _transformator });

            _transCurrent = new Parameter { ParameterGroup = _transformatorType, Value = "TT" }.AddRequiredPreviousParameters(new[] { _transformator });
            _transVoltage = new Parameter { ParameterGroup = _transformatorType, Value = "ТН" }.AddRequiredPreviousParameters(new[] { _transformator });

            _groups = new List<ParameterGroup> {_eqType, _voltage, _current, _transformatorType };
            _parameters = new List<Parameter> {_breaker, _transformator, _drive, _drivesReducer, _v110, _v220, _v500, _c2500, _c3150, _c0001, _c0005, _transCurrent, _transVoltage};

            






            _preSelectedProductBlock = new ProductBlock {Parameters = new List<Parameter> {_breaker, _v500, _c3150} };

            _productBlockSelector = new ProductBlockSelector(_parameters);
        }

        [TestMethod]
        public void ProductBlockSelectorIncludsAllGroups()
        {
            Assert.IsTrue(_productBlockSelector.ParameterSelectors.Count == _groups.Count);
        }

        [TestMethod]
        public void PartSelectorDefaultProduct()
        {
            var parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            Assert.IsTrue(_productBlockSelector.SelectedProductBlock.Parameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_productBlockSelector.ParameterSelectors.Select(x => x.SelectedParameterFlaged.Parameter).AllMembersAreSame(parameters));
        }

        [TestMethod]
        public void ProductBlockSelectorRequiredParameters()
        {
            var requiredParameters = new List<Parameter> { _transformator, _c0005 };
            var productBlockSelector = new ProductBlockSelector(_parameters);

            foreach (var requiredParameter in requiredParameters)
            {
                //находим селектор
                ParameterSelector parameterSelector = productBlockSelector.ParameterSelectors.Single(x => x.ParametersFlaged.Select(p => p.Parameter).Contains(requiredParameter));
                Assert.AreEqual(parameterSelector.ParametersFlaged.Count, 1);
                Assert.AreEqual(parameterSelector.SelectedParameterFlaged.Parameter, requiredParameter);
            }

            Assert.IsTrue(productBlockSelector.ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null).All(x => x.IsActual));
        }

        [TestMethod]
        public void ProductBlockSelectorSelectParameter()
        {
            var parameters = new List<Parameter> { _transformator, _c0001, _transCurrent };
            //находим селектор с типами оборудования
            var parameterSelector = GetParameterSelector(_productBlockSelector, _eqType);
            parameterSelector.SelectedParameterFlaged = parameterSelector.ParametersFlaged.Single(x => x.Parameter.Equals(_transformator));

            Assert.IsTrue(_productBlockSelector.SelectedProductBlock.Parameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_productBlockSelector.ParameterSelectors.Select(x => x.SelectedParameterFlaged.Parameter).AllMembersAreSame(parameters));

            //находим селектор с токами
            ParameterSelector parameterSelector2 = _productBlockSelector.ParameterSelectors.
                Single(x => x.ParametersFlaged.Select(pf => pf.Parameter).All(p => Equals(p.ParameterGroup, _current)));
            //актуальны параметры с током 1 и 5
            Assert.IsTrue(parameterSelector2.ParametersFlaged.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c0001, _c0005 }));
        }

        [TestMethod]
        public void ProductBlockSelectorPreSelectedPart()
        {
            ProductBlockSelector productBlockSelector = new ProductBlockSelector(_parameters, _preSelectedProductBlock.Parameters);

            Assert.AreEqual(_preSelectedProductBlock, productBlockSelector.SelectedProductBlock);
            Assert.IsTrue(_preSelectedProductBlock.Parameters.AllMembersAreSame(productBlockSelector.SelectedProductBlock.Parameters));
        }

        [TestMethod]
        public void ProductBlockSelectorActualParameters()
        {
            ProductBlockSelector productBlockSelector = new ProductBlockSelector(_parameters, _preSelectedProductBlock.Parameters);

            //находим селектор с токами
            var currentSelector = GetParameterSelector(productBlockSelector, _current);
            //актуален только параметр с током 3150
            Assert.IsTrue(currentSelector.ParametersFlaged.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c3150 }));

            //находим селектор с напряженями
            var voltageSelector = GetParameterSelector(productBlockSelector, _voltage);
            //актуалены все напряжения
            Assert.IsTrue(voltageSelector.ParametersFlaged.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(_parameters.Where(x => x.ParameterGroup.Equals(_voltage))));
        }

        //когда прилетают неупорядоченные группы параметров
        [TestMethod]
        public void ProductBlockSelectorDefaultProductNotOrderedParameters()
        {
            var eqType = new List<Parameter> {_breaker, _transformator};
            var voltage = new List<Parameter> {_v110, _v220};
            var current = new List<Parameter> {_c0005, _c2500 };

            var parametersList = new List<List<Parameter>> {current, voltage, eqType};

            var parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            var productBlockSelector = new ProductBlockSelector(_parameters);

            Assert.IsTrue(productBlockSelector.ParameterSelectors.Select(x => x.SelectedParameterFlaged.Parameter).AllMembersAreSame(parameters));
            Assert.IsTrue(productBlockSelector.SelectedProductBlock.Parameters.AllMembersAreSame(parameters));

            //проверяем верна ли последовательность
            for (int i = 0; i < parametersList.Count; i++)
            {
                Assert.AreEqual(parameters[i], productBlockSelector.SelectedProductBlock.Parameters.ToList()[i]);
            }
        }

        private ParameterSelector GetParameterSelector(ProductBlockSelector productBlockSelector, ParameterGroup group)
        {
            return productBlockSelector.ParameterSelectors.Single(x => x.ParametersFlaged.Select(p => p.Parameter).All(p => Equals(p.ParameterGroup, group)));
        }
    }
}