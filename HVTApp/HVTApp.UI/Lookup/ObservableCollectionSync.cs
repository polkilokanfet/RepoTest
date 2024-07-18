using System;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using Prism.Events;

namespace HVTApp.UI.Lookup
{
    /// <summary>
    /// Коллекция, которая отслеживает события сохранения сущности (например, при редактировании её деталей)
    /// </summary>
    /// <typeparam name="TLookup"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    public class ObservableCollectionSync<TLookup, TModel, TEvent> : ObservableCollection<TLookup>
        where TLookup : LookupItem<TModel>
        where TModel : class, IBaseEntity
        where TEvent : PubSubEvent<TModel>, new()
    {
        public ObservableCollectionSync(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<TEvent>().Subscribe(AfterSaveLookupAction);
        }

        private void AfterSaveLookupAction(TModel model)
        {
            if (model == null) return;

            var lookup = this.SingleOrDefault(lookupItem => lookupItem.Id == model.Id);
            if (lookup != null)
            {
                lookup.Refresh(model);
                return;
            }

            lookup = (TLookup)Activator.CreateInstance(typeof(TLookup), model);
            this.Add(lookup);
        }
    }
}