using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    /// <summary>
    /// ����������, ����������� � ������� ������� �� ���������� �������.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TLookup"></typeparam>
    /// <typeparam name="TSelectedItemChangedEvent"></typeparam>
    /// <typeparam name="TAfterSaveItemEvent"></typeparam>
    /// <typeparam name="TAfterRemoveItemEvent"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    /// <typeparam name="TSelectedFiltChangedEvent"></typeparam>
    public abstract class BaseContainerFilt<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent, TFilter, TSelectedFiltChangedEvent> : 
                                      BaseContainer<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem>
        where TSelectedItemChangedEvent : PubSubEvent<TItem>, new()
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
        where TFilter : class, IBaseEntity
        where TSelectedFiltChangedEvent : PubSubEvent<TFilter>, new()
    {
        /// <summary>
        /// ����������� �������� (��������, ������)
        /// </summary>
        protected TFilter Filter;

        protected BaseContainerFilt(IUnityContainer container) : base(container)
        {
            // ������� �� ����� �������
            container.Resolve<IEventAggregator>().GetEvent<TSelectedFiltChangedEvent>().Subscribe(OnFilterChanged);
        }

        //������� �� ������� ��������� �������
        protected virtual void OnFilterChanged(TFilter filter)
        {
            Filter = filter;

            this.Clear();
            if (filter != null)
                this.AddRange(GetActualLookups(filter));
        }

        /// <summary>
        /// ������� ���������� ��� ������� ��������
        /// </summary>
        /// <param name="filt"> ������ </param>
        /// <returns></returns>
        protected abstract IEnumerable<TLookup> GetActualLookups(TFilter filt);
    }
}