using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class EquipmentToSelect
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Product> _products;
        private readonly IList<Equipment> _equipments;
        private readonly IEnumerable<RequiredDependentEquipmentsParameters> _requiredDependentEquipmentsParametersList;
        private Equipment _parentEquipment;
        private Equipment _selectedEquipment;

        public EquipmentToSelect(
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
            Product = new ProductToSelect(_groups.Select(x => x.Parameters), null);
            Product.SelectedProductChanged += OnSelectedProductChanged;
        }

        private void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            RefreshDependentEquipments();
            SelectedEquipment = GetEquipment();
        }

        private Equipment GetEquipment()
        {
            var dependentEquipment = DependentEquipments.Select(x => SelectedEquipment);
            var result = _equipments.SingleOrDefault(x => Equals(x.Product, Product.SelectedProduct) && 
                                                     x.DependentEquipments.All(e => dependentEquipment.Contains(e)));
            if (result == null)
            {
                result = new Equipment { Product = Product.SelectedProduct, DependentEquipments = new List<Equipment>(dependentEquipment) };
                _equipments.Add(result);
            }
            return result;
        }

        public ProductToSelect Product { get; set; }
        public ObservableCollection<EquipmentToSelect> DependentEquipments { get; set; } = new ObservableCollection<EquipmentToSelect>();

        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                if (Equals(_parentEquipment, value)) return;
                var oldValue = _selectedEquipment;
                _selectedEquipment = value;
                OnSelectedEquipmentChanged(oldValue, value);
            }
        }

        private void RefreshDependentEquipments()
        {
            IEnumerable<IEnumerable<Parameter>> dependentEquipmentsParametersList = 
                _requiredDependentEquipmentsParametersList.
                Where(x => x.MainProductParameters.All(p => Product.SelectedProduct.Parameters.Contains(p))).
                Select(x => x.ChildProductParameters);

            DependentEquipments.Clear();
            foreach (var dependentEquipmentsParameters in dependentEquipmentsParametersList)
            {
                DependentEquipments.Add(new EquipmentToSelect(_groups, _requiredDependentEquipmentsParametersList, this.SelectedEquipment, null));
            }
        }


        public event Action<Equipment, Equipment> SelectedEquipmentChanged;

        protected virtual void OnSelectedEquipmentChanged(Equipment oldEq, Equipment newEq)
        {
            SelectedEquipmentChanged?.Invoke(oldEq, newEq);
        }
    }

    public class ProductToSelect
    {
        private Product _selectedProduct;

        public ProductToSelect(IEnumerable<IEnumerable<Parameter>> parametersGroups, Func<IEnumerable<Parameter>, Product> getProduct)
        {
            ParametersToSelect = new ObservableCollection<ParametersToSelect>(parametersGroups.Select(x => new ParametersToSelect(x)));

            foreach (var parameters in ParametersToSelect)
            {
                parameters.SelectedParameterChanged += (oldParameter, newParameter) =>
                {
                    SelectedProduct = getProduct(SelectedParameters);
                };
            }
        }

        public ObservableCollection<ParametersToSelect> ParametersToSelect { get; set; }

        private IEnumerable<Parameter> SelectedParameters => ParametersToSelect.Select(x => x.SelectedParameter); 

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (Equals(_selectedProduct, value)) return;
                var oldValue = _selectedProduct;
                _selectedProduct = value;
                OnSelectedProductChanged(oldValue, value);
            }
        }


        public event Action<Product, Product> SelectedProductChanged;

        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }
    }


    public class ParametersToSelect
    {
        private Parameter _selectedParameter;
        private IList<Parameter> _parameters; 

        public ParametersToSelect(IEnumerable<Parameter> parameters, Parameter selectedParameter = null)
        {
            _parameters = new List<Parameter>(parameters);
            SelectedParameter = selectedParameter ?? _parameters.First();
        }

        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();

        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;
                var oldValue = _selectedParameter;
                _selectedParameter = value;
                OnSelectedParameterChanged(oldValue, value);
            }
        }



        public event Action<Parameter, Parameter> SelectedParameterChanged; 

        protected virtual void OnSelectedParameterChanged(Parameter oldParam, Parameter newParam)
        {
            SelectedParameterChanged?.Invoke(oldParam, newParam);
        }
    }
}