using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public class ChooseProductServiceRealization : IChooseProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ParameterGroupWrapper> _parameterGroups;
        private readonly List<RequiredProductsChildsWrapper> _requiredProductsChilds; 

        public ChooseProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _parameterGroups = unitOfWork.ParametersGroups.GetAll();
            _requiredProductsChilds = unitOfWork.RequiredProductsChildses.GetAll();
        }

        private List<ParameterWrapper> ChooseParameters(IEnumerable<ParameterWrapper> requiredParameters = null)
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
            var viewModel = new SelectParametersViewModel(parametersUnions, _unitOfWork);

            SelectParametersWindow window = new SelectParametersWindow { DataContext = viewModel };

            window.ShowDialog();

            return viewModel.SelectedParameters.ToList();
        }

        private IEnumerable<ParameterWrapper> GetNextChildsProductParameters(List<List<ParameterWrapper>> parameters)
        {
            foreach (var requiredProductsChild in _requiredProductsChilds)
            {
                if (parameters.Any(p => !requiredProductsChild.MainProductParameters.Except(p).Any())) //если выбран родитель
                    if (parameters.All(x => requiredProductsChild.ChildProductParameters.Except(x).Any())) //если еще не выбран дочерний продукт
                        return requiredProductsChild.ChildProductParameters;
            }
            return new List<ParameterWrapper>();
        } 

        public Product ChooseProduct(ProductWrapper originProduct = null)
        {
            var nextChildsProductParameters = new List<ParameterWrapper>();
            var cp = new List<List<ParameterWrapper>>();
            do
            {
                cp.Add(ChooseParameters(nextChildsProductParameters));
                nextChildsProductParameters = GetNextChildsProductParameters(cp).ToList();
            } while (nextChildsProductParameters.Any());

            return null;
        }
    }

    public class SelectParametersViewModel : INotifyPropertyChanged
    {
        public SelectParametersViewModel(IEnumerable<ParametersUnion> parametersUnions, IUnitOfWork unitOfWork)
        {
            _existsProductItems = unitOfWork.ProductItems.GetAll();

            ParametersUnions = parametersUnions.ToList();

            foreach (var parametersUnion in ParametersUnions)
            {
                parametersUnion.SelectedParameterChanged += ParametersUnionOnSelectedParameterChanged;
                parametersUnion.RefreshParametersToSelect(SelectedParameters);
            }
        }

        private readonly List<ProductItemWrapper> _existsProductItems; 

        public IEnumerable<ParametersUnion> ParametersUnions { get; }

        public ProductItemWrapper ProductItem
        {
            get
            {
                foreach (var productItem in _existsProductItems)
                    if (!productItem.Parameters.Except(SelectedParameters).Any())
                        return productItem;

                var productItemNew = new ProductItem()
                {
                    Designation = "new product",
                    Parameters = new List<Parameter>(SelectedParameters.Select(x => x.Model))
                };
                return new ProductItemWrapper(productItemNew);
            }
        }

        private void ParametersUnionOnSelectedParameterChanged(object sender, EventArgs eventArgs)
        {
            foreach (var uop in ParametersUnions)
                uop.RefreshParametersToSelect(SelectedParameters);

            OnPropertyChanged(nameof(Product));
        }

        public List<ParameterWrapper> SelectedParameters => 
            ParametersUnions.Where(x => x.IsActual).Select(x => x.SelectedParameter).ToList();



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
