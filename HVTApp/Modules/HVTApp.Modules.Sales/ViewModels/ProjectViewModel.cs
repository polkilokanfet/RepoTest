using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel : ProjectDetailsViewModel
    {
        public GroupsViewModel GroupsViewModel { get; private set; }

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ChangeFacilityCommand { get; private set; }
        public ICommand ChangeProductCommand { get; private set; }
        public ICommand ChangePaymentsCommand { get; private set; }

        public ICommand AddProductIncludedCommand { get; private set; }
        public ICommand RemoveProductIncludedCommand { get; private set; }

        #endregion


        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override async Task AfterLoading()
        {
            await base.AfterLoading();

            //назначаем менеджера
            if (Item.Manager == null)
            {
                var model = await UnitOfWork.Repository<User>().GetByIdAsync(CommonOptions.User.Id);
                Item.Manager = new UserWrapper(model);
            }

            //загружаем строки с оборудованием
            var units = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == Item.Id);
            GroupsViewModel = new GroupsViewModel(Container, units, UnitOfWork);
            await GroupsViewModel.LoadAsync();

            //команды
            AddCommand = GroupsViewModel.AddCommand;
            RemoveCommand = GroupsViewModel.RemoveCommand;
            ChangeFacilityCommand = GroupsViewModel.ChangeFacilityCommand;
            ChangeProductCommand = GroupsViewModel.ChangeProductCommand;
            ChangePaymentsCommand = GroupsViewModel.ChangePaymentsCommand;
            AddProductIncludedCommand = GroupsViewModel.AddProductIncludedCommand;
            RemoveProductIncludedCommand = GroupsViewModel.RemoveProductIncludedCommand;

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //сигнал об изменении модели
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        protected override async void SaveCommand_Execute()
        {
            base.SaveCommand_Execute();
            await GroupsViewModel.SaveChanges();
        }

        protected override bool SaveCommand_CanExecute()
        {
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid)
                return false;

            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }

        //protected override DateTime GetDate()
        //{
        //    var oitDate = Item.Units.Min(x => x.OrderInTakeDate);
        //    return oitDate < DateTime.Today ? oitDate : DateTime.Today;
        //}
    }
}