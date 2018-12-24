using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class DatesViewModel : ViewModelBase
    {
        private IUnitOfWork _unitOfWork;
        private List<SalesUnitWrapper> _units;

        public ObservableCollection<DatesGroup> Groups { get; set; } = new ObservableCollection<DatesGroup>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public DatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            _units = (await _unitOfWork.Repository<SalesUnit>().GetAllAsync())
                                .Where(x => !x.DeliveryDate.HasValue || 
                                            !x.EndProductionDate.HasValue ||
                                            !x.PickingDate.HasValue ||
                                            !x.RealizationDate.HasValue || 
                                            !x.ShipmentDate.HasValue ||
                                            string.IsNullOrEmpty(x.SerialNumber))
                                .OrderBy(x => x.EndProductionDateCalculated)
                                .Select(x => new SalesUnitWrapper(x))
                                .ToList();

            _units.ForEach(x => x.PropertyChanged += UnitOnPropertyChanged);

            Groups.Clear();
            Groups.AddRange(DatesGroup.GetGroups(_units));
        }

        public async void SaveCommand_Execute()
        {
            await _unitOfWork.SaveChangesAsync();
            _units.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        public bool SaveCommand_CanExecute()
        {
            return _units != null && _units.All(x => x.IsValid) && _units.Any(x => x.IsChanged);
        }

        private void UnitOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}