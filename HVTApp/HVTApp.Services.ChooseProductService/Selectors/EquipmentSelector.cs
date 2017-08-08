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

        public EquipmentSelector(IEnumerable<ParameterGroup> groups, IEnumerable<RequiredDependentEquipmentsParameters> requiredDependentEquipmentsParametersList) : 
            this(groups, new List<Product>(), new List<Equipment>(), requiredDependentEquipmentsParametersList)
        {
        }

        public EquipmentSelector(
            IEnumerable<ParameterGroup> groups, 
            IList<Product> products, 
            IList<Equipment> equipments, 
            IEnumerable<RequiredDependentEquipmentsParameters> requiredDependentEquipmentsParametersList, 
            IEnumerable<Parameter> requiredProductsParameters = null)
        {
            RequiredProductsParameters = requiredProductsParameters;
            _groups = groups;
            _products = products;
            _equipments = equipments;
            _requiredDependentEquipmentsParametersList = requiredDependentEquipmentsParametersList;

            //продукт
            ProductSelector = new ProductSelector(_groups.Select(x => x.Parameters), _products, RequiredProductsParameters);
            ProductSelector.SelectedProductChanged += OnMainProductChanged;

            //дочернее оборудование
            DependentEquipmentSelectors = new ObservableCollection<EquipmentSelector>();
            RefreshDependentEquipments();
            SelectedEquipment = GetEquipment();
        }

        private void OnMainProductChanged(Product oldProduct, Product newProduct)
        {
            RefreshDependentEquipments();
            SelectedEquipment = GetEquipment();
        }

        public IEnumerable<Parameter> RequiredProductsParameters { get; }
        public ProductSelector ProductSelector { get; }
        public ObservableCollection<EquipmentSelector> DependentEquipmentSelectors { get; }

        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                if (Equals(_selectedEquipment, value)) return;
                var oldValue = _selectedEquipment;
                _selectedEquipment = value;

                OnSelectedEquipmentChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private Equipment GetEquipment()
        {
            var dependentEquipment = DependentEquipmentSelectors.Select(x => SelectedEquipment);
            var result = _equipments.SingleOrDefault(x => Equals(x.Product, ProductSelector.SelectedProduct) &&
                                                          x.DependentEquipments.AllMembersAreSame(dependentEquipment));
            if (result == null)
            {
                result = new Equipment
                {
                    Product = ProductSelector.SelectedProduct,
                    DependentEquipments = new List<Equipment>(dependentEquipment)
                };
                _equipments.Add(result);
            }
            return result;
        }

        IEnumerable<IEnumerable<Parameter>> DependentEquipmentParametersList => 
                _requiredDependentEquipmentsParametersList.Where(x => x.MainProductParameters.All(p => ProductSelector.SelectedParameters.Contains(p))).
                                                           Select(x => x.ChildProductParameters);

        private void RefreshDependentEquipments()
        {

            //исключаем не актуальное дочернее оборудование
            foreach (var equipmentSelector in DependentEquipmentSelectors
                .Where(des => !DependentEquipmentParametersList.Any(x => x.AllMembersAreSame(des.RequiredProductsParameters))))
            {
                DependentEquipmentSelectors.Remove(equipmentSelector);
                equipmentSelector.SelectedEquipmentChanged -= OnDependentSelectedEquipmentChanged;
            }

            //добавляем актуальное дочернее оборудование
            foreach (var dependentEquipmentsParameters in DependentEquipmentParametersList
                .Where(x => !DependentEquipmentSelectors.Any(des => des.RequiredProductsParameters.AllMembersAreSame(x))))
            {
                var equipmentSelector = new EquipmentSelector(_groups, _products, _equipments, _requiredDependentEquipmentsParametersList, dependentEquipmentsParameters);
                DependentEquipmentSelectors.Add(equipmentSelector);
                equipmentSelector.SelectedEquipmentChanged += OnSelectedEquipmentChanged;
            }
        }

        private void OnDependentSelectedEquipmentChanged(Equipment oldEquipment, Equipment newEquipment)
        {
            SelectedEquipment = GetEquipment();
        }


        public event Action<Equipment, Equipment> SelectedEquipmentChanged;
        protected virtual void OnSelectedEquipmentChanged(Equipment oldEquipment, Equipment newEquipment)
        {
            SelectedEquipmentChanged?.Invoke(oldEquipment, newEquipment);
        }
    }
}