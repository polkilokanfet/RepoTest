using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates.ServiceRealizationDates
{
    public class ServiceRealizationDatesViewModel : LoadableExportableViewModel
    {
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<ServiceRealizationDatesItem> _items;

        public ObservableCollection<ServiceRealizationDatesGroup> Groups { get; } = new ObservableCollection<ServiceRealizationDatesGroup>();

        public DelegateLogCommand SaveCommand { get; }

        public ServiceRealizationDatesViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    _items.PropertyChanged -= ItemsOnPropertyChanged;

                    //принимаем все изменения
                    _items.AcceptChanges();
                    //сохраняем изменения
                    _unitOfWork.SaveChanges();
                    //проверяем актуальность команды
                    SaveCommand.RaiseCanExecuteChanged();

                    _items.PropertyChanged += ItemsOnPropertyChanged;
                }, 
                () => _items != null && _items.IsValid && _items.IsChanged);
        }

        private IOrderedEnumerable<ServiceRealizationDatesGroup> _groups;

        protected override void GetData()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var items = _unitOfWork.Repository<SalesUnit>().GetAll()
                .Where(x => !x.IsRemoved && !x.IsLoosen && x.Specification != null)
                .Where(x => x.Product.ProductBlock.IsService)
                .OrderBy(salesUnit => salesUnit.RealizationDateCalculated)
                .Select(salesUnit => new ServiceRealizationDatesItem(salesUnit))
                .ToList();
            _items = new ValidatableChangeTrackingCollection<ServiceRealizationDatesItem>(items);

            //подписываемся на изменение каждой сущности
            _items.PropertyChanged += ItemsOnPropertyChanged;

            _groups = _items
                .GroupBy(item => new
                {
                    Cost = item.Model.Cost,
                    Facility = item.Model.Facility.Id,
                    Product = item.Model.Product.Id,
                    Order = item.Model.Order?.Id,
                    Project = item.Model.Project.Id,
                    Specification = item.Model.Specification?.Id,
                    item.RealizationDate
                })
                .Select(x => new ServiceRealizationDatesGroup(x))
                .OrderBy(x => x.Units.First().Model.OrderInTakeDate);
        }

        protected override void BeforeGetData()
        {
            Groups.Clear();
        }

        protected override void AfterGetData()
        {
            Groups.AddRange(_groups);
        }

        private void ItemsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}