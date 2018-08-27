using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<IUnitGroup> ProjectUnits { get; } = new ObservableCollection<IUnitGroup>();

        public IUnitGroup SelectedUnitGroup
        {
            get { return _selectedUnitGroup; }
            set
            {
                _selectedUnitGroup = value;
                InvalidateCommands();
            }
        }

        public ICommand GroupingCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            GroupingCommand = new DelegateCommand(GroupingCommand_Execute);
            EditCommand = new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);
        }

        //protected override async Task LoadOtherAsync()
        //{
        //    ProjectUnits.Clear();
        //    var salesUnits = await WrapperDataService.GetRepository<SalesUnit>().FindAsync(x => Equals(x.Project, Item.Model));
        //    ProjectUnits.AddRange(salesUnits.Select(x => new SalesUnitWrapper(x)));
        //    RefreshGroups();
        //}

        private IUnitGroup _selectedUnitGroup;
        private bool _isGrouping = true;

        private void GroupingCommand_Execute()
        {
            _isGrouping = !_isGrouping;
            RefreshGroups();
        }

        private void RefreshGroups()
        {
            ProjectUnits.Clear();

            //if (_isGrouping)
            //    ProjectUnits.AddRange(Item.SalesUnits.ToUnitGroups());
            //else
            //    ProjectUnits.AddRange(Item.SalesUnits);

            OnPropertyChanged(nameof(SelectedUnitGroup));
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
        }

        private void EditCommand_Execute()
        {
            var projectUnitGroup = (UnitGroup)SelectedUnitGroup;
            var projectUnitGroupViewModel = new UnitGroupViewModel(projectUnitGroup, Container, WrapperDataService);
            Container.Resolve<IDialogService>().ShowDialog(projectUnitGroupViewModel);
        }

        private bool EditCommand_CanExecute()
        {
            //return SelectedUnitGroup != null;
            return true;
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override async void SaveCommand_Execute()
        {
            if (await WrapperDataService.GetRepository<Project>().GetByIdAsync(Item.Model.Id) == null)
                WrapperDataService.GetRepository<Project>().Add(Item.Model);
            Item.AcceptChanges();

            await WrapperDataService.SaveChangesAsync();

            //формируем задания на расчет
            foreach (var product in ProjectUnits.Select(x => x.Product))
            {
                await Container.Resolve<IGenerateCalculatePriceTasksService>().GenerateCalculatePriceTasks(product, DateTime.Today, Item.Id);
            }

            EventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(Item.Model);

            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }
    }
}