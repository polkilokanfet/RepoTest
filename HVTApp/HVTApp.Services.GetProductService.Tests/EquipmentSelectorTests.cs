using System;
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
    public class EquipmentSelectorTests
    {
        private List<ParameterGroup> _groups;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private ParameterGroup _current, _voltage, _eqType;

        private List<RequiredDependentProductsParameters> _requiredDependentEquipmentsParametersList;
        private RequiredDependentProductsParameters _requiredDependentProductsParametersTransformatorsToBreker, _requiredDependentProductsParametersDriveToBreker, _requiredDependentProductsParametersReducerToDrive;
        private EquipmentSelector _equipmentSelector;

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter() { Value = "выключатель" };
            _transformator = new Parameter() { Value = "трансформатор" };
            _drive = new Parameter() {Value = "привод выключателя"};
            _drivesReducer = new Parameter() {Value = "редуктор"};
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

            _groups = new List<ParameterGroup> { _eqType, _voltage, _current };

            _requiredDependentProductsParametersTransformatorsToBreker = new RequiredDependentProductsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator },
                Count = 6
            };
            _requiredDependentProductsParametersDriveToBreker = new RequiredDependentProductsParameters
            {
                MainProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _drive },
                Count = 1
            };
            _requiredDependentProductsParametersReducerToDrive = new RequiredDependentProductsParameters
            {
                MainProductParameters = new List<Parameter> { _drive },
                ChildProductParameters = new List<Parameter> { _drivesReducer },
                Count = 3
            };
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentProductsParameters> { _requiredDependentProductsParametersTransformatorsToBreker, _requiredDependentProductsParametersDriveToBreker, _requiredDependentProductsParametersReducerToDrive };

            _equipmentSelector = new EquipmentSelector(_groups, new List<Part>(), new List<Product>(), _requiredDependentEquipmentsParametersList);
        }

        [TestMethod]
        public void EquipmentSelectorHasSelectedEquipment()
        {
            DependentEquipmentNotNull(_equipmentSelector.SelectedProduct);
        }
        //всё зависимое оборудование выбрано
        private void DependentEquipmentNotNull(Product product)
        {
            Assert.IsTrue(product != null);
            foreach (var dependentEquipment in product.DependentProducts)
            {
                DependentEquipmentNotNull(dependentEquipment);
            }
        }

        [TestMethod]
        public void EquipmentSelectorIncludesDependentEquipmentSelectors()
        {
            //должен иметь зависимые продукты (выключатель: трансформаторы + привод)
            Assert.AreEqual(_equipmentSelector.EquipmentSelectors.Count, _requiredDependentProductsParametersTransformatorsToBreker.Count +
                                                                         _requiredDependentProductsParametersDriveToBreker.Count);

            //должен иметь зависимые продукты (привод: редуктор)
            EquipmentSelector driveEquipmentSelector = _equipmentSelector.EquipmentSelectors.Single(x => x.ProductSelector.SelectedParameters.Contains(_drive));
            Assert.AreEqual(driveEquipmentSelector.EquipmentSelectors.Count, _requiredDependentProductsParametersReducerToDrive.Count);
        }

        [TestMethod]
        public void EquipmentSelectorPreSelectedParametersOfDependentEquipment()
        {
            EquipmentSelector driveEquipmentSelector = _equipmentSelector.EquipmentSelectors.Single(x => x.ProductSelector.SelectedParameters.Contains(_drive));
            Assert.IsTrue(driveEquipmentSelector.ProductSelector.SelectedParameters.AllMembersAreSame(new[] { _drive }));
            Assert.IsTrue(driveEquipmentSelector.ProductSelector.SelectedPart.Parameters.AllMembersAreSame(new[] { _drive }));
        }

        [TestMethod]
        public void EquipmentSelectorSelectParameters()
        {
            ParameterSelector parameterSelector = _equipmentSelector.ProductSelector.ParameterSelectors.Single(x => Equals(x.SelectedParameterWithActualFlag.Parameter, _breaker));
            parameterSelector.SetSelectedParameterWithActualFlag(_drive);

            Assert.IsTrue(_equipmentSelector.SelectedProduct.Part.Parameters.AllMembersAreSame(new[] { _drive }));
            Assert.AreEqual(_equipmentSelector.SelectedProduct.DependentProducts.Count, _requiredDependentProductsParametersReducerToDrive.Count);
        }

        [TestMethod]
        public void EquipmentSelectorPreSelectedEquipment()
        {
            List<Part> products = new List<Part>();
            List<Product> equipments = new List<Product>();
            EquipmentSelector equipmentSelector1 = new EquipmentSelector(_groups, products, equipments, _requiredDependentEquipmentsParametersList);

            ParameterSelector parameterSelector = equipmentSelector1.ProductSelector.ParameterSelectors.Single(x => Equals(x.SelectedParameterWithActualFlag.Parameter, _breaker));
            parameterSelector.SetSelectedParameterWithActualFlag(_drive);
            Product refProduct = equipmentSelector1.SelectedProduct;

            EquipmentSelector equipmentSelector2 = new EquipmentSelector(_groups, products, equipments, _requiredDependentEquipmentsParametersList, 
                preSelectedProduct: refProduct);

            //продукты совпадают
            Assert.AreEqual(equipmentSelector2.SelectedProduct, refProduct);
            //параметры главных продуктов совпадают
            Assert.IsTrue(equipmentSelector2.SelectedProduct.Part.Parameters.AllMembersAreSame(equipmentSelector1.SelectedProduct.Part.Parameters));
            //параметры выбранного продукта и выбранные параметры селектора совпадают
            Assert.IsTrue(equipmentSelector2.SelectedProduct.Part.Parameters.AllMembersAreSame(equipmentSelector2.ProductSelector.SelectedParameters));

            Assert.IsTrue(GetParts(refProduct).AllMembersAreSame(GetParts(equipmentSelector2.SelectedProduct)));
        }

        IEnumerable<Part> GetParts(Product product)
        {
            yield return product.Part;

            foreach (var dependentEquipment in product.DependentProducts)
            {
                foreach (var part in GetParts(dependentEquipment))
                {
                    yield return part;
                }
            }
        }
    }
}
