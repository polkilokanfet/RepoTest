using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
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
        private double _roundUpAccuracy = 5000;

        /// <summary>
        /// ����������� ����������
        /// </summary>
        public double RoundUpAccuracy
        {
            get { return _roundUpAccuracy; }
            set
            {
                if (value <= 0) return;
                _roundUpAccuracy = value;
            }
        }

        //������
        public TDetailsViewModel DetailsViewModel { get; }

        //������ ������
        public TGroupsViewModel GroupsViewModel { get; }

        public DelegateLogCommand SaveCommand { get; }

        /// <summary>
        /// ��������� ����
        /// </summary>
        public DelegateLogCommand RoundUpCommand { get; }

        protected UnitsContainer(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<TDetailsViewModel>();
            GroupsViewModel = container.Resolve<TGroupsViewModel>();

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    //������� �� ������� ��������� ����� � �������������
                    this.GroupsViewModel.GroupChanged -= OnGroupChanged;

                    GroupsViewModel.AcceptChanges();

                    //��������� ��������, ���� �� �� ������������
                    if (UnitOfWork.Repository<TModel>().GetById(DetailsViewModel.Item.Model.Id) == null)
                        UnitOfWork.Repository<TModel>().Add(DetailsViewModel.Item.Model);

                    DetailsViewModel.Item.AcceptChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<TAfterSaveModelEvent>().Publish(DetailsViewModel.Item.Model);

                    //���������
                    try
                    {
                        UnitOfWork.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� ����������", e.PrintAllExceptions());
                    }

                    //����������� �� ������� ��������� ����� � �������������
                    this.GroupsViewModel.GroupChanged += OnGroupChanged;

                    SaveCommand.RaiseCanExecuteChanged();

                },
                () =>
                {
                    //��� �������� ������ ���� �������
                    if (!GroupsViewModel.IsValid || !DetailsViewModel.Item.IsValid)
                        return false;
                    
                    //�����-�� �������� ������ ���� ��������
                    return DetailsViewModel.Item.IsChanged || GroupsViewModel.IsChanged;
                });

            RoundUpCommand = new DelegateLogCommand(
                () =>
                {
                    GroupsViewModel.RoundUpGroupsCosts(RoundUpAccuracy);
                });
        }

        public virtual void Load(TModel model, bool isNew, object parameter = null)
        {
            //������
            DetailsViewModel.Load(model, UnitOfWork);
            DetailsViewModel.Item.PropertyChanged += ItemOnPropertyChanged;

            //������ ������
            var units = GetUnits(model, parameter);
            GroupsViewModel.Load(units, DetailsViewModel.Item, UnitOfWork, isNew);
            GroupsViewModel.GroupChanged += OnGroupChanged;

            AfterUnitsLoading();

            SaveCommand.RaiseCanExecuteChanged();
        }

        public virtual void AfterUnitsLoading()
        {
        }

        /// <summary>
        /// ��������� ��������� � �������� ������
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected abstract IEnumerable<TUnit> GetUnits(TModel model, object parameter = null);

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void OnGroupChanged()
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public bool IsConfirmGoBackWithoutSaving { get; private set; } = false;

        protected override void GoBackCommand_Execute()
        {
            //���� ������ ������ ��� ������������� ����������
            if (SaveCommand.CanExecute())
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "��������� ��������� ���������?", defaultNo:true);
                if(dr == MessageDialogResult.Yes)
                    SaveCommand.Execute();

                if (dr == MessageDialogResult.No)
                    IsConfirmGoBackWithoutSaving = true;
            }

            base.GoBackCommand_Execute();
        }
    }
}