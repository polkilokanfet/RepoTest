using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyDetailsViewModel
    {
        private readonly ISelectService _selectService;
        private ActivityFieldWrapper _selectedActivityField;
        private IEnumerable<CompanyFormWrapper> _forms;

        public CompanyDetailsViewModel(IUnityContainer container, ISelectService selectService) : base(container)
        {
            _selectService = selectService;

            //Forms = WrapperDataService.CompanyFormWrapperDataService.GetAllAsync();

            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_ExecuteAsync);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_ExecuteAsync);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        public IEnumerable<CompanyFormWrapper> Forms
        {
            get { return _forms; }
            private set
            {
                _forms = value;
                OnPropertyChanged();
            }
        }

        public override async Task LoadAsync(Guid id)
        {
            await base.LoadAsync(id);
            Forms = (await UnitOfWork.GetRepository<CompanyForm>().GetAllAsNoTrackingAsync()).Select(x => new CompanyFormWrapper(x));
        }

        #region Commands

        public ICommand SelectParentCompanyCommand { get; }
        public ICommand RemoveParentCompanyCommand { get; }
        public ICommand AddActivityFieldCommand { get; }
        public ICommand RemoveActivityFieldCommand { get; }


        public ActivityFieldWrapper SelectedActivityField
        {
            get { return _selectedActivityField; }
            set
            {
                _selectedActivityField = value;
                ((DelegateCommand)RemoveActivityFieldCommand).RaiseCanExecuteChanged();
            }
        }

        private async void AddActivityFieldCommand_ExecuteAsync()
        {
            var exceptIds = Item.ActivityFilds.Select(x => x.Id);
            var fields = (await UnitOfWork.GetRepository<ActivityField>().GetAllAsync())
                                                                  .Where(x => !exceptIds.Contains(x.Id))
                                                                  .Select(x => new ActivityFieldWrapper(x));
            //var field = _selectService.SelectItem(fields);
            //if (field != null && !Item.ActivityFilds.Contains(field))
            //    Item.ActivityFilds.Add(field);
        }

        private void RemoveActivityFieldCommand_Execute()
        {
            Item.ActivityFilds.Remove(SelectedActivityField);
        }

        private bool RemoveActivityFieldCommand_CanExecute()
        {
            return SelectedActivityField != null;
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            //если головная компания не назначена
            if (Item.ParentCompany == null) return;
            //удалаем головную компанию текущей компании
            Item.ParentCompany = null;
        }

        private async void SelectParentCompanyCommand_ExecuteAsync()
        {
            var companies = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            //компании, которые не могут быть головной (дочерние и т.д.)
            var exceptCompanies = companies.Where(x => Equals(x.ParentCompany?.Id, Item.Id)).Concat(new[] {Item.Model});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = companies.Except(exceptCompanies).Select(x => new CompanyWrapper(x));
            //выбор одной из компаний
            //CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Item.ParentCompany.Id);

            //if (possibleParent != null && !Equals(possibleParent.Id, Item.ParentCompany?.Id))
            //{
            //    Item.ParentCompany = possibleParent;
            //}
        }

        #endregion
    }
}
