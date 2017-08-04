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
            ProductSelector.PropertyChanged += ProductOnPropertyChanged;

            //дочернее оборудование
            RefreshDependentEquipments();
            DependetEquipmentSelectors.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(SelectedEquipment));
        }

        private void ProductOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RefreshDependentEquipments();
            OnPropertyChanged(nameof(SelectedEquipment));
        }

        public IEnumerable<Parameter> RequiredProductsParameters { get; set; }
        public ProductSelector ProductSelector { get; set; }
        public ObservableCollection<EquipmentSelector> DependetEquipmentSelectors { get; set; } = new ObservableCollection<EquipmentSelector>();

        public Equipment SelectedEquipment
        {
            get
            {
                var dependentEquipment = DependetEquipmentSelectors.Select(x => SelectedEquipment);
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
        }

        private void RefreshDependentEquipments()
        {
            IEnumerable<IEnumerable<Parameter>> dependentEquipmentsParametersList = 
                _requiredDependentEquipmentsParametersList.
                    Where(x => x.MainProductParameters.All(p => ProductSelector.SelectedParameters.Contains(p))).
                    Select(x => x.ChildProductParameters);

            //исключаем не актуальное дочернее оборудование
            foreach (var equipmentSelector in DependetEquipmentSelectors.Where(des => !dependentEquipmentsParametersList.Any(x => x.AllMembersAreSame(des.RequiredProductsParameters))))
            {
                DependetEquipmentSelectors.Remove(equipmentSelector);
                equipmentSelector.PropertyChanged -= EquipmentSelectorOnPropertyChanged;
            }

            //добавляем актуальное дочернее оборудование
            foreach (var dependentEquipmentsParameters in 
                dependentEquipmentsParametersList.Where(x => !DependetEquipmentSelectors.Any(des => des.RequiredProductsParameters.AllMembersAreSame(x))))
            {
                var equipmentSelector = new EquipmentSelector(_groups, _products, _equipments, _requiredDependentEquipmentsParametersList, dependentEquipmentsParameters);
                DependetEquipmentSelectors.Add(equipmentSelector);
                equipmentSelector.PropertyChanged += EquipmentSelectorOnPropertyChanged;
            }
        }

        private void EquipmentSelectorOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(SelectedEquipment));
        }
    }
}