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
        //детали
        public ProjectDetailsViewModel DetailsViewModel { get; }

        //группы юнитов
        public ProjectUnitGroupsViewModel GroupsViewModel { get; }

        public ICommand SaveCommand { get; }

        public ProjectViewModel1(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<ProjectDetailsViewModel>();
            GroupsViewModel = container.Resolve<ProjectUnitGroupsViewModel>();
            SaveCommand = new DelegateCommand(
                () =>
                {
                    //отписка от событий изменения строк с оборудованием
                    this.GroupsViewModel.GroupChanged -= OnGroupChanged;

                    GroupsViewModel.AcceptChanges();

                    //добавляем сущность, если ее не существовало
                    if (UnitOfWork.Repository<Project>().GetById(DetailsViewModel.Item.Model.Id) == null)
                        UnitOfWork.Repository<Project>().Add(DetailsViewModel.Item.Model);

                    DetailsViewModel.Item.AcceptChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProjectEvent>().Publish(DetailsViewModel.Item.Model);

                    //сохраняем
                    UnitOfWork.SaveChanges();

                    //регистрация на события изменения строк с оборудованием
                    this.GroupsViewModel.GroupChanged += OnGroupChanged;

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () =>
                {
                    //все сущности должны быть валидны
                    if (!GroupsViewModel.IsValid || !DetailsViewModel.Item.IsValid)
                        return false;

                    //какая-то сущность должна быть изменена
                    return DetailsViewModel.Item.IsChanged || GroupsViewModel.IsChanged;
                });
        }

        public void Load(Project project, bool isNew, object parameter = null)
        {
            //детали
            DetailsViewModel.Load(project, UnitOfWork);
            DetailsViewModel.Item.PropertyChanged += (sender, args) => { ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); };

            //группы юнитов
            var units = UnitOfWork.Repository<SalesUnit>().GetAll().Where(x => x.Project.Id == project.Id);
            GroupsViewModel.Load(units, DetailsViewModel.Item, UnitOfWork, isNew);
            GroupsViewModel.GroupChanged += OnGroupChanged;

            //назначаем менеджера
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
            //если придет запрос при несохраненных изменениях
            if (SaveCommand.CanExecute(null))
            {
                var ms = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить сделанные изменения?", defaultNo: true);
                if (ms == MessageDialogResult.Yes)
                    SaveCommand.Execute(null);
            }

            base.GoBackCommand_Execute();
        }

    }
}