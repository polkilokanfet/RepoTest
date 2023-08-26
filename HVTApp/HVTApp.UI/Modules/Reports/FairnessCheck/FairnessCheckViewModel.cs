using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.FairnessCheck
{
    public class FairnessCheckViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly List<SalesUnit> _salesUnits;
        private FairnessCheckItem _selectedItemToCheck;
        private FairnessCheckItem _selectedItemResult;

        public ObservableCollection<FairnessCheckItem> ItemsToCheck { get; } = new ObservableCollection<FairnessCheckItem>();
        public ObservableCollection<FairnessCheckItem> ItemsResult { get; } = new ObservableCollection<FairnessCheckItem>();

        public FairnessCheckItem SelectedItemToCheck
        {
            get => _selectedItemToCheck;
            set
            {
                if (Equals(_selectedItemToCheck, value)) return;
                _selectedItemToCheck = value;
                RefreshResult();
            }
        }

        public FairnessCheckItem SelectedItemResult
        {
            get => _selectedItemResult;
            set
            {
                _selectedItemResult = value;
                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand LoadItemsToCheckCommand { get; }

        public FairnessCheckViewModel(IUnityContainer container) : base(container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            _salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Order != null);

            LoadItemsToCheckCommand = new DelegateLogCommand(
                () =>
                {
                    var managers = unitOfWork.Repository<User>().Find(user => user.Roles.Select(role => role.Role).Contains(Role.SalesManager));
                    var manager = container.Resolve<ISelectService>().SelectItem(managers);
                    if (manager == null) return;
                    var salesUnits = _salesUnits.Where(salesUnit => salesUnit.Project.Manager.Id == manager.Id);
                    ItemsToCheck.Clear();
                    ItemsToCheck.AddRange(TransformToFairnessCheckItems(salesUnits));
                    SelectedItemToCheck = null;
                });
        }

        private void RefreshResult()
        {
            ItemsResult.Clear();
            if (SelectedItemToCheck == null) return;
            var salesUnits = _salesUnits.Where(salesUnit =>
                salesUnit.Product.Category.Id == SelectedItemToCheck.SalesUnit.Product.Category.Id &&
                salesUnit.OrderInTakeDate >= SelectedItemToCheck.SalesUnit.OrderInTakeDate &&
                salesUnit.EndProductionDateCalculated < SelectedItemToCheck.SalesUnit.EndProductionDateCalculated);
            ItemsResult.AddRange(TransformToFairnessCheckItems(salesUnits));
            SelectedItemResult = null;
        }

        private IEnumerable<FairnessCheckItem> TransformToFairnessCheckItems(IEnumerable<SalesUnit> salesUnits)
        {
            return salesUnits
                .GroupBy(x => new { x.Product.Id, x.OrderInTakeDate, x.EndProductionDateCalculated })
                .Select(x => new FairnessCheckItem(x))
                .OrderBy(x => x.SalesUnit.OrderInTakeDate)
                .ThenBy(x => x.SalesUnit.EndProductionDateCalculated);
        }
    }
}