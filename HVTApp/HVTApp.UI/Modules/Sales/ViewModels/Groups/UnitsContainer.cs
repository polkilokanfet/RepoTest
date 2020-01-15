using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public abstract class UnitsContainer<TModel, TWrapper, TDetailsViewModel, TGroupsViewModel, TUnit, TAfterSaveModelEvent> : ViewModelBase
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
        where TDetailsViewModel : IDetailsViewModel<TWrapper, TModel>
        where TGroupsViewModel : IGroupsViewModel<TUnit, TWrapper>
        where TAfterSaveModelEvent : PubSubEvent<TModel>, new()
    {
        //детали
        public TDetailsViewModel DetailsViewModel { get; }

        //группы юнитов
        public TGroupsViewModel GroupsViewModel { get; }

        public ICommand SaveCommand { get; }

        protected UnitsContainer(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<TDetailsViewModel>();
            GroupsViewModel = container.Resolve<TGroupsViewModel>();
            SaveCommand = new DelegateCommand(SaveCommandExecute, SaveCommandCanExecute);
        }

        public virtual void Load(TModel model, bool isNew, object parameter = null)
        {
            //детали
            DetailsViewModel.Load(model, UnitOfWork);
            DetailsViewModel.Item.PropertyChanged += ItemOnPropertyChanged;

            //группы юнитов
            var units = GetUnits(model, parameter);
            GroupsViewModel.Load(units, DetailsViewModel.Item, UnitOfWork, isNew);
            GroupsViewModel.GroupChanged += OnGroupChanged;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Получение связанных с объектом юнитов
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected abstract IEnumerable<TUnit> GetUnits(TModel model, object parameter = null);

        #region SaveCommand

        protected void SaveCommandExecute()
        {
            //отписка от событий изменения строк с оборудованием
            this.GroupsViewModel.GroupChanged -= OnGroupChanged;

            GroupsViewModel.AcceptChanges();

            //добавляем сущность, если ее не существовало
            if (UnitOfWork.Repository<TModel>().GetById(DetailsViewModel.Item.Model.Id) == null)
                UnitOfWork.Repository<TModel>().Add(DetailsViewModel.Item.Model);

            DetailsViewModel.Item.AcceptChanges();
            Container.Resolve<IEventAggregator>().GetEvent<TAfterSaveModelEvent>().Publish(DetailsViewModel.Item.Model);

            //сохраняем
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", e.GetAllExceptions());
            }

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.GroupChanged += OnGroupChanged;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool SaveCommandCanExecute()
        {
            //все сущности должны быть валидны
            if (!GroupsViewModel.IsValid || !DetailsViewModel.Item.IsValid)
                return false;

            //какая-то сущность должна быть изменена
            return DetailsViewModel.Item.IsChanged || GroupsViewModel.IsChanged;
        }

        #endregion

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnGroupChanged()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //если придет запрос при несохраненных изменениях
            if (SaveCommandCanExecute())
            {
                var ms = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить сделанные изменения?");
                if(ms == MessageDialogResult.Yes)
                    SaveCommandExecute();
            }

            base.GoBackCommand_Execute();
        }
    }
}