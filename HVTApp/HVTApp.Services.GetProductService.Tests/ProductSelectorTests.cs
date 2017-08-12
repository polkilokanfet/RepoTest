using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetEquipmentService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class ProductSelectorTests
    {
        private List<ParameterGroup> _groups;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private Parameter _transCurrent, _transVoltage;
        private ProductSelector _productSelector;
        private Product _preSelectedProduct;
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

            _preSelectedProduct = new Product {Parameters = new List<Parameter> {_breaker, _v500, _c3150} };

            _productSelector = new ProductSelector(_groups.Select(x => x.Parameters), new List<Product>());
        }

        [TestMethod]
        public void ProductSelectorIncludsAllGroups()
        {
            Assert.IsTrue(_productSelector.ParameterSelectors.Count == _groups.Count);
        }

        [TestMethod]
        public void ProductSelectorDefaultProduct()
        {
            List<Parameter> parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            Assert.IsTrue(_productSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_productSelector.SelectedProduct.Parameters.AllMembersAreSame(parameters));
        }

        [TestMethod]
        public void ProductSelectorRequiredParameters()
        {
            List<Parameter> requiredParameters = new List<Parameter> { _transformator, _c0005 };
            ProductSelector productSelector = new ProductSelector(_groups.Select(x => x.Parameters), new List<Product>(), requiredParameters);

            foreach (var requiredParameter in requiredParameters)
            {
                //находим селектор
                ParameterSelector parameterSelector = productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(requiredParameter));
                Assert.AreEqual(parameterSelector.ParametersWithActualFlag.Count, 1);
                Assert.AreEqual(parameterSelector.SelectedParameterWithActualFlag.Parameter, requiredParameter);
            }

            Assert.IsTrue(productSelector.ParameterSelectors.Select(x => x.SelectedParameterWithActualFlag).Where(x => x != null).All(x => x.IsActual));
        }

        [TestMethod]
        public void ProductSelectorSelectParameter()
        {
            List<Parameter> parameters = new List<Parameter> { _transformator, _c0001, _transCurrent };
            //находим селектор с типами оборудования
            ParameterSelector parameterSelector = _productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_eqType.Parameters));
            parameterSelector.SetSelectedParameterWithActualFlag(_transformator);

            Assert.IsTrue(_productSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(_productSelector.SelectedProduct.Parameters.AllMembersAreSame(parameters));

            //находим селектор с токами
            ParameterSelector parameterSelector2 = _productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            //актуальны параметры с током 1 и 5
            Assert.IsTrue(parameterSelector2.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c0001, _c0005 }));
        }

        [TestMethod]
        public void ProductSelectorPreSelectedProduct()
        {
            ProductSelector productSelector = new ProductSelector(_groups.Select(x => x.Parameters), new List<Product> { _preSelectedProduct }, null, _preSelectedProduct);

            Assert.AreEqual(_preSelectedProduct, productSelector.SelectedProduct);
            Assert.IsTrue(_preSelectedProduct.Parameters.AllMembersAreSame(productSelector.SelectedParameters));
        }

        [TestMethod]
        public void ProductSelectorActualParameters()
        {
            ProductSelector productSelector = new ProductSelector(_groups.Select(x => x.Parameters), new List<Product> { _preSelectedProduct }, null, _preSelectedProduct);

            //находим селектор с токами
            ParameterSelector parameterSelector = productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            //актуален только параметр с током 3150
            Assert.IsTrue(parameterSelector.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(new[] { _c3150 }));

            //находим селектор с напряженями
            ParameterSelector parameterSelector2 = productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_voltage.Parameters));
            //актуалены все напряжения
            Assert.IsTrue(parameterSelector2.ParametersWithActualFlag.Where(x => x.IsActual).Select(x => x.Parameter).AllMembersAreSame(_voltage.Parameters));
        }

        [TestMethod]
        public void ProductSelectorCreateProducts()
        {
            List<Product> products = new List<Product>();
            ProductSelector productSelector = new ProductSelector(_groups.Select(x => x.Parameters), products);

            Assert.AreEqual(products.Count, 1);

            //находим селектор с типами оборудования
            ParameterSelector parameterSelector = productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_eqType.Parameters));
            parameterSelector.SetSelectedParameterWithActualFlag(_transformator);

            Assert.AreEqual(products.Count, 2);

            //находим селектор с токами
            ParameterSelector parameterSelector2 = productSelector.ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).AllMembersAreSame(_current.Parameters));
            parameterSelector2.SetSelectedParameterWithActualFlag(_c0005);

            Assert.AreEqual(products.Count, 3);
        }

        //когда прилетают неупоряноченные группы параметров
        [TestMethod]
        public void ProductSelectorDefaultProductNotOrderedParameters()
        {
            List<Parameter> eqType = new List<Parameter>() {_breaker, _transformator};
            List<Parameter> voltage = new List<Parameter>() {_v110, _v220};
            List<Parameter> current = new List<Parameter>() {_c0005, _c2500 };

            List<List<Parameter>> parametersList = new List<List<Parameter>>() {current, voltage, eqType};

            List<Parameter> parameters = new List<Parameter> { _breaker, _v110, _c2500 };
            ProductSelector productSelector = new ProductSelector(parametersList, new List<Product>());

            Assert.IsTrue(productSelector.SelectedParameters.AllMembersAreSame(parameters));
            Assert.IsTrue(productSelector.SelectedProduct.Parameters.AllMembersAreSame(parameters));

            //проверяем верна ли последовательность
            for (int i = 0; i < parametersList.Count; i++)
            {
                Assert.AreEqual(parameters[i], productSelector.SelectedParameters.ToList()[i]);
            }
        }


    }
}