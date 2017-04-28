using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IList<ProjectWrapper> _projects;

        public ProjectsViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projects = new List<ProjectWrapper>(unitOfWork.Projects.GetAll());
            Projects = new ObservableCollection<ProjectWrapper>(_projects);

            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute, NewProjectCommand_CanExecute);
        }

        public ObservableCollection<ProjectWrapper> Projects { get; }
        public ProjectWrapper SelectedProject { get; set; }

        public ICommand NewProjectCommand { get; }

        private void NewProjectCommand_Execute()
        {
            
        }

        private bool NewProjectCommand_CanExecute()
        {
            return true;
        }
    }
}
