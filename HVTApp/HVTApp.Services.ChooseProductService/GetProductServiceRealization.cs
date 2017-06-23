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

namespace HVTApp.Services.ChooseProductService
{
    public class GetProductServiceRealization : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ProductItemWrapper GetProductItem(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            requiredParameters = requiredParameters == null ? new List<ParameterWrapper>() : new List<ParameterWrapper>(requiredParameters);

            var parametersUnions = new List<ParametersUnion>();
            foreach (var parameterGroup in _unitOfWork.ParametersGroups.GetAll())
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

        private IEnumerable<RequiredChildProductParametersWrapper> GetRequiredChildProductParameters(ProductItem productItem)
        {
            return _unitOfWork.RequiredChildProductParameters.GetAll()
                .Where(p => !p.MainProductParameters.Select(x => x.Model).Except(productItem.Parameters).Any());
        }


        private ProductWrapper SelectProduct(IEnumerable<ParameterWrapper> requiredParameters = null)
        {
            Product product = new Product { ProductItem = GetProductItem(requiredParameters).Model };
            foreach (var requiredChildProductParameters in GetRequiredChildProductParameters(product.ProductItem))
            {
                var childProduct = SelectProduct(requiredChildProductParameters.ChildProductParameters);
                for (int i = 0; i < requiredChildProductParameters.Count; i++)
                {
                    product.ChildProducts.Add(childProduct.Model);
                }
            }

            var result = _unitOfWork.Products.GetAll().FirstOrDefault(x => ProductsAreSame(x.Model, product));
            return result ?? WrappersFactory.GetWrapper<ProductWrapper>(product);
        }

        public ProductWrapper GetProduct(ProductWrapper originProduct = null)
        {
            return SelectProduct();
        }

        private bool ProductsAreSame(Product firstProduct, Product secondProduct)
        {
            return new ProductsComparer().Equals(firstProduct, secondProduct);
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
