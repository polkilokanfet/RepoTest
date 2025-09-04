using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public class ProjectItemsCollection : IEnumerable<MarketProjectItem>, INotifyCollectionChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<MarketProjectItem> _projectItems 
            = new ObservableCollection<MarketProjectItem>();

        public ProjectItemsCollection(IEventAggregator eventAggregator, Market2ViewModel viewModel)
        {
            _eventAggregator = eventAggregator;

            _projectItems.CollectionChanged += (sender, args) => this.CollectionChanged?.Invoke(this, args);

            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                this
                    .Where(marketProjectItem => marketProjectItem.Project.Id == project.Id)
                    .ForEach(marketProjectItem => marketProjectItem.Project = project);
            });

            eventAggregator.GetEvent<AfterChangeSalesUnitsEvent>().Subscribe(salesUnits =>
            {
                var result = new List<SalesUnit>(salesUnits);
                if (result.Any() == false) return;

                var projectItems = this
                    .Where(item => item.Project.Id == result.First().Project.Id)
                    .ToList();

                var salesUnits1 = projectItems.SelectMany(item => item.SalesUnits);
                foreach (var salesUnit in salesUnits1)
                {
                    if (result.Select(x => x.Id).Contains(salesUnit.Id) == false) 
                        result.Add(salesUnit);
                }

                projectItems.ForEach(marketProjectItem => this._projectItems.Remove(marketProjectItem));

                this.AddItems(result);

                viewModel.SelectedItem = this.Single(x => x.SalesUnits.Contains(salesUnits.First()));
            });
        }

        private void AddItems(IEnumerable<SalesUnit> salesUnits)
        {
            var items = salesUnits
                .GroupBy(salesUnit => salesUnit, new MarketProjectItem.Comparer())
                .Select(units => new MarketProjectItem(units))
                .OrderBy(projectItem => projectItem.DaysToStartProduction)
                .ThenBy(projectItem => projectItem.OrderInTakeDate);

            _projectItems.AddRange(items);
        }

        public IEnumerator<MarketProjectItem> GetEnumerator()
        {
            return _projectItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            this._projectItems.Clear();
            this.AddItems(salesUnits);
        }
    }
}