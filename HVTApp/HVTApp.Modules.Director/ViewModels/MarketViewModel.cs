using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Director.ViewModels
{
    public class MarketViewModel : ViewModelBase
    {
        private bool _isLoaded = false;
        public ObservableCollection<MarketUnit> MarketUnits { get; } = new ObservableCollection<MarketUnit>();

        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ReloadCommand { get; }

        public MarketViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(async () => { await Load(); });
        }

        public async Task Load()
        {
            IsLoaded = false;

            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync()).Where(x => x.Project.ForReport);

            MarketUnits.Clear();
            MarketUnits.AddRange(
                salesUnits
                .GroupBy(x => new {x.Project.Id, x.OrderInTakeDate})
                .OrderBy(x => x.Key.OrderInTakeDate)
                .Select(x => new MarketUnit(x)));

            IsLoaded = true;
        }
    }

    public class MarketUnit
    {
        public string Project { get; }
        public string Facilities { get; }
        public double Sum { get; }
        public DateTime OrderInTakeDate { get; }
        public string Manager { get; }

        public List<SalesGroup> SalesGroups { get; }

        public MarketUnit(IEnumerable<SalesUnit> salesUnits)
        {
            if (salesUnits == null) throw new NullReferenceException($"{nameof(salesUnits)} не должен быть null");
            var salesUnitsList = salesUnits.ToList();
            if (!salesUnitsList.Any()) throw new ArgumentException($@"{nameof(salesUnits)} не имеет членов", nameof(salesUnits));

            Project = salesUnitsList.First().Project.ToString();

            var builder = new StringBuilder();
            salesUnitsList.Select(x => x.Facility).Distinct().ForEach(x => builder.Append("; ").Append($"{x}"));
            Facilities = builder.Remove(0, 2).ToString();

            Sum = salesUnitsList.Sum(x => x.Cost);

            OrderInTakeDate = salesUnitsList.First().OrderInTakeDate;

            Manager = salesUnitsList.First().Project.Manager.Employee.Person.ToString();

            SalesGroups = salesUnitsList.GroupBy(x => new
            {
                x.Cost,
                FacilityId = x.Facility.Id,
                ProductId = x.Product.Id
            })
            .Select(x => new SalesGroup(x))
            .OrderByDescending(x => x.Sum)
            .ToList();
        }
    }

    public class SalesGroup
    {
        public string Facility { get; }
        public string ProductType { get; }
        public string ProductDesignation { get; }
        public double Cost { get; }
        public int Amount { get; }
        public double Sum { get; set; }

        public SalesGroup(IEnumerable<SalesUnit> salesUnits)
        {
            if (salesUnits == null) throw new NullReferenceException($"{nameof(salesUnits)} не должен быть null");
            var salesUnitsList = salesUnits.ToList();
            if(!salesUnitsList.Any()) throw new ArgumentException($@"{nameof(salesUnits)} не имеет членов", nameof(salesUnits));

            Facility = salesUnitsList.First().Facility.ToString();
            ProductType = salesUnitsList.First().Product.ProductType?.ToString();
            ProductDesignation = salesUnitsList.First().Product.Designation;
            Cost = salesUnitsList.First().Cost;
            Amount = salesUnitsList.Count;
            Sum = Cost * Amount;
        }
    }

}
