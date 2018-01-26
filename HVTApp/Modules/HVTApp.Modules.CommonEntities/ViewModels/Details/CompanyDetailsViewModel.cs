using System.Collections.Generic;
using System.Linq;
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

        public CompanyDetailsViewModel(IUnityContainer container, ISelectService selectService, CompanyWrapper wrapper = null) : base(container, wrapper)
        {
            _selectService = selectService;

            Forms = WrapperDataService.CompanyFormWrapperDataService.GetAll();

            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        public IEnumerable<CompanyFormWrapper> Forms { get; }


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

        private void AddActivityFieldCommand_Execute()
        {
            var exceptIds = Item.ActivityFilds.Select(x => x.Id);
            var fields = UnitOfWork.GetRepository<ActivityField>().GetAll()
                                                                  .Where(x => !exceptIds.Contains(x.Id))
                                                                  .Select(x => new ActivityFieldWrapper(x));
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
            var companies = UnitOfWork.GetRepository<Company>().GetAll();
            //компании, которые не могут быть головной (дочерние и т.д.)
            var exceptCompanies = companies.Where(x => Equals(x.ParentCompany?.Id, Item.Id)).Concat(new[] {Item.Model});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = companies.Except(exceptCompanies).Select(x => new CompanyWrapper(x));
            //выбор одной из компаний
            CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Item.ParentCompany.Id);

            if (possibleParent != null && !Equals(possibleParent.Id, Item.ParentCompany?.Id))
            {
                Item.ParentCompany = possibleParent;
            }
        }

        #endregion
    }
}
