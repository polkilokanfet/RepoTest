using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;
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
        private ParameterGroup _current, _voltage, _eqType;

        private List<ProductsRelation> _requiredDependentEquipmentsParametersList;
        private ProductsRelation _productsRelationTransformatorsToBreker, _productsRelationDriveToBreker, _productsRelationReducerToDrive;
        private ProductSelector _productSelector;

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

            _productsRelationTransformatorsToBreker = new ProductsRelation
            {
                ParentProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator },
                Count = 6
            };
            _productsRelationDriveToBreker = new ProductsRelation
            {
                ParentProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _drive },
                Count = 1
            };
            _productsRelationReducerToDrive = new ProductsRelation
            {
                ParentProductParameters = new List<Parameter> { _drive },
                ChildProductParameters = new List<Parameter> { _drivesReducer },
                Count = 3
            };
            _requiredDependentEquipmentsParametersList = new List<ProductsRelation> { _productsRelationTransformatorsToBreker, _productsRelationDriveToBreker, _productsRelationReducerToDrive };

            _productSelector = new ProductSelector(_groups, new List<Part>(), new List<Product>(), _requiredDependentEquipmentsParametersList);
        }

        [TestMethod]
        public void ProductSelectorHasSelectedProduct()
        {
            DependentProductNotNull(_productSelector.SelectedProduct);
        }

        //всё зависимое оборудование выбрано
        private void DependentProductNotNull(Product product)
        {
            Assert.IsTrue(product != null);
            foreach (var dependentEquipment in product.DependentProducts)
            {
                DependentProductNotNull(dependentEquipment);
            }
        }

        [TestMethod]
        public void ProductSelectorIncludesDependentProductSelectors()
        {
            //должен иметь зависимые продукты (выключатель: трансформаторы + привод)
            Assert.AreEqual(_productSelector.ProductSelectors.Count(x => x.IsActual), _productsRelationTransformatorsToBreker.Count +
                                                                         _productsRelationDriveToBreker.Count);

            //должен иметь зависимые продукты (привод: редуктор)
            ProductSelector driveProductSelector = _productSelector.ProductSelectors.Single(x => x.PartSelector.SelectedParameters.Contains(_drive));
            Assert.AreEqual(driveProductSelector.ProductSelectors.Count, _productsRelationReducerToDrive.Count);
        }

        [TestMethod]
        public void ProductSelectorPreSelectedParametersOfDependentProduct()
        {
            ProductSelector driveProductSelector = _productSelector.ProductSelectors.Single(x => x.PartSelector.SelectedParameters.Contains(_drive));
            Assert.IsTrue(driveProductSelector.PartSelector.SelectedParameters.AllMembersAreSame(new[] { _drive }));
            Assert.IsTrue(driveProductSelector.PartSelector.SelectedPart.Parameters.AllMembersAreSame(new[] { _drive }));
        }

        [TestMethod]
        public void ProductSelectorSelectParameters()
        {
            ParameterSelector parameterSelector = _productSelector.PartSelector.ParameterSelectors.Single(x => Equals(x.SelectedParameterFlaged.Parameter, _breaker));
            parameterSelector.SelectedParameter = (_drive);

            Assert.IsTrue(_productSelector.SelectedProduct.Part.Parameters.AllMembersAreSame(new[] { _drive }));
            Assert.AreEqual(_productSelector.SelectedProduct.DependentProducts.Count, _productsRelationReducerToDrive.Count);
        }

        [TestMethod]
        public void ProductSelectorPreSelectedProduct()
        {
            List<Part> parts = new List<Part>();
            List<Product> products = new List<Product>();
            ProductSelector productSelector1 = new ProductSelector(_groups, parts, products, _requiredDependentEquipmentsParametersList);

            ParameterSelector parameterSelector = productSelector1.PartSelector.ParameterSelectors.Single(x => Equals(x.SelectedParameterFlaged.Parameter, _breaker));
            parameterSelector.SelectedParameter = (_drive);
            Product refProduct = productSelector1.SelectedProduct;

            ProductSelector productSelector2 = new ProductSelector(_groups, parts, products, _requiredDependentEquipmentsParametersList, 
                preSelectedProduct: refProduct);

            //продукты совпадают
            Assert.AreEqual(productSelector2.SelectedProduct, refProduct);
            //параметры главных продуктов совпадают
            Assert.IsTrue(productSelector2.SelectedProduct.Part.Parameters.AllMembersAreSame(productSelector1.SelectedProduct.Part.Parameters));
            //параметры выбранного продукта и выбранные параметры селектора совпадают
            Assert.IsTrue(productSelector2.SelectedProduct.Part.Parameters.AllMembersAreSame(productSelector2.PartSelector.SelectedParameters));

            Assert.IsTrue(GetParts(refProduct).AllMembersAreSame(GetParts(productSelector2.SelectedProduct)));
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

    [TestClass]
    public class TestClass2
    {
        [TestMethod]
        public void ProductSelectorPreSelectedProduct2()
        {
            TestData testData = new TestData();
            var groups = new List<ParameterGroup> {testData.ParameterGroupBreakerType, testData.ParameterGroupEqType, testData.ParameterGroupTransformatorType, testData.ParameterGroupVoltage, testData.ParameterGroupDrivesVoltage};
            var parts = new List<Part> {testData.PartBreakesDrive, testData.PartVeb110, testData.PartVgb35, testData.PartZng110};
            var products = new List<Product> {testData.ProductBreakersDrive, testData.ProductVeb110, testData.ProductZng110};
            var rdpp = new List<ProductsRelation> {testData.RequiredChildProductRelationBreakerBlock, testData.RequiredChildProductRelationDrive};

            var productSelector = new ProductSelector(groups, parts, products, rdpp, preSelectedProduct: testData.ProductVeb110);
            var parts1 = GetParts(productSelector.SelectedProduct);
            var parts2 = GetParts(testData.ProductVeb110);
            Assert.IsTrue(parts1.AllMembersAreSame(parts2));

            var drive = testData.ProductBreakersDrive;
            var breaker = testData.ProductVeb110;
            breaker.DependentProducts.Remove(drive);
            breaker.DependentProducts.Add(new Product() {Part = new Part() {Parameters = new List<Parameter>() {testData.ParameterBrakersDrive, testData.ParameterVoltage110V} } });

            var productSelector2 = new ProductSelector(groups, parts, products, rdpp, preSelectedProduct: breaker);
            var parts3 = GetParts(productSelector2.SelectedProduct);
            var parts4 = GetParts(breaker);
            Assert.IsTrue(parts3.AllMembersAreSame(parts4));
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
