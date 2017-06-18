using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public class GetProductServiceRealization : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ParameterGroupWrapper> _parameterGroups;
        private readonly List<ProductWrapper> _products; 
        private readonly List<RequiredProductsChildsWrapper> _requiredProductsChilds; 

        public GetProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _parameterGroups = unitOfWork.ParametersGroups.GetAll();
            _products = unitOfWork.Products.GetAll();
            _requiredProductsChilds = unitOfWork.RequiredProductsChildses.GetAll();
        }

        private ProductItemWrapper GetProductItem(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            requiredParameters = requiredParameters == null ? new List<ParameterWrapper>() : new List<ParameterWrapper>(requiredParameters);

            var parametersUnions = new List<ParametersUnion>();
            foreach (var parameterGroup in _parameterGroups)
            {
                var intersect = parameterGroup.Parameters.Intersect(requiredParameters).ToList();
                parametersUnions.Add(intersect.Count == 1
                    ? new ParametersUnion(intersect)
                    : new ParametersUnion(parameterGroup.Parameters));
            }
            var viewModel = new SelectProductItemViewModel(parametersUnions, _unitOfWork);

            SelectProductItemWindow window = new SelectProductItemWindow { DataContext = viewModel };

            window.ShowDialog();

            return viewModel.ProductItem;
        }

        private IEnumerable<RequiredProductsChildsWrapper> GetRequiredProductsChilds(ProductItem productItem)
        {
            foreach (var requiredProductsChild in _requiredProductsChilds)
            {
                if (!requiredProductsChild.MainProductParameters.Select(x => x.Model).Except(productItem.Parameters).Any())
                    yield return requiredProductsChild;
            }
        }


        private ProductWrapper SelectProduct(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            Product product = new Product { ProductItem = GetProductItem(requiredParameters).Model };
            foreach (var requiredProductsChildsWrapper in GetRequiredProductsChilds(product.ProductItem))
            {
                var childProduct = SelectProduct(requiredProductsChildsWrapper.ChildProductParameters);
                for (int i = 0; i < requiredProductsChildsWrapper.Count; i++)
                {
                    product.ChildProducts.Add(childProduct.Model);
                }
            }

            var result = _products.FirstOrDefault(x => x.Model.Equals(product));
            return result ?? WrappersFactory.GetWrapper<Product, ProductWrapper>(product);
        }

        public ProductWrapper GetProduct(ProductWrapper originProduct = null)
        {
            return SelectProduct();
        }
    }

    public class SelectProductItemViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectProductItemViewModel(IEnumerable<ParametersUnion> parametersUnions, IUnitOfWork unitOfWork)
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

        public ProductItemWrapper ProductItem => _unitOfWork.ProductItems.GetProductItem(SelectedParameters);

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
