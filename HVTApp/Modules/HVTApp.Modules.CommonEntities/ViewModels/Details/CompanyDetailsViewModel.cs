using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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

        public CompanyDetailsViewModel(IUnityContainer container, ISelectService selectService, CompanyWrapper wrapper = null) : base(container, wrapper)
        {
            _selectService = selectService;
            Load();

            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        public async void Load(CompanyWrapper wrapper = null)
        {
            Forms.Clear();
            var forms = await WrapperDataService.CompanyFormWrapperDataService.GetAllAsync();
            Forms.AddRange(forms);
        }

        public ObservableCollection<CompanyFormWrapper> Forms { get; } = new ObservableCollection<CompanyFormWrapper>();


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

        private async void AddActivityFieldCommand_Execute()
        {
            var fields = (await UnitOfWork.ActivityFieldRepository.GetAllAsync()).Select(x => new ActivityFieldWrapper(x)).Except(Item.ActivityFilds);
            var field = _selectService.SelectItem(fields);
            if (field != null && !Item.ActivityFilds.Contains(field))
                Item.ActivityFilds.Add(field);
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

        private async void SelectParentCompanyCommand_Execute()
        {
            var companies = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            //компании, которые не могут быть головной (дочерние и т.д.)
            var exceptCompanies = companies.Where(x => Equals(x.ParentCompany?.Id, Item.Id)).Concat(new[] {Item.Model});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = companies.Except(exceptCompanies).Select(x => new CompanyWrapper(x));
            //выбор одной из компаний
            CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Item.ParentCompany);

            if (possibleParent != null && !Equals(possibleParent.Id, Item.ParentCompany?.Id))
            {
                Item.ParentCompany = possibleParent;
            }
        }

        #endregion
    }
}
