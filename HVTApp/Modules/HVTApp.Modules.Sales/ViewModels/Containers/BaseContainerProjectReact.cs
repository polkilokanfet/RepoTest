using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    /// <summary>
    /// Контейнеры, отображение в которых зависит от выбранного проекта.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TLookup"></typeparam>
    /// <typeparam name="TSelectedItemChangedEvent"></typeparam>
    /// <typeparam name="TAfterSaveItemEvent"></typeparam>
    /// <typeparam name="TAfterRemoveItemEvent"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class BaseContainerProjectReact<T, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, 
        TAfterRemoveItemEvent, TCollection> : 
        BaseContainer<T, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent, TCollection>
        where T : class, IBaseEntity
        where TLookup : LookupItem<T>
        where TSelectedItemChangedEvent : PubSubEvent<T>, new()
        where TAfterSaveItemEvent : PubSubEvent<T>, new()
        where TAfterRemoveItemEvent : PubSubEvent<T>, new()        
        where TCollection : ItemsCollection<T, TLookup>
    {
        protected BaseContainerProjectReact(IUnityContainer container) : base(container)
        {
            container.Resolve<IEventAggregator>().GetEvent<SelectedProjectChangedEvent>().Subscribe(OnAfterSelectProjectEvent);
        }

        /// <summary>
        /// Реакция на смену выбранного проекта
        /// </summary>
        /// <param name="project"></param>
        protected virtual void OnAfterSelectProjectEvent(Project project)
        {
            Items.Clear();
            Items.AddRange(GetActualForProjectItems(project));
        }

        /// <summary>
        /// Вернуть актуальные для проекта сущности
        /// </summary>
        /// <param name="project"> Проект </param>
        /// <returns></returns>
        protected abstract IEnumerable<T> GetActualForProjectItems(Project project);

    }
}