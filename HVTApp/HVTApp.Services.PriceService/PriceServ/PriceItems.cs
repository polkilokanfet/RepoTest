using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService.PriceServ
{
    /// <summary>
    /// ��������� �������� ��
    /// </summary>
    internal class PriceItems
    {
        private readonly Dictionary<Guid, PriceItem> _items = new Dictionary<Guid, PriceItem>();

        /// <summary>
        /// ���� �� ������ ������ ������ ������� ����?
        /// </summary>
        public bool IsEmpty => _items.Any() == false;

        /// <summary>
        /// ���������� ���������� �������
        /// </summary>
        public double? Price => IsEmpty
            ? null
            : this.ActualPriceCalculationItem.Price;

        /// <summary>
        /// ������� ���������� ������ ��
        /// </summary>
        public bool HasActualPriceCalculationItem => _items.Any() &&
                                                     _items
                                                         .Select(x => x.Value)
                                                         .Any(priceItem => priceItem.PriceCalculationItem.HasPrice);

        /// <summary>
        /// ���������� ������ ��
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
        /// �������� ������ ��
        /// </summary>
        /// <param name="item">������ ��</param>
        public void Add(PriceItem item)
        {
            this.Remove(item.PriceCalculationItem);
            _items.Add(item.PriceCalculationItem.Id, item);
        }

        /// <summary>
        /// ������� ������ ��
        /// </summary>
        /// <param name="item">������ ��</param>
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