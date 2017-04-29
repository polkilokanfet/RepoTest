using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public class ChooseProductService : IChooseProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<UnionOfParameters> UnionsOfParameters { get; }
        public IEnumerable<ParameterWrapper> SelectedParameters => UnionsOfParameters.Select(x => x.SelectedParameter);

        public ChooseProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var parameters = unitOfWork.ProductParameters.GetAll().OrderBy(x => x.Rank);

            UnionsOfParameters = new ObservableCollection<UnionOfParameters>();
            var groups = parameters.Select(x => x.Group).Distinct();
            foreach (var group in groups)
            {
                var unionOfParameters = new UnionOfParameters(group, parameters.Where(x => Equals(x.Group, group)));
                UnionsOfParameters.Add(unionOfParameters);
                unionOfParameters.UnionChanged += OnUnionOfParametersChanged;
            }
        }

        private void OnUnionOfParametersChanged(object sender, EventArgs eventArgs)
        {
            foreach (var unionOfParameters in UnionsOfParameters)
            {
                unionOfParameters.RefreshParametersToSelect(SelectedParameters);
            }
        }


        public Product ChooseProduct(Product product = null)
        {
            UnionsOfParameters.First().SelectedParameter = UnionsOfParameters.First().Parameters.First();
            SelectParametersWindow window = new SelectParametersWindow();
            window.DataContext = this;
            window.ShowDialog();
            return null;
        }
    }






    public class UnionOfParameters : INotifyPropertyChanged
    {
        public ParameterGroupWrapper Group { get; }

        public List<ParameterWrapper> Parameters { get; }

        public ObservableCollection<ParameterWrapper> ParametersToSelect { get; } = new ObservableCollection<ParameterWrapper>();

        private ParameterWrapper _selectedParameter;
        public ParameterWrapper SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                _selectedParameter = value;
                OnPropertyChanged();
                UnionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsActual => ParametersToSelect.Any();

        public UnionOfParameters(ParameterGroupWrapper group, IEnumerable<ParameterWrapper> parameters)
        {
            Group = group;
            Parameters = new List<ParameterWrapper>(parameters);
        }

        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> selectedParameters)
        {
            var paramsToSelect = Parameters.Where(x => x.CanBeSelected(selectedParameters)).ToList();

            var paramsToAdd = paramsToSelect.Except(ParametersToSelect).ToList();
            var paramsToRemove = ParametersToSelect.Except(paramsToSelect).ToList();

            if (paramsToAdd.Any() || paramsToRemove.Any())
            {
                foreach (var param in paramsToAdd)
                    ParametersToSelect.Add(param);
                foreach (var param in paramsToRemove)
                    ParametersToSelect.Remove(param);

                if (ParametersToSelect.Any())
                    if (SelectedParameter == null || !ParametersToSelect.Contains(SelectedParameter))
                        SelectedParameter = ParametersToSelect.First();

                UnionChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged(nameof(IsActual));
            }
        }


        public event EventHandler UnionChanged; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
