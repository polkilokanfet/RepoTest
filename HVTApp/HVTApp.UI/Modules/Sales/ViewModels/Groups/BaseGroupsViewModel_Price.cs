using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public abstract partial class BaseGroupsViewModel<TGroup, TMember, TModel, TAfterSaveEvent, TAfterRemoveEvent> : ViewModelBase
        where TModel : class, IUnit
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TAfterRemoveEvent : PubSubEvent<TModel>, new()
    {
        //блоки, необходимые для поиска аналогов
        protected readonly Dictionary<TGroup, Price> PriceDictionary = new Dictionary<TGroup, Price>();

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public List<Price> Prices => Groups.SelectedGroup == null ? null : new List<Price> { PriceDictionary[Groups.SelectedGroup] };

        /// <summary>
        /// Дата для расчета себестоимости.
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        protected abstract DateTime GetPriceDate(TGroup grp);

        /// <summary>
        /// Обновление себестоимости группы.
        /// </summary>
        /// <param name="grp"></param>
        protected void RefreshPrice(TGroup grp)
        {
            if (grp == null) return;

            //срок актуальности
            var priceTerm = GlobalAppProperties.Actual.ActualPriceTerm;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionary.ContainsKey(grp)) PriceDictionary.Add(grp, null);

            //обновляем структуру себестоимости этой группе
            PriceDictionary[grp] = GlobalAppProperties.PriceService.GetPrice(grp.Model, GetPriceDate(grp));

            //обновляем себестоимость группы
            grp.Price = PriceDictionary[grp].SumPriceTotal;
            grp.FixedCost = PriceDictionary[grp].SumFixedTotal;
            OnPropertyChanged(nameof(Prices));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }

    }
}
