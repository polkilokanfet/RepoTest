using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
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

            //назначаем менеджера
            if (Item.Manager == null)
            {
                var model = await UnitOfWork.Repository<User>().GetByIdAsync(CommonOptions.User.Id);
                Item.Manager = new UserWrapper(model);
            }

            //загружаем строки с оборудованием
            var units = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == Item.Id);
            GroupsViewModel = new SalesUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);
            await GroupsViewModel.LoadAsync();

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged += GroupsOnCollectionChanged;

            //сигнал об изменении модели
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        private void GroupsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }


        protected override async void SaveCommand_Execute()
        {

            //отписка от событий изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged -= GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged -= GroupsOnCollectionChanged;

            //добавляем сущность, если ее не существовало
            if (await UnitOfWork.Repository<Project>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.Repository<Project>().Add(Item.Model);

            Item.AcceptChanges();
            GroupsViewModel.AcceptChanges();

            //сохраняем
            try
            {
                await UnitOfWork.SaveChangesAsync();
                EventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var sb = new StringBuilder();
                Exception exception = e;
                do
                {
                    sb.AppendLine(e.Message);
                    exception = exception.InnerException;
                } while (exception != null);

                Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", sb.ToString());
            }

            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        protected override bool SaveCommand_CanExecute()
        {
            //все сущности должны быть валидны
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid || 
                GroupsViewModel.Groups == null || !GroupsViewModel.Groups.Any())
                return false;

            //какая-то сущность должна быть изменена
            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }
    }
}