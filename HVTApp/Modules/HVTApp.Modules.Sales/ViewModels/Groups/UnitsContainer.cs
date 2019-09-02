using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class UnitsContainer<TModel, TWrapper, TDetailsViewModel, TGroupsViewModel, TUnit, TAfterSaveModelEvent> : ViewModelBase
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
        where TDetailsViewModel : IDetailsViewModel<TWrapper, TModel>
        where TGroupsViewModel : IGroupsViewModel<TUnit, TWrapper>
        where TAfterSaveModelEvent : PubSubEvent<TModel>, new()
    {
        //������
        public TDetailsViewModel DetailsViewModel { get; }

        //������ ������
        public TGroupsViewModel GroupsViewModel { get; }

        public ICommand SaveCommand { get; }

        protected UnitsContainer(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<TDetailsViewModel>();
            GroupsViewModel = container.Resolve<TGroupsViewModel>();
            SaveCommand = new DelegateCommand(SaveCommandExecute, SaveCommandCanExecute);
        }

        public virtual async Task LoadAsync(TModel model, bool isNew, object parameter = null)
        {
            //������
            await DetailsViewModel.LoadAsync(model, UnitOfWork);
            DetailsViewModel.Item.PropertyChanged += ItemOnPropertyChanged;

            //������ ������
            var units = await GetUnits(model, parameter);
            GroupsViewModel.Load(units, DetailsViewModel.Item, UnitOfWork, isNew);
            GroupsViewModel.GroupChanged += OnGroupChanged;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// ��������� ��������� � �������� ������
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<TUnit>> GetUnits(TModel model, object parameter = null);

        #region SaveCommand

        protected async void SaveCommandExecute()
        {
            //������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.GroupChanged -= OnGroupChanged;

            //��������� ��������, ���� �� �� ������������
            if (await UnitOfWork.Repository<TModel>().GetByIdAsync(DetailsViewModel.Item.Model.Id) == null)
                UnitOfWork.Repository<TModel>().Add(DetailsViewModel.Item.Model);

            DetailsViewModel.Item.AcceptChanges();
            Container.Resolve<IEventAggregator>().GetEvent<TAfterSaveModelEvent>().Publish(DetailsViewModel.Item.Model);

            GroupsViewModel.AcceptChanges();

            //���������
            try
            {
                await UnitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� ����������", e.GetAllExceptions());
            }

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.GroupChanged += OnGroupChanged;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool SaveCommandCanExecute()
        {
            //��� �������� ������ ���� �������
            if (!GroupsViewModel.IsValid || !DetailsViewModel.Item.IsValid)
                return false;

            //�����-�� �������� ������ ���� ��������
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
            //���� ������ ������ ��� ������������� ����������
            if (SaveCommandCanExecute())
            {
                var ms = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ��������� ���������?");
                if(ms == MessageDialogResult.Yes)
                    SaveCommandExecute();
            }

            base.GoBackCommand_Execute();
        }
    }
}