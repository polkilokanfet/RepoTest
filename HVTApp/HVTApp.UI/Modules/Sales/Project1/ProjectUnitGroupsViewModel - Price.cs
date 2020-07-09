using System;
using System.Collections.Generic;
using HVTApp.Model;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public partial class ProjectUnitGroupsViewModel
    {
        //�����, ����������� ��� ������ ��������
        protected readonly Dictionary<IProjectUnit, Price> PriceDictionary = new Dictionary<IProjectUnit, Price>();

        /// <summary>
        /// ��������� ������������� ��������� ������
        /// </summary>
        public List<Price> Prices => Groups.SelectedGroup == null 
            ? null
            : new List<Price> { PriceDictionary[Groups.SelectedGroup] };

        /// <summary>
        /// ���������� ������������� ������.
        /// </summary>
        /// <param name="unit"></param>
        protected void RefreshPrice(IProjectUnit unit)
        {
            if (unit == null) return;

            //���� � ������� ��� ����� ������, ��������� �
            if (!PriceDictionary.ContainsKey(unit))
                PriceDictionary.Add(unit, null);

            //��������� ��������� ������������� ���� ������
            var priceDate = unit.Model.OrderInTakeDate < DateTime.Today ? unit.Model.OrderInTakeDate : DateTime.Today;
            PriceDictionary[unit] = GlobalAppProperties.PriceService.GetPrice(unit.Model, priceDate);

            //��������� ������������� ������
            unit.Price = PriceDictionary[unit].SumPriceTotal;
            unit.FixedCost = PriceDictionary[unit].SumFixedTotal;
            OnPropertyChanged(nameof(Prices));

            //���� � ������ ���� ��������� ������ - �������� � ��� ���
            (unit as ProjectUnitGroup)?.ForEach(RefreshPrice);
        }
    }
}
