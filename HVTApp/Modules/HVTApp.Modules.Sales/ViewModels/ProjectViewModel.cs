using System.Linq;
using System.Threading.Tasks;
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
        public SalesUnitsGroupsViewModel GroupsViewModel { get; private set; }

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override async Task AfterLoading()
        {
            await base.AfterLoading();

            //��������� ���������
            if (Item.Manager == null)
            {
                var model = await UnitOfWork.Repository<User>().GetByIdAsync(CommonOptions.User.Id);
                Item.Manager = new UserWrapper(model);
            }

            //��������� ������ � �������������
            var units = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == Item.Id);
            GroupsViewModel = new SalesUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);
            await GroupsViewModel.LoadAsync();

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //������ �� ��������� ������
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        protected override async void SaveCommand_Execute()
        {
            await GroupsViewModel.SaveChanges();
            base.SaveCommand_Execute();
        }

        protected override bool SaveCommand_CanExecute()
        {
            //��� �������� ������ ���� �������
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid || 
                GroupsViewModel.Groups == null || !GroupsViewModel.Groups.Any())
                return false;

            //�����-�� �������� ������ ���� ��������
            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }
    }
}