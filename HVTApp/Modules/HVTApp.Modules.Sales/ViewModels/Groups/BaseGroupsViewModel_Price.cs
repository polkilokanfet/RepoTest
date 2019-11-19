using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;
using HVTApp.UI.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract partial class BaseGroupsViewModel<TGroup, TMember, TModel, TAfterSaveEvent, TAfterRemoveEvent> : ViewModelBase
        where TModel : class, IUnit
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TAfterRemoveEvent : PubSubEvent<TModel>, new()
    {
        //блоки, необходимые для поиска аналогов
        protected readonly Dictionary<TGroup, PriceStructures> PriceDictionary = new Dictionary<TGroup, PriceStructures>();

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public PriceStructures PriceStructures => Groups.SelectedGroup == null ? null : PriceDictionary[Groups.SelectedGroup];

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
            PriceDictionary[grp] = GlobalAppProperties.PriceService.GetPriceStructures(grp.Model, GetPriceDate(grp), priceTerm);

            //обновляем себестоимость группы
            grp.Price = PriceDictionary[grp].TotalPriceFixedCostLess;
            grp.FixedCost = PriceDictionary[grp].TotalFixedCost;
            OnPropertyChanged(nameof(PriceStructures));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }

    }
}
