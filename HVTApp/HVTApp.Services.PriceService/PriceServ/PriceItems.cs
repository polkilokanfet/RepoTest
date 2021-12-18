using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService.PriceServ
{
    /// <summary>
    /// Контейнер расчетов ПЗ
    /// </summary>
    internal class PriceItems
    {
        private readonly Dictionary<Guid, PriceItem> _items = new Dictionary<Guid, PriceItem>();

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
                                                     _items
                                                         .Select(x => x.Value)
                                                         .Any(priceItem => priceItem.PriceCalculationItem.HasPrice);

        /// <summary>
        /// Актуальный расчет ПЗ
        /// </summary>
        public PriceCalculationItem ActualPriceCalculationItem => HasActualPriceCalculationItem
            ? _items
                .Select(x => x.Value)
                .Where(priceItem => priceItem.PriceCalculationItem.HasPrice)
                .OrderBy(priceItem => priceItem.FinishDate)
                .Last()
                .PriceCalculationItem
            : null;

        public PriceItems(IEnumerable<PriceItem> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Добавить расчет ПЗ
        /// </summary>
        /// <param name="item">расчет ПЗ</param>
        public void Add(PriceItem item)
        {
            this.Remove(item.PriceCalculationItem);
            _items.Add(item.PriceCalculationItem.Id, item);
        }

        /// <summary>
        /// Удалить расчет ПЗ
        /// </summary>
        /// <param name="item">расчет ПЗ</param>
        /// <returns></returns>
        public bool Remove(PriceCalculationItem item)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items.Remove(item.Id);
                return true;
            }

            return false;
        }
    }
}