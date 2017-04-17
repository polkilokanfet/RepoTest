using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Annotations;

namespace HVTApp.Services.ChooseProductService
{
    public class ChooseProductService : IChooseProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ProductParameterWrapper> _parameters;
        private readonly List<ParametersGroup> _parametersGroups = new List<ParametersGroup>(); 

        public ChooseProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _parameters = new List<ProductParameterWrapper>(unitOfWork.ProductParameters.GetAll().Select(ProductParameterWrapper.GetWrapper));

            var groups = _parameters.Select(x => x.Group).Distinct();
            foreach (var group in groups)
            {
                ParametersGroup parametersGroup = new ParametersGroup(group, _parameters.Where(x => Equals(x.Group, group)));
                _parametersGroups.Add(parametersGroup);
                parametersGroup.PropertyChanged += ParametersGroupOnPropertyChanged;
            }


        }

        private void ParametersGroupOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ParametersGroup.SelectedParameter)) return;
            RefreshParametersGroups();
        }

        public ObservableCollection<ParametersGroup> ParametersGroups { get; } = new ObservableCollection<ParametersGroup>();
        public List<ProductParameterWrapper> SelectedParameters => ParametersGroups.Select(x => x.SelectedParameter).ToList();

        private void RefreshParametersGroups()
        {
            var parametersCurrent = ParametersGroups.SelectMany(x => x.Parameters);
            var parametersCanBeSelected = _parameters.Where(x => x.CanBeSelected(SelectedParameters));

            var parametersToAdd = parametersCanBeSelected.Except(parametersCurrent);
            var parametersToRemove = _parameters.Except(parametersCanBeSelected).Intersect(parametersCurrent);

            foreach (var parameter in parametersToAdd)
                _parametersGroups.Single(x => Equals(x.Group, parameter.Group)).Parameters.Add(parameter);

            foreach (var parameter in parametersToRemove)
                _parametersGroups.Single(x => Equals(x.Group, parameter.Group)).Parameters.Remove(parameter);

            ParametersGroups.Clear();
            _parametersGroups.Where(x => x.Parameters.Any()).ToList().ForEach(ParametersGroups.Add);
        }

        public Product ChooseProduct(Product product = null)
        {
            throw new NotImplementedException();
        }
    }

    public class ParametersGroup : INotifyPropertyChanged
    {
        private ProductParameterWrapper _selectedParameter;

        public ParametersGroup(ProductParameterGroupWrapper group, IEnumerable<ProductParameterWrapper> parameters)
        {
            Group = group;

            Parameters = new ObservableCollection<ProductParameterWrapper>(parameters);
            if (Parameters.Any()) SelectedParameter = Parameters.First();

            this.Parameters.CollectionChanged += ParametersOnCollectionChanged;
        }

        private void ParametersOnCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
        {
            if (Parameters.Any())
            {
                if (SelectedParameter != null && Parameters.Contains(SelectedParameter))
                    return;
                SelectedParameter = Parameters.First();
            }
            else
            {
                SelectedParameter = null;
            }
        }

        public ProductParameterGroupWrapper Group { get; }

        public ObservableCollection<ProductParameterWrapper> Parameters { get; }

        public ProductParameterWrapper SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;
                _selectedParameter = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
