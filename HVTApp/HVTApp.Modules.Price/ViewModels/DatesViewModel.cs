using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class DatesViewModel : ViewModelBase
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;

        public ObservableCollection<DatesGroup> Groups { get; } = new ObservableCollection<DatesGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public DatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                async () =>
                {
                    _salesUnitWrappers.PropertyChanged -= SalesUnitWrappersOnPropertyChanged;

                    //сохраняем изменения
                    await _unitOfWork.SaveChangesAsync();
                    //принимаем все изменения
                    _salesUnitWrappers.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
                    //проверяем актуальность команды
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                    _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;
                },
                () => _salesUnitWrappers != null &&
                      _salesUnitWrappers.IsValid &&
                      _salesUnitWrappers.IsChanged);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = (await _unitOfWork.Repository<SalesUnit>().GetAllAsync())
                .Where(x => x.OrderInTakeDate <= DateTime.Today)
                .Where(EditingRequired)
                .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                .Select(salesUnit => new SalesUnitWrapper(salesUnit))
                .ToList();
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits);

            //подписываемся на изменение каждой сущности
            _salesUnitWrappers.PropertyChanged += SalesUnitWrappersOnPropertyChanged;

            Groups.Clear();
            Groups.AddRange(_salesUnitWrappers.ConvertToGroups());
        }

        private void SalesUnitWrappersOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Единица требует внесения информации в текущем модуле.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        private bool EditingRequired(SalesUnit salesUnit)
        {
            return !salesUnit.DeliveryDate.HasValue ||
                   !salesUnit.EndProductionDate.HasValue ||
                   !salesUnit.PickingDate.HasValue ||
                   !salesUnit.RealizationDate.HasValue ||
                   !salesUnit.ShipmentDate.HasValue ||
                   string.IsNullOrEmpty(salesUnit.SerialNumber);
        }

    }
}