using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Part> _parts;
        private readonly IList<Product> _products;
        private readonly IEnumerable<RequiredDependentProductsParameters> _requiredDependentProductsParametersList;

        private Product _selectedProduct;

        public PartSelector PartSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; }


        public ProductSelector(  IEnumerable<ParameterGroup> groups, 
                                 IList<Part> parts, 
                                 IList<Product> products, 
                                 IEnumerable<RequiredDependentProductsParameters> requiredDependentProductsParametersList, 
                                 IEnumerable<Parameter> requiredProductsParameters = null,
                                 Product preSelectedProduct = null)
        {
            _groups = new List<ParameterGroup>(groups);
            _parts = parts;
            _products = products;
            _requiredDependentProductsParametersList = new List<RequiredDependentProductsParameters>(requiredDependentProductsParametersList);

            //продукт
            PartSelector = new PartSelector(_groups.Select(x => x.Parameters), _parts, requiredProductsParameters, preSelectedProduct?.Part);
            PartSelector.SelectedProductChanged += OnMainProductChanged;

            //дочернее оборудование
            ProductSelectors = new ObservableCollection<ProductSelector>();
            RefreshDependentEquipments();
            SelectedProduct = GetEquipment();
        }


        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            private set
            {
                if (Equals(_selectedProduct, value)) return;
                var oldValue = _selectedProduct;
                _selectedProduct = value;
                if(!_products.Contains(value)) _products.Add(value);

                OnSelectedEquipmentChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private Product GetEquipment()
        {
            var dependentEquipment = ProductSelectors.Select(x => x.SelectedProduct).ToList();
            var result = _products.SingleOrDefault(x => Equals(x.Part, PartSelector.SelectedPart) &&
                                                          x.DependentProducts.AllMembersAreSame(dependentEquipment));
            if (result == null)
            {
                result = new Product
                {
                    Part = PartSelector.SelectedPart,
                    DependentProducts = dependentEquipment
                };
                _products.Add(result);
            }
            return result;
        }

        /// <summary>
        /// Параметры, необходимые зависимому оборудованию
        /// </summary>
        IEnumerable<RequiredDependentProductsParameters> RequiredDependentEquipmentsParameterses => _requiredDependentProductsParametersList
            .Where(x => x.MainProductParameters.AllContainsIn(PartSelector.SelectedParameters));

        private void RefreshDependentEquipments()
        {
            //исключаем не актуальное дочернее оборудование
            foreach (var equipmentSelector in ProductSelectors
                .Where(des => !RequiredDependentEquipmentsParameterses.Any(x => x.ChildProductParameters.AllMembersAreSame(des.PartSelector.GetRequaredParameters()))).ToList())
            {
                ProductSelectors.Remove(equipmentSelector);
                equipmentSelector.SelectedEquipmentChanged -= OnDependentSelectedEquipmentChanged;
            }

            //добавляем актуальное дочернее оборудование
            foreach (var requiredDependentEquipmentsParameters in RequiredDependentEquipmentsParameterses
                .Where(x => !ProductSelectors.Any(des => des.PartSelector.GetRequaredParameters().AllMembersAreSame(x.ChildProductParameters))))
            {
                for (int i = 0; i < requiredDependentEquipmentsParameters.Count; i++)
                {
                    var equipmentSelector = new ProductSelector(_groups, _parts, _products, _requiredDependentProductsParametersList, requiredDependentEquipmentsParameters.ChildProductParameters);
                    ProductSelectors.Add(equipmentSelector);
                    equipmentSelector.SelectedEquipmentChanged += OnSelectedEquipmentChanged;
                }
            }
        }

        private void OnDependentSelectedEquipmentChanged(Product oldProduct, Product newProduct)
        {
            SelectedProduct = GetEquipment();
        }

        private void OnMainProductChanged(Part oldPart, Part newPart)
        {
            RefreshDependentEquipments();
            SelectedProduct = GetEquipment();
        }


        public event Action<Product, Product> SelectedEquipmentChanged;
        protected virtual void OnSelectedEquipmentChanged(Product oldProduct, Product newProduct)
        {
            SelectedEquipmentChanged?.Invoke(oldProduct, newProduct);
        }
    }
}