using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetEquipmentService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class PartSelectorTests
    {
        private List<ParameterGroup> _groups;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private Parameter _transCurrent, _transVoltage;
        private PartSelector _partSelector;
        private Part _preSelectedPart;
        private ParameterGroup _current, _voltage, _eqType, _transformatorType;

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter() { Value = "выключатель" };
            _transformator = new Parameter() { Value = "трансформатор" };
            _drive = new Parameter() { Value = "привод выключателя" };
            _drivesReducer = new Parameter() { Value = "редкутор" };
            _eqType = new ParameterGroup().AddParameters(new[] { _breaker, _transformator, _drive, _drivesReducer });

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

            _transCurrent = new Parameter() { Value = "TT" }.AddRequiredPreviousParameters(new[] { _transformator });
            _transVoltage = new Parameter() { Value = "ТН" }.AddRequiredPreviousParameters(new[] { _transformator });
            _transformatorType = new ParameterGroup().AddParameters(new[] {_transCurrent, _transVoltage});

            _groups = new List<ParameterGroup> {_eqType, _voltage, _current, _transformatorType };

            _preSelectedPart = new Part {Parameters = new List<Parameter> {_breaker, _v500, _c3150} };

            _partSelector = new PartSelector(_groups.Select(x => x.Parameters), new List<Part>());
        }

        [TestMethod]
        public void PartSelectorIncludsAllGroups()
        {
            Assert.IsTrue(_partSelector.ParameterSelectors.Count == _groups.Count);
        }

        [TestMethod]
        public void PartSelectorDefaultProduct()
        {
            List<Parameter> parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            Assert.IsTrue(_partSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_partSelector.SelectedPart.Parameters.AllMembersAreSame(parameters));
        }

        [TestMethod]
        public void PartSelectorRequiredParameters()
        {
            List<Parameter> requiredParameters = new List<Parameter> { _transformator, _c0005 };
            PartSelector partSelector = new PartSelector(_groups.Select(x => x.Parameters), new List<Part>(), requiredParameters);

            foreach (var requiredParameter in requiredParameters)
            {
                //находим селектор
                ParameterSelector parameterSelector = partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(requiredParameter));
                Assert.AreEqual(parameterSelector.ParametersWithActualFlag.Count, 1);
                Assert.AreEqual(parameterSelector.SelectedParameterWithActualFlag.Parameter, requiredParameter);
            }

            Assert.IsTrue(partSelector.ParameterSelectors.Select(x => x.SelectedParameterWithActualFlag).Where(x => x != null).All(x => x.IsActual));
        }

        [TestMethod]
        public void PartSelectorSelectParameter()
        {
            List<Parameter> parameters = new List<Parameter> { _transformator, _c0001, _transCurrent };
            //находим селектор с типами оборудования
            ParameterSelector parameterSelector = _partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_eqType.Parameters));
            parameterSelector.SetSelectedParameterWithActualFlag(_transformator);

            Assert.IsTrue(_partSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_partSelector.SelectedPart.Parameters.AllMembersAreSame(parameters));

            //находим селектор с токами
            ParameterSelector parameterSelector2 = _partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            //актуальны параметры с током 1 и 5
            Assert.IsTrue(parameterSelector2.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c0001, _c0005 }));
        }

        [TestMethod]
        public void PartSelectorPreSelectedPart()
        {
            PartSelector partSelector = new PartSelector(_groups.Select(x => x.Parameters), new List<Part> { _preSelectedPart }, null, _preSelectedPart);

            Assert.AreEqual(_preSelectedPart, partSelector.SelectedPart);
            Assert.IsTrue(_preSelectedPart.Parameters.AllMembersAreSame(partSelector.SelectedParameters));
        }

        [TestMethod]
        public void PartSelectorActualParameters()
        {
            PartSelector partSelector = new PartSelector(_groups.Select(x => x.Parameters), new List<Part> { _preSelectedPart }, null, _preSelectedPart);

            //находим селектор с токами
            ParameterSelector parameterSelector = partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            //актуален только параметр с током 3150
            Assert.IsTrue(parameterSelector.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c3150 }));

            //находим селектор с напряженями
            ParameterSelector parameterSelector2 = partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_voltage.Parameters));
            //актуалены все напряжения
            Assert.IsTrue(parameterSelector2.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(_voltage.Parameters));
        }

        [TestMethod]
        public void PartSelectorCreateProducts()
        {
            List<Part> products = new List<Part>();
            PartSelector partSelector = new PartSelector(_groups.Select(x => x.Parameters), products);

            Assert.AreEqual(products.Count, 1);

            //находим селектор с типами оборудования
            ParameterSelector parameterSelector = partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_eqType.Parameters));
            parameterSelector.SetSelectedParameterWithActualFlag(_transformator);

            Assert.AreEqual(products.Count, 2);

            //находим селектор с токами
            ParameterSelector parameterSelector2 = partSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            parameterSelector2.SetSelectedParameterWithActualFlag(_c0005);

            Assert.AreEqual(products.Count, 3);
        }

        //когда прилетают неупоряноченные группы параметров
        [TestMethod]
        public void PartSelectorDefaultProductNotOrderedParameters()
        {
            List<Parameter> eqType = new List<Parameter>() {_breaker, _transformator};
            List<Parameter> voltage = new List<Parameter>() {_v110, _v220};
            List<Parameter> current = new List<Parameter>() {_c0005, _c2500 };

            List<List<Parameter>> parametersList = new List<List<Parameter>>() {current, voltage, eqType};

            List<Parameter> parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            PartSelector partSelector = new PartSelector(parametersList, new List<Part>());

            Assert.IsTrue(partSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(partSelector.SelectedPart.Parameters.AllMembersAreSame(parameters));

            //проверяем верна ли последовательность
            for (int i = 0; i < parametersList.Count; i++)
            {
                Assert.AreEqual(parameters[i], partSelector.SelectedParameters.ToList()[i]);
            }
        }
    }
}