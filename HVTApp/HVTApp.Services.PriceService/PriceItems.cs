using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Services.PriceService1
{
    /// <summary>
    /// Контейнер расчетов ПЗ
    /// </summary>
    internal class PriceItems
    {
        private readonly Dictionary<Guid, PriceCalculationItem> _items = new Dictionary<Guid, PriceCalculationItem>();

        /// <summary>
        /// Пуст ли данный список единиц расчета цены?
        /// </summary>
        public bool IsEmpty => _items.Any() == false;

        /// <summary>
        /// Актуальные Переменные затраты
        /// </summary>
        public double? Price => IsEmpty
            ? null
            : this.ActualPriceCalculationItem.Price;

        /// <summary>
        /// Имеется актуальный расчет ПЗ
        /// </summary>
        public bool HasActualPriceCalculationItem => _items.Any() &&
                                                     _items.Any(item => item.Value.HasPrice);

        /// <summary>
        /// Актуальный расчет ПЗ
        /// </summary>
        public PriceCalculationItem ActualPriceCalculationItem => HasActualPriceCalculationItem
            ? _items
                .Select(x => x.Value)
                .Where(priceItem => priceItem.HasPrice)
                .OrderBy(priceItem => priceItem.FinishDate)
                .Last()
            : null;

        public PriceItems(IEnumerable<PriceCalculationItem> priceCalculationItems) =>
            priceCalculationItems.ForEach(this.Add);

        /// <summary>
        /// Добавить расчет ПЗ (предварительно удалив предшественника, если он есть)
        /// </summary>
        /// <param name="priceCalculationItem">расчет ПЗ</param>
        public void Add(PriceCalculationItem priceCalculationItem)
        {
            this.Remove(priceCalculationItem);
            _items.Add(priceCalculationItem.Id, priceCalculationItem);
        }

        /// <summary>
        /// Удалить расчет ПЗ
        /// </summary>
        /// <param name="priceCalculationItem">расчет ПЗ</param>
        /// <returns></returns>
        public bool Remove(PriceCalculationItem priceCalculationItem)
        {
            return _items.ContainsKey(priceCalculationItem.Id) && 
                   _items.Remove(priceCalculationItem.Id);
        }
    }
}