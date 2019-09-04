using System;
using System.Collections.Generic;
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
        private List<SalesUnitWrapper> _salesUnitWrappers;

        public ObservableCollection<DatesGroup> Groups { get; } = new ObservableCollection<DatesGroup>();

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

            var salesUnits = await _unitOfWork.Repository<SalesUnit>().GetAllAsync();
            _salesUnitWrappers = salesUnits.Where(EditingRequired)
                                           .OrderBy(salesUnit => salesUnit.EndProductionDateCalculated)
                                           .Select(salesUnit => new SalesUnitWrapper(salesUnit)).ToList();

            //подписываемся на изменение каждой сущности
            _salesUnitWrappers.ForEach(x => x.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged());

            Groups.Clear();
            Groups.AddRange(_salesUnitWrappers.ConvertToGroups());
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

        public async void SaveCommand_Execute()
        {
            //сохраняем изменения
            await _unitOfWork.SaveChangesAsync();
            //принимаем все изменения
            _salesUnitWrappers.Where(x => x.IsChanged).ToList().ForEach(x => x.AcceptChanges());
            //проверяем актуальность команды
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public bool SaveCommand_CanExecute()
        {
            //все сущности валидны и хотя бы в одной есть изменения
            return _salesUnitWrappers != null && _salesUnitWrappers.All(x => x.IsValid) && _salesUnitWrappers.Any(x => x.IsChanged);
        }
    }
}