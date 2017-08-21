using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetEquipmentService;

namespace HVTApp.Services.GetProductService
{
    public class EquipmentSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Part> _products;
        private readonly IList<Product> _equipments;
        private readonly IEnumerable<RequiredDependentProductsParameters> _requiredDependentEquipmentsParametersList;

        private Product _selectedProduct;

        public ProductSelector ProductSelector { get; }
        public ObservableCollection<EquipmentSelector> EquipmentSelectors { get; }


        public EquipmentSelector(IEnumerable<ParameterGroup> groups, 
                                 IList<Part> products, 
                                 IList<Product> equipments, 
                                 IEnumerable<RequiredDependentProductsParameters> requiredDependentEquipmentsParametersList, 
                                 IEnumerable<Parameter> requiredProductsParameters = null,
                                 Product preSelectedProduct = null)
        {
            _groups = new List<ParameterGroup>(groups);
            _products = products;
            _equipments = equipments;
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentProductsParameters>(requiredDependentEquipmentsParametersList);

            //продукт
            ProductSelector = new ProductSelector(_groups.Select(x => x.Parameters), _products, requiredProductsParameters, preSelectedProduct?.Part);
            ProductSelector.SelectedProductChanged += OnMainProductChanged;

            //дочернее оборудование
            EquipmentSelectors = new ObservableCollection<EquipmentSelector>();
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
                if(!_equipments.Contains(value)) _equipments.Add(value);

                OnSelectedEquipmentChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private Product GetEquipment()
        {
            var dependentEquipment = EquipmentSelectors.Select(x => x.SelectedProduct).ToList();
            var result = _equipments.SingleOrDefault(x => Equals(x.Part, ProductSelector.SelectedPart) &&
                                                          x.DependentProducts.AllMembersAreSame(dependentEquipment));
            if (result == null)
            {
                result = new Product
                {
                    Part = ProductSelector.SelectedPart,
                    DependentProducts = dependentEquipment
                };
                _equipments.Add(result);
            }
            return result;
        }

        /// <summary>
        /// Параметры, необходимые зависимому оборудованию
        /// </summary>
        IEnumerable<RequiredDependentProductsParameters> RequiredDependentEquipmentsParameterses => _requiredDependentEquipmentsParametersList
            .Where(x => x.MainProductParameters.AllContainsIn(ProductSelector.SelectedParameters));

        private void RefreshDependentEquipments()
        {
            //исключаем не актуальное дочернее оборудование
            foreach (var equipmentSelector in EquipmentSelectors
                .Where(des => !RequiredDependentEquipmentsParameterses.Any(x => x.ChildProductParameters.AllMembersAreSame(des.ProductSelector.GetRequaredParameters()))).ToList())
            {
                EquipmentSelectors.Remove(equipmentSelector);
                equipmentSelector.SelectedEquipmentChanged -= OnDependentSelectedEquipmentChanged;
            }

            //добавляем актуальное дочернее оборудование
            foreach (var requiredDependentEquipmentsParameters in RequiredDependentEquipmentsParameterses
                .Where(x => !EquipmentSelectors.Any(des => des.ProductSelector.GetRequaredParameters().AllMembersAreSame(x.ChildProductParameters))))
            {
                for (int i = 0; i < requiredDependentEquipmentsParameters.Count; i++)
                {
                    var equipmentSelector = new EquipmentSelector(_groups, _products, _equipments, _requiredDependentEquipmentsParametersList, requiredDependentEquipmentsParameters.ChildProductParameters);
                    EquipmentSelectors.Add(equipmentSelector);
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