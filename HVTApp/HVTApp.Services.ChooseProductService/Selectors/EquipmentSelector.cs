using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class EquipmentSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Product> _products;
        private readonly IList<Equipment> _equipments;
        private readonly IEnumerable<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;

        private Equipment _selectedEquipment;

        public ProductSelector ProductSelector { get; }
        public ObservableCollection<EquipmentSelector> EquipmentSelectors { get; }


        public EquipmentSelector(IEnumerable<ParameterGroup> groups, 
                                 IList<Product> products, 
                                 IList<Equipment> equipments, 
                                 IEnumerable<RequiredDependentEquipmentsParameters> requiredDependentEquipmentsParametersList, 
                                 IEnumerable<Parameter> requiredProductsParameters = null,
                                 Equipment preSelectedEquipment = null)
        {
            _groups = new List<ParameterGroup>(groups);
            _products = products;
            _equipments = equipments;
            _requiredDependentEquipmentsParametersList = new List<RequiredDependentEquipmentsParameters>(requiredDependentEquipmentsParametersList);

            //продукт
            ProductSelector = new ProductSelector(_groups.Select(x => x.Parameters), _products, requiredProductsParameters, preSelectedEquipment?.Product);
            ProductSelector.SelectedProductChanged += OnMainProductChanged;

            //дочернее оборудование
            EquipmentSelectors = new ObservableCollection<EquipmentSelector>();
            RefreshDependentEquipments();
            SelectedEquipment = GetEquipment();
        }


        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            private set
            {
                if (Equals(_selectedEquipment, value)) return;
                var oldValue = _selectedEquipment;
                _selectedEquipment = value;
                if(!_equipments.Contains(value)) _equipments.Add(value);

                OnSelectedEquipmentChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private Equipment GetEquipment()
        {
            var dependentEquipment = EquipmentSelectors.Select(x => x.SelectedEquipment).ToList();
            var result = _equipments.SingleOrDefault(x => Equals(x.Product, ProductSelector.SelectedProduct) &&
                                                          x.DependentEquipments.AllMembersAreSame(dependentEquipment));
            if (result == null)
            {
                result = new Equipment
                {
                    Product = ProductSelector.SelectedProduct,
                    DependentEquipments = dependentEquipment
                };
                _equipments.Add(result);
            }
            return result;
        }

        /// <summary>
        /// Параметры, необходимые зависимому оборудованию
        /// </summary>
        IEnumerable<RequiredDependentEquipmentsParameters> RequiredDependentEquipmentsParameterses => _requiredDependentEquipmentsParametersList
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

        private void OnDependentSelectedEquipmentChanged(Equipment oldEquipment, Equipment newEquipment)
        {
            SelectedEquipment = GetEquipment();
        }

        private void OnMainProductChanged(Product oldProduct, Product newProduct)
        {
            RefreshDependentEquipments();
            SelectedEquipment = GetEquipment();
        }


        public event Action<Equipment, Equipment> SelectedEquipmentChanged;
        protected virtual void OnSelectedEquipmentChanged(Equipment oldEquipment, Equipment newEquipment)
        {
            SelectedEquipmentChanged?.Invoke(oldEquipment, newEquipment);
        }
    }
}