using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.Modules.Products.Parameters
{
    public class ParameterLookups : ObservableCollection<ParameterLookup>
    {
        private readonly ParametersViewModel _viewModel;

        public ParameterLookups(IEnumerable<Parameter> parameters, ParametersViewModel viewModel) : 
            base(parameters.Select(parameter => new ParameterLookup(parameter)))
        {
            _viewModel = viewModel;
        }

        public ParameterLookup Refresh(Parameter parameter)
        {
            var lookup = this.SingleOrDefault(parameterLookup => parameterLookup.Entity.Id == parameter.Id);
            if (lookup != null)
            {
                lookup.Refresh(parameter);
            }
            else
            {
                lookup = new ParameterLookup(parameter);
                this.Add(lookup);
                _viewModel.SelectedParameterLookup = lookup;
            }

            return lookup;
        }
    }
}