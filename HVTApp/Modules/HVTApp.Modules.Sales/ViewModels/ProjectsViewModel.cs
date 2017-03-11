using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BindableBase
    {
        public ICommand NewProjectCommand { get; }
        public ProjectsViewModel()
        {
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute, NewProjectCommand_CanExecute);
        }

        private void NewProjectCommand_Execute()
        {
            
        }

        private bool NewProjectCommand_CanExecute()
        {
            return true;
        }
    }
}
