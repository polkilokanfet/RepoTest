using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class OrderViewModel : OrderDetailsViewModel
    {
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitsWrappers;

        public GroupsContainer<ProductionGroup> GroupsInOrder { get; } = new GroupsContainer<ProductionGroup>();
        public GroupsContainer<ProductionGroup> GroupsPotential { get; } = new GroupsContainer<ProductionGroup>();

        public ICommand SaveOrderCommand { get; }
        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand ShowProductStructureCommand { get; }

        public OrderViewModel(IUnityContainer container) : base(container)
        {
            SaveOrderCommand = new DelegateCommand(async () =>
                {
                    _salesUnitsWrappers.AcceptChanges();
                    await UnitOfWork.SaveChangesAsync();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveOrderEvent>().Publish(Item.Model);
                },

                () =>
                {
                    var unitsIsValid = _salesUnitsWrappers != null && _salesUnitsWrappers.IsValid;
                    var orderIsValid = Item != null && Item.IsValid && GroupsInOrder.Any();
                    return unitsIsValid && orderIsValid && (_salesUnitsWrappers.IsChanged || Item.IsChanged);
                });

            AddGroupCommand = new DelegateCommand(AddGroupCommand_Execute, () => GroupsPotential.SelectedGroup != null);

            RemoveGroupCommand = new DelegateCommand(RemoveGroupCommand_Execute, () => GroupsInOrder.SelectedGroup != null);

            ShowProductStructureCommand = new DelegateCommand(() =>
                {
                    var productStructure = new ProductStructureViewModel(GroupsPotential.SelectedGroup.Unit.Model);
                    Container.Resolve<IDialogService>().Show(productStructure);
                }, 

                () => GroupsPotential.SelectedGroup != null);
        }

        private void AddGroupCommand_Execute()
        {
            var targetGroup = GroupsPotential.SelectedGroup;

            //��������� �����
            targetGroup.Order = Item;

            //��������� ���� �������� � �����
            targetGroup.SignalToStartProductionDone = DateTime.Today;

            //������ �������������� ���� ������������
            targetGroup.EndProductionPlanDate = targetGroup.Unit.DeliveryDateExpected;

            //��������� ������� ������
            targetGroup.FillPositions();

            //��������� ������ � ���� ������������
            GroupsInOrder.Add(targetGroup);

            //������� ������ �� ������������� �����
            if (GroupsPotential.Contains(targetGroup))
            {
                GroupsPotential.Remove(targetGroup);
            }
            else
            {
                //������� � ����������
                var group = GroupsPotential.Single(x => x.Groups.Contains(targetGroup));
                group.Groups.Remove(targetGroup);
                if (!group.Groups.Any())
                    GroupsPotential.Remove(group);
            }
        }

        private void RemoveGroupCommand_Execute()
        {
            var selectedGroup = GroupsInOrder.SelectedGroup;
            selectedGroup.Order = null;
            selectedGroup.SignalToStartProductionDone = null;
            selectedGroup.EndProductionPlanDate = null;
            selectedGroup.OrderPosition = null;
            GroupsPotential.Add(selectedGroup);
            GroupsInOrder.Remove(selectedGroup);
        }

        protected override async Task AfterLoading()
        {
            //��������� ����� ��� ������� ���� � ������������� �������
            //��������� ����� ��� ������� � ������������
            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => x.SignalToStartProduction != null)
                .Where(x => x.Order == null || x.Order.Id == Item.Id);
            _salesUnitsWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));

            //����� � ������
            var unitsInOrder = _salesUnitsWrappers.Where(x => x.Order != null).ToList();
            GroupsInOrder.AddRange(ProductionGroup.Grouping(unitsInOrder));

            //����� ��� ���������� � ������������
            GroupsPotential.AddRange(ProductionGroup.Grouping(_salesUnitsWrappers.Except(unitsInOrder)));

            //�������� �� ����� ��������� ������������� ������ � ������������
            GroupsPotential.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)ShowProductStructureCommand).RaiseCanExecuteChanged();
            };

            //�������� �� ����� ��������� ������ � ������������
            GroupsInOrder.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
            };

            //�������� �� ��������� ������� ������ � ������
            Item.PropertyChanged += OnPropertyChanged;
            _salesUnitsWrappers?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            await base.AfterLoading();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand)SaveOrderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
        }

    }
}