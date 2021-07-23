using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public abstract partial class BaseGroupsViewModel<TGroup, TMember, TModel, TAfterSaveEvent, TAfterRemoveEvent>
        where TModel : class, IUnit
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TAfterRemoveEvent : PubSubEvent<TModel>, new()
    {
        //блоки, необходимые для поиска аналогов
        protected readonly Dictionary<TGroup, Price> PriceDictionary = new Dictionary<TGroup, Price>();

        protected readonly Dictionary<TGroup, Price> PriceDictionaryLaborHours = new Dictionary<TGroup, Price>();

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public List<Price> Prices => Groups.SelectedGroup == null 
            ? null 
            : new List<Price> { PriceDictionary[Groups.SelectedGroup] };

        public List<Price> PricesLaborHours => Groups.SelectedGroup == null 
            ? null
            : new List<Price> { PriceDictionaryLaborHours[Groups.SelectedGroup] };

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

            this.RefreshPriceLaborHours(grp);

            //срок актуальности
            var priceTerm = GlobalAppProperties.Actual.ActualPriceTerm;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionary.ContainsKey(grp))
                PriceDictionary.Add(grp, null);

            //обновляем структуру себестоимости этой группе
            PriceDictionary[grp] = GlobalAppProperties.PriceService.GetPrice(grp.Model, GetPriceDate(grp), true);

            //обновляем себестоимость группы
            grp.Price = PriceDictionary[grp].SumPriceTotal;
            grp.FixedCost = PriceDictionary[grp].SumFixedTotal;

            //основная з/п
            var primaryPayment = PriceDictionary[grp].LaborHoursTotal * GlobalAppProperties.PriceService.GetLaborHoursCost(GetPriceDate(grp));
            //отчисления
            var dif = primaryPayment * 30.7 / 100.0;
            //резерв отпусков
            var vac = (primaryPayment + dif) * 7.7 / 100;
            //фонд оплаты труда
            grp.WageFund = primaryPayment + dif + vac;

            RaisePropertyChanged(nameof(Prices));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }

        private void RefreshPriceLaborHours(TGroup grp)
        {
            if (grp == null) return;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionaryLaborHours.ContainsKey(grp))
                PriceDictionaryLaborHours.Add(grp, null);

            //обновляем структуру себестоимости этой группе
            PriceDictionaryLaborHours[grp] = GlobalAppProperties.PriceService.GetPrice(grp.Model, GetPriceDate(grp), false);

            //обновляем себестоимость группы
            grp.Price = PriceDictionaryLaborHours[grp].SumPriceTotal;
            grp.FixedCost = PriceDictionaryLaborHours[grp].SumFixedTotal;

            //основная з/п
            var primaryPayment = PriceDictionaryLaborHours[grp].LaborHoursTotal * GlobalAppProperties.PriceService.GetLaborHoursCost(GetPriceDate(grp));
            //отчисления
            var dif = primaryPayment * 30.7 / 100.0;
            //резерв отпусков
            var vac = (primaryPayment + dif) * 7.7 / 100;
            //фонд оплаты труда
            grp.WageFund = primaryPayment + dif + vac;

            RaisePropertyChanged(nameof(PricesLaborHours));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }
    }
}
