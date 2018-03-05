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
        private List<Parameter> _parameters;
        private Parameter _breaker, _transformator, _drive, _drivesReducer;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150, _c0001, _c0005;
        private ParameterGroup _current, _voltage, _eqType;

        private List<ProductRelation> _requiredDependentEquipmentsParametersList;
        private ProductRelation _productsRelationTransformatorsToBreker, _productsRelationDriveToBreker, _productsRelationReducerToDrive;
        private ProductSelector _productSelector;

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter { ParameterGroup = _eqType, Value = "выключатель" };
            _transformator = new Parameter { ParameterGroup = _eqType, Value = "трансформатор" };
            _drive = new Parameter { ParameterGroup = _eqType, Value = "привод выключателя" };
            _drivesReducer = new Parameter { ParameterGroup = _eqType, Value = "редуктор" };

            _v110 = new Parameter { ParameterGroup = _voltage, Value = "110кВ" }.
                AddRequiredPreviousParameters(new[] { _breaker });
            _v220 = new Parameter { ParameterGroup = _voltage, Value = "220кВ" }.
                AddRequiredPreviousParameters(new[] { _breaker });
            _v500 = new Parameter { ParameterGroup = _voltage, Value = "500кВ" }.
                AddRequiredPreviousParameters(new[] { _breaker });

            _c2500 = new Parameter { ParameterGroup = _current, Value = "2500 А" }.
                AddRequiredPreviousParameters(new[] { _breaker, _v110 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v220 });
            _c3150 = new Parameter { ParameterGroup = _current, Value = "3150 А" }.
                AddRequiredPreviousParameters(new[] { _breaker, _v110 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v220 }).
                AddRequiredPreviousParameters(new[] { _breaker, _v500 });
            _c0001 = new Parameter { ParameterGroup = _current, Value = "1 А" }.
                AddRequiredPreviousParameters(new[] { _transformator });
            _c0005 = new Parameter { ParameterGroup = _current, Value = "5 А" }.
                AddRequiredPreviousParameters(new[] { _transformator });

            _parameters = new List<Parameter>(new[]
            {
                _breaker, _transformator, _drive, _drivesReducer, _v110, _v220, _v500, _c2500, _c3150, _c0001, _c0005
            });





            _groups = new List<ParameterGroup> { _eqType, _voltage, _current };

            _productsRelationTransformatorsToBreker = new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _transformator },
                Count = 6
            };
            _productsRelationDriveToBreker = new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { _breaker, _v110 },
                ChildProductParameters = new List<Parameter> { _drive },
                Count = 1
            };
            _productsRelationReducerToDrive = new ProductRelation
            {
                ParentProductParameters = new List<Parameter> { _drive },
                ChildProductParameters = new List<Parameter> { _drivesReducer },
                Count = 3
            };
            _requiredDependentEquipmentsParametersList = new List<ProductRelation>
            {
                _productsRelationTransformatorsToBreker,
                _productsRelationDriveToBreker,
                _productsRelationReducerToDrive
            };

            _productSelector = new ProductSelector(_parameters, null);
            ProductSelector.ProductRelations = _requiredDependentEquipmentsParametersList;
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
            Assert.AreEqual(_productSelector.ProductSelectors.Count, 
                _productsRelationTransformatorsToBreker.Count + _productsRelationDriveToBreker.Count);

            //должен иметь зависимые продукты (привод: редуктор)
            var driveProductSelector = _productSelector.ProductSelectors.Single(x => x.PartSelector.SelectedParameters.Contains(_drive));
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
            var testData = new TestData();
            var groups = testData.GetAll<ParameterGroup>().ToList();
            var parts = testData.GetAll<Part>().ToList();
            var products = testData.GetAll<Product>().ToList();
            var relations = testData.GetAll<ProductsRelation>().ToList();

            var productSelector = new ProductSelector(groups, parts, products, relations, preSelectedProduct: testData.ProductVeb110);
            var parts1 = GetParts(productSelector.SelectedProduct);
            var parts2 = GetParts(testData.ProductVeb110);
            Assert.IsTrue(parts1.AllMembersAreSame(parts2));

            var drive = testData.ProductBreakersDrive;
            var breaker = testData.ProductVeb110;
            breaker.DependentProducts.Remove(drive);
            breaker.DependentProducts.Add(new Product {Part = new Part {Parameters = new List<Parameter> {testData.ParameterBrakersDrive, testData.ParameterVoltage110V} } });

            var productSelector2 = new ProductSelector(groups, parts, products, relations, preSelectedProduct: breaker);
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
