using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionPlanViewModel : LoadableBindableBase
    {
        private List<SalesUnitWrapper> _unitsToPlan;
        private ProductionGroup _selectedGroupToPlan;

        public ObservableCollection<ProductionGroup> GroupsInPlan { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToPlan { get; } = new ObservableCollection<ProductionGroup>();

        public ProductionGroup SelectedGroupToPlan
        {
            get { return _selectedGroupToPlan; }
            set { _selectedGroupToPlan = value; }
        }

        public ICommand SaveCommand { get; }
        public ICommand SetInPlanCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(async () => await UnitOfWork.SaveChangesAsync(), (() => _unitsToPlan != null && _unitsToPlan.Any(x => x.IsChanged)));
            SetInPlanCommand = new DelegateCommand(SetInPlanCommand_Execute, SetInPlanCommand_CanExecute);
        }

        private bool SetInPlanCommand_CanExecute()
        {
            return SelectedGroupToPlan?.Unit.EndProductionPlanDate != null;
        }

        private void SetInPlanCommand_Execute()
        {
            SelectedGroupToPlan.SetSignalToStartProductionDone();
            GroupsInPlan.Add(SelectedGroupToPlan);
            if (GroupsToPlan.Contains(SelectedGroupToPlan))
            {
                GroupsToPlan.Remove(SelectedGroupToPlan);
            }
            else
            {
                var group = GroupsToPlan.Single(x => x.Groups.Contains(SelectedGroupToPlan));
                group.Groups.Remove(SelectedGroupToPlan);
                if (!group.Groups.Any())
                    GroupsToPlan.Remove(group);
            }
        }

        protected override async Task LoadedAsyncMethod()
        {
            _unitsToPlan?.ForEach(x => x.PropertyChanged -= OnPropertyChanged);

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var units = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();

            var unitsInPlan = units.Where(x => x.SignalToStartProductionDone != null).ToList();
            _unitsToPlan = units.Except(unitsInPlan).Where(x => x.SignalToStartProduction != null).Select(x => new SalesUnitWrapper(x)).ToList();
            _unitsToPlan?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            GroupsInPlan.Clear();
            GroupsInPlan.AddRange(ProductionGroup.Grouping(unitsInPlan.Select(x => new SalesUnitWrapper(x))));

            GroupsToPlan.Clear();
            GroupsToPlan.AddRange(ProductionGroup.Grouping(_unitsToPlan));
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }

    public class ProductionGroup
    {
        private readonly List<SalesUnitWrapper> _units;
        private DateTime? _date;

        public SalesUnitWrapper Unit => _units.First();
        public int Amount => _units.Count;

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                if (Groups.Any())
                {
                    Groups.ForEach(x => x.Date = value);
                }
                else
                {
                    _units.ForEach(x => x.EndProductionPlanDate = value);
                }
            }
        }

        public ObservableCollection<ProductionGroup> Groups { get; } = new ObservableCollection<ProductionGroup>();

        public ProductionGroup(IEnumerable<SalesUnitWrapper> units)
        {
            _units = units.ToList();
        }

        public static IEnumerable<ProductionGroup> Grouping(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                x.EndProductionPlanDate
            }).OrderBy(x => x.Key.EndProductionPlanDate);

            return groups.Select(x => new ProductionGroup(x));
        }

        public void SetSignalToStartProductionDone()
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.SetSignalToStartProductionDone());
                return;
            }
            _units.ForEach(x => x.SignalToStartProductionDone = DateTime.Today);
        }
    }
}
