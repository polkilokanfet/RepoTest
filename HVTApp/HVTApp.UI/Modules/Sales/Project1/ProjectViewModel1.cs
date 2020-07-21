using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectViewModel1 : ViewModelBase
    {
        //������
        public ProjectDetailsViewModel DetailsViewModel { get; }

        //������ ������
        public ProjectUnitGroupsViewModel GroupsViewModel { get; }

        public ICommand SaveCommand { get; }

        public ProjectViewModel1(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<ProjectDetailsViewModel>();
            GroupsViewModel = container.Resolve<ProjectUnitGroupsViewModel>();
            SaveCommand = new DelegateCommand(
                () =>
                {
                    //������� �� ������� ��������� ����� � �������������
                    this.GroupsViewModel.GroupChanged -= OnGroupChanged;

                    GroupsViewModel.AcceptChanges();

                    //��������� ��������, ���� �� �� ������������
                    if (UnitOfWork.Repository<Project>().GetById(DetailsViewModel.Item.Model.Id) == null)
                        UnitOfWork.Repository<Project>().Add(DetailsViewModel.Item.Model);

                    DetailsViewModel.Item.AcceptChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProjectEvent>().Publish(DetailsViewModel.Item.Model);

                    //���������
                    UnitOfWork.SaveChanges();

                    //����������� �� ������� ��������� ����� � �������������
                    this.GroupsViewModel.GroupChanged += OnGroupChanged;

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () =>
                {
                    //��� �������� ������ ���� �������
                    if (!GroupsViewModel.IsValid || !DetailsViewModel.Item.IsValid)
                        return false;

                    //�����-�� �������� ������ ���� ��������
                    return DetailsViewModel.Item.IsChanged || GroupsViewModel.IsChanged;
                });
        }

        public void Load(Project project, bool isNew, object parameter = null)
        {
            //������
            DetailsViewModel.Load(project, UnitOfWork);
            DetailsViewModel.Item.PropertyChanged += (sender, args) => { ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); };

            //������ ������
            var units = UnitOfWork.Repository<SalesUnit>().GetAll().Where(x => x.Project.Id == project.Id);
            GroupsViewModel.Load(units, DetailsViewModel.Item, UnitOfWork, isNew);
            GroupsViewModel.GroupChanged += OnGroupChanged;

            //��������� ���������
            if (DetailsViewModel.Item.Manager == null)
            {
                var user = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Manager = new UserWrapper(user);
            }

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnGroupChanged()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //���� ������ ������ ��� ������������� ����������
            if (SaveCommand.CanExecute(null))
            {
                var ms = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ��������� ���������?", defaultNo: true);
                if (ms == MessageDialogResult.Yes)
                    SaveCommand.Execute(null);
            }

            base.GoBackCommand_Execute();
        }

    }
}