using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Services.PriceService1
{
    /// <summary>
    /// ��������� �������� ��
    /// </summary>
    internal class PriceItems
    {
        private readonly Dictionary<Guid, PriceCalculationItem> _items = new Dictionary<Guid, PriceCalculationItem>();

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
                                                     _items.Any(item => item.Value.HasPrice);

        /// <summary>
        /// ���������� ������ ��
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
        /// �������� ������ �� (�������������� ������ ���������������, ���� �� ����)
        /// </summary>
        /// <param name="priceCalculationItem">������ ��</param>
        public void Add(PriceCalculationItem priceCalculationItem)
        {
            this.Remove(priceCalculationItem);
            _items.Add(priceCalculationItem.Id, priceCalculationItem);
        }

        /// <summary>
        /// ������� ������ ��
        /// </summary>
        /// <param name="priceCalculationItem">������ ��</param>
        /// <returns></returns>
        public bool Remove(PriceCalculationItem priceCalculationItem)
        {
            return _items.ContainsKey(priceCalculationItem.Id) && 
                   _items.Remove(priceCalculationItem.Id);
        }
    }
}