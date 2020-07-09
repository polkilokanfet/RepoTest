using System;
using System.Collections.Generic;
using HVTApp.Model;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public partial class ProjectUnitGroupsViewModel
    {
        //блоки, необходимые для поиска аналогов
        protected readonly Dictionary<IProjectUnit, Price> PriceDictionary = new Dictionary<IProjectUnit, Price>();

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public List<Price> Prices => Groups.SelectedGroup == null 
            ? null
            : new List<Price> { PriceDictionary[Groups.SelectedGroup] };

        /// <summary>
        /// Обновление себестоимости группы.
        /// </summary>
        /// <param name="unit"></param>
        protected void RefreshPrice(IProjectUnit unit)
        {
            if (unit == null) return;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionary.ContainsKey(unit))
                PriceDictionary.Add(unit, null);

            //обновляем структуру себестоимости этой группе
            var priceDate = unit.Model.OrderInTakeDate < DateTime.Today ? unit.Model.OrderInTakeDate : DateTime.Today;
            PriceDictionary[unit] = GlobalAppProperties.PriceService.GetPrice(unit.Model, priceDate);

            //обновляем себестоимость группы
            unit.Price = PriceDictionary[unit].SumPriceTotal;
            unit.FixedCost = PriceDictionary[unit].SumFixedTotal;
            OnPropertyChanged(nameof(Prices));

            //если в группе есть зависимые группы - обновить и для них
            (unit as ProjectUnitGroup)?.ForEach(RefreshPrice);
        }
    }
}
