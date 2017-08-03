using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class EquipmentSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Product> _products;
        private readonly IList<Equipment> _equipments;
        private readonly IEnumerable<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;

        private Equipment _parentEquipment;

        public EquipmentSelector(
            IEnumerable<ParameterGroup> groups, 
            IList<Product> products, 
            IList<Equipment> equipments, 
            IEnumerable<RequiredDependentEquipmentsParameters> requiredDependentEquipmentsParametersList, 
            Equipment parentEquipment)
        {
            _groups = groups;
            _products = products;
            _equipments = equipments;
            _requiredDependentEquipmentsParametersList = requiredDependentEquipmentsParametersList;
            _parentEquipment = parentEquipment;

            //продукт
            ProductSelector = new ProductSelector(_groups.Select(x => x.Parameters), null);
            ProductSelector.PropertyChanged += ProductOnPropertyChanged;
        }

        private void ProductOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RefreshDependentEquipments();
            OnPropertyChanged(nameof(SelectedEquipment));
        }

        public ProductSelector ProductSelector { get; set; }
        public ObservableCollection<EquipmentSelector> DependetEquipmentSelectors { get; set; } = new ObservableCollection<EquipmentSelector>();

        public Equipment SelectedEquipment
        {
            get
            {
                var dependentEquipment = DependetEquipmentSelectors.Select(x => SelectedEquipment);
                var result = _equipments.SingleOrDefault(x => Equals(x.Product, ProductSelector.SelectedProduct) &&
                                                              x.DependentEquipments.All(e => dependentEquipment.Contains(e)));
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
                    Where(x => x.MainProductParameters.All(p => ProductSelector.SelectedProduct.Parameters.Contains(p))).
                    Select(x => x.ChildProductParameters);

            DependetEquipmentSelectors.Clear();
            foreach (var dependentEquipmentsParameters in dependentEquipmentsParametersList)
            {
                DependetEquipmentSelectors.Add(new EquipmentSelector(_groups, _products, _equipments, _requiredDependentEquipmentsParametersList, this.SelectedEquipment));
            }
        }
    }
}