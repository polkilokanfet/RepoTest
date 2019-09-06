using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    /// <summary>
    /// ����������, ����������� � ������� ������� �� ���������� �������.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TLookup"></typeparam>
    /// <typeparam name="TSelectedItemChangedEvent"></typeparam>
    /// <typeparam name="TAfterSaveItemEvent"></typeparam>
    /// <typeparam name="TAfterRemoveItemEvent"></typeparam>
    /// <typeparam name="TFilt"></typeparam>
    /// <typeparam name="TSelectedFiltChangedEvent"></typeparam>
    public abstract class BaseContainerFilt<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent, TFilt, TSelectedFiltChangedEvent> : 
                                      BaseContainer<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem>
        where TSelectedItemChangedEvent : PubSubEvent<TItem>, new()
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
        where TFilt : class, IBaseEntity
        where TSelectedFiltChangedEvent : PubSubEvent<TFilt>, new()
    {
        /// <summary>
        /// ����������� �������� (��������, ������)
        /// </summary>
        protected TFilt Filt;

        protected BaseContainerFilt(IUnityContainer container) : base(container)
        {
            // ������� �� ����� ���������� �������
            container.Resolve<IEventAggregator>().GetEvent<TSelectedFiltChangedEvent>().Subscribe(filt =>
            {
                Filt = filt;
                this.Clear();
                SelectedItem = null;
                this.AddRange(GetActualLookups(filt));
            });
        }

        /// <summary>
        /// ������� ���������� ��� ������� ��������
        /// </summary>
        /// <param name="filt"> ������ </param>
        /// <returns></returns>
        protected abstract IEnumerable<TLookup> GetActualLookups(TFilt filt);
    }
}