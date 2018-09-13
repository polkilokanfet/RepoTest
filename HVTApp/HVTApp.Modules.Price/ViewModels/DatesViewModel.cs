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

namespace HVTApp.Modules.Price.ViewModels
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
            Action save = async () =>
            {
                await _unitOfWork.SaveChangesAsync();
                _units.ForEach(x => x.AcceptChanges());
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            };
            SaveCommand = new DelegateCommand(save, () => _units != null && _units.All(x => x.IsValid) && _units.Any(x => x.IsChanged));
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            var units = await _unitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            units = units.Where(x => !x.DeliveryDate.HasValue || 
                                     !x.EndProductionDate.HasValue ||
                                     !x.PickingDate.HasValue ||
                                     !x.RealizationDate.HasValue ||
                                     !x.ShipmentDate.HasValue ||
                                     x.OrderPosition == null).OrderBy(x => x.EndProductionDateCalculated).ToList();
            _units = units.Select(x => new SalesUnitWrapper(x)).ToList();
            _units.ForEach(x => x.PropertyChanged += XOnPropertyChanged);

            Groups.Clear();
            Groups.AddRange(DatesGroup.GetGroups(_units));
        }

        private void XOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}