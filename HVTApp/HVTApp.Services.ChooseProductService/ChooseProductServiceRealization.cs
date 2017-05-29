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

        public ChooseProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product ChooseProduct(ProductWrapper originProduct = null)
        {
            SelectParametersWindow window = new SelectParametersWindow();
            var uops = _unitOfWork.ParametersGroups.GetAll().Select(x => new ParametersUnion(x.Parameters));
            window.DataContext = new SelectParametersViewModel(uops, _unitOfWork);

            window.ShowDialog();
            return null;
        }
    }




    public class SelectParametersViewModel : INotifyPropertyChanged
    {
        public SelectParametersViewModel(IEnumerable<ParametersUnion> parametersUnions, IUnitOfWork unitOfWork)
        {
            _existsProducts = unitOfWork.Products.GetAll();

            ParametersUnions = parametersUnions.ToList();

            foreach (var parametersUnion in ParametersUnions)
            {
                parametersUnion.SelectedParameterChanged += ParametersUnionOnSelectedParameterChanged;
                parametersUnion.RefreshParametersToSelect(SelectedParameters);
            }
        }

        private readonly List<ProductWrapper> _existsProducts; 

        public IEnumerable<ParametersUnion> ParametersUnions { get; }

        public ProductWrapper Product
        {
            get
            {
                foreach (var product in _existsProducts)
                    if (!product.Parameters.Except(SelectedParameters).Any())
                        return product;

                var productNew = new Product
                {
                    Designation = "new product",
                    Parameters = new List<Parameter>(SelectedParameters.Select(x => x.Model))
                };
                return new ProductWrapper(productNew);
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
