using System.Collections.Generic;
using System.Windows.Input;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Sales.Converter;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper>
    {
        public ProjectDetailsViewModel(ProjectWrapper item) : base(item)
        {
            AddProjectUnitsCommand = new DelegateCommand(AddProjectUnitsCommand_Execute);
        }

        private void AddProjectUnitsCommand_Execute()
        {
            throw new System.NotImplementedException();
        }

        public ProductUnitsGroup ProductGroup { get; set; }

        public ICommand AddProjectUnitsCommand { get; }
    }
}