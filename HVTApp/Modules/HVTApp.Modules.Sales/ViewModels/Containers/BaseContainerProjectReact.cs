using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    /// <summary>
    /// Контейнеры, отображение в которых зависит от выбранного проекта.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TLookup"></typeparam>
    /// <typeparam name="TSelectedItemChangedEvent"></typeparam>
    /// <typeparam name="TAfterSaveItemEvent"></typeparam>
    /// <typeparam name="TAfterRemoveItemEvent"></typeparam>
    public abstract class BaseContainerProjectReact<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent> : 
        BaseContainer<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem>
        where TSelectedItemChangedEvent : PubSubEvent<TItem>, new()
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()        
    {
        protected BaseContainerProjectReact(IUnityContainer container) : base(container)
        {
            // Реакция на смену выбранного проекта
            container.Resolve<IEventAggregator>().GetEvent<SelectedProjectChangedEvent>().Subscribe(project =>
            {
                this.Clear();
                this.AddRange(GetActualForProjectLookups(project));
            });
        }

        /// <summary>
        /// Вернуть актуальные для проекта сущности
        /// </summary>
        /// <param name="project"> Проект </param>
        /// <returns></returns>
        protected abstract IEnumerable<TLookup> GetActualForProjectLookups(Project project);

    }
}