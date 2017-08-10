using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceRealization : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ProductWrapper GetProduct(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            requiredParameters = requiredParameters == null 
                ? new List<ParameterWrapper>() 
                : new List<ParameterWrapper>(requiredParameters);

            var parametersUnions = new List<ParametersUnion>();
            foreach (var parameterGroup in _unitOfWork.ParametersGroups.GetAll())
            {
                var intersect = parameterGroup.Parameters.Intersect(requiredParameters).ToList();
                parametersUnions.Add(intersect.Count == 1
                    ? new ParametersUnion(intersect)
                    : new ParametersUnion(parameterGroup.Parameters));
            }
            var viewModel = new SelectProductViewModel(parametersUnions, _unitOfWork);

            SelectProductWindow window = new SelectProductWindow { DataContext = viewModel };

            window.ShowDialog();

            return viewModel.ProductItem;
        }

        private IEnumerable<RequiredDependentEquipmentsParametersWrapper> GetRequiredDependentEquipmentsParameters(Product product)
        {
            return _unitOfWork.RequiredDependentEquipmentsParameters.GetAll()
                .Where(p => !p.MainProductParameters.Select(x => x.Model).Except(product.Parameters).Any());
        }


        private EquipmentWrapper SelectEquipment(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            Equipment equipment = new Equipment { Product = GetProduct(requiredParameters).Model };
            foreach (var requiredChildProductParameters in GetRequiredDependentEquipmentsParameters(equipment.Product))
            {
                var childProduct = SelectEquipment(requiredChildProductParameters.ChildProductParameters);
                for (int i = 0; i < requiredChildProductParameters.Count; i++)
                {
                    equipment.DependentEquipments.Add(childProduct.Model);
                }
            }

            var result = _unitOfWork.Equipments.GetAll().FirstOrDefault(x => ProductsAreSame(x.Model, equipment));
            return result ?? _unitOfWork.Equipments.GetWrapper(equipment);
        }

        public EquipmentWrapper GetEquipment(EquipmentWrapper templateEquipment = null)
        {
            return SelectEquipment();
        }

        private bool ProductsAreSame(Equipment firstEquipment, Equipment secondEquipment)
        {
            return new ProductsComparer().Equals(firstEquipment, secondEquipment);
        }
    }

    public class SelectProductViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectProductViewModel(IEnumerable<ParametersUnion> parametersUnions, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ParametersUnions = parametersUnions.ToList();

            foreach (var parametersUnion in ParametersUnions)
            {
                parametersUnion.SelectedParameterChanged += ParametersUnionOnSelectedParameterChanged;
                parametersUnion.RefreshParametersToSelect(SelectedParameters);
            }
        }


        public IEnumerable<ParametersUnion> ParametersUnions { get; }

        public ProductWrapper ProductItem => _unitOfWork.Products.GetProductItem(SelectedParameters);

        private void ParametersUnionOnSelectedParameterChanged(object sender, EventArgs eventArgs)
        {
            foreach (var uop in ParametersUnions)
                uop.RefreshParametersToSelect(SelectedParameters);

            OnPropertyChanged(nameof(ProductItem));
        }

        public List<ParameterWrapper> SelectedParameters => ParametersUnions.Where(x => x.IsActual).Select(x => x.SelectedParameter).ToList();



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }




    public class ParametersUnion : INotifyPropertyChanged
    {
        public string GroupName { get; }
        private readonly IEnumerable<ParameterWrapper> _parameters;

        public ParametersUnion(IEnumerable<ParameterWrapper> parameters)
        {
            _parameters = parameters;
            GroupName = _parameters.First().Group.Name;

            ParametersToSelect = new ObservableCollection<ParameterWrapper>();
        }

        public ObservableCollection<ParameterWrapper> ParametersToSelect { get; }
        public bool IsActual => ParametersToSelect.Any();

        private ParameterWrapper _selectedParameter;
        public ParameterWrapper SelectedParameter
        {
            get
            {
                if (!IsActual) return null;
                return _selectedParameter;
            }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                _selectedParameter = value;

                SelectedParameterChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged(nameof(SelectedParameter));
                OnPropertyChanged(nameof(IsActual));
            }
        }

        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> contextParameters)
        {
            var actual = _parameters.Where(x => x.CanBeSelected(contextParameters)).OrderBy(x => x.Value).ToList();

            if (!actual.Except(ParametersToSelect).Any() &&
                !ParametersToSelect.Except(actual).Any()) return;

            ParametersToSelect.Clear();
            foreach (var parameter in actual)
            {
                if (!ParametersToSelect.Contains(parameter)) ParametersToSelect.Add(parameter);
            }

            //выбор
            if (IsActual && !ParametersToSelect.Contains(_selectedParameter))
                SelectedParameter = ParametersToSelect.First();
        }

        public event EventHandler SelectedParameterChanged;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
