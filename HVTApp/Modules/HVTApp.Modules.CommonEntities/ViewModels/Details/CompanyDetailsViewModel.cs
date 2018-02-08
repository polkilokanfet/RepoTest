using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyDetailsViewModel
    {
        private ActivityFieldWrapper _selectedActivityField;

        protected override void InitCommands()
        {
            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_ExecuteAsync);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_ExecuteAsync);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        public ICollection<CompanyFormWrapper> Forms { get; } = new ObservableCollection<CompanyFormWrapper>();

        protected override async Task LoadOtherAsync()
        {
            var forms = (await UnitOfWork.GetRepository<CompanyForm>().GetAllAsNoTrackingAsync()).Select(x => new CompanyFormWrapper(x));
            forms.ForEach(Forms.Add);
        }

        #region Commands

        public ICommand SelectParentCompanyCommand { get; private set; }
        public ICommand RemoveParentCompanyCommand { get; private set; }
        public ICommand AddActivityFieldCommand { get; private set; }
        public ICommand RemoveActivityFieldCommand { get; private set; }


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
            var fields = (await UnitOfWork.GetRepository<ActivityField>().GetAllAsync()).Where(x => !exceptIds.Contains(x.Id)).Select(x => new ActivityFieldLookup(x));
            var field = Container.Resolve<ISelectService>().SelectItem(fields);
            if (field != null && !Item.ActivityFilds.Any(x => Equals(x.Id, field.Id)))
                Item.ActivityFilds.Add(new ActivityFieldWrapper(field.Entity));
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
            var possibleParents = companies.Except(exceptCompanies);
            //выбор одной из компаний
            SelectAndSetWrapper<Company, CompanyLookup, CompanyWrapper>(possibleParents, nameof(Item.ParentCompany));
        }

        #endregion
    }
}
