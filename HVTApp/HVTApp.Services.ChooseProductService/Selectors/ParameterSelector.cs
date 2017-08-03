using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<Parameter> _selectedParameters; 
        public ParameterSelector(Parameter parameter, ObservableCollection<Parameter> selectedParameters)
        {
            Parameter = parameter;
            _selectedParameters = selectedParameters;
            selectedParameters.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(CanBeSelected));
        }

        public Parameter Parameter { get; }

        public bool CanBeSelected => Parameter.RequiredPreviousParameters.Any(pp => pp.RequiredParameters.All(x => _selectedParameters.Contains(x)));

    }
}