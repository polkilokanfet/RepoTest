using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Model.Events;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public class ProjectItemsCollection : ObservableCollection<ProjectItem>
    {
        private readonly Market2ViewModel _viewModel;

        public ProjectItemsCollection(Market2ViewModel viewModel, IEventAggregator eventAggregator)
        {
            _viewModel = viewModel;

            //при добавлении или удалении айтема, подписываем/отписываем на событие удаления
            this.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var projectItem in args.NewItems.Cast<ProjectItem>())
                    {
                        projectItem.LastSalesUnitRemoveEvent += ProjectItemOnLastSalesUnitRemoveEvent;
                    }
                }

                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var projectItem in args.OldItems.Cast<ProjectItem>())
                    {
                        projectItem.LastSalesUnitRemoveEvent -= ProjectItemOnLastSalesUnitRemoveEvent;
                    }
                }
            };

            //реакция на сохранение юнита
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                //проверяем, можно ли юнит поместить в существующую группу
                this.ToList().ForEach(projectItem => projectItem.Check(salesUnit));

                //если не смогли пристроить в существующую группу, создаем новую
                if (!this.SelectMany(projectItem => projectItem.SalesUnits).Contains(salesUnit))
                {
                    this.Add(new ProjectItem(new[] { salesUnit }, eventAggregator));
                }
            });

        }

        //удалить айтем, если он уже опустел
        private void ProjectItemOnLastSalesUnitRemoveEvent(ProjectItem item)
        {
            if (this.Contains(item))
            {
                if (_viewModel.SelectedProjectItem == item)
                {
                    _viewModel.SelectedItem = null;
                }
                this.Remove(item);
            }
        }
    }
}