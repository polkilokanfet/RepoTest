using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
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
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_ExecuteAsync);
            RemoveActivityFieldCommand = new DelegateCommand(RemoveActivityFieldCommand_Execute, RemoveActivityFieldCommand_CanExecute);
        }

        public ICollection<CompanyFormWrapper> Forms { get; } = new ObservableCollection<CompanyFormWrapper>();

        protected override async Task LoadOtherAsync()
        {
            var forms = (await UnitOfWork.GetRepository<CompanyForm>().GetAllAsNoTrackingAsync()).Select(x => new CompanyFormWrapper(x));
            forms.ForEach(Forms.Add);
        }

        protected override void InitGetMethods()
        {
            _getEntitiesForSelectParentCompanyCommand = async () =>
            {
                var companies = await UnitOfWork.GetRepository<Company>().GetAllAsync();
                //компании, которые не могут быть головной (дочерние и т.д.)
                var exceptCompanies = companies.Where(x => Equals(x.ParentCompany?.Id, Item.Id)).Concat(new[] {Item.Model});
                //возможные головные компании
                return companies.Except(exceptCompanies).ToList();
            };
        }

        #region Commands

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
            var fields = (await UnitOfWork.GetRepository<ActivityField>().GetAllAsync()).Where(x => !exceptIds.Contains(x.Id));
            var field = Container.Resolve<ISelectService>().SelectItem(fields);
            if (field != null && !Item.ActivityFilds.Any(x => Equals(x.Id, field.Id)))
                Item.ActivityFilds.Add(new ActivityFieldWrapper(field));
        }

        private void RemoveActivityFieldCommand_Execute()
        {
            Item.ActivityFilds.Remove(SelectedActivityField);
        }

        private bool RemoveActivityFieldCommand_CanExecute()
        {
            return SelectedActivityField != null;
        }
        #endregion
    }
}
