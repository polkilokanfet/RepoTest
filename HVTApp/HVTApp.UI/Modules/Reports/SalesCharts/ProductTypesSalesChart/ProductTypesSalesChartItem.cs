﻿using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart
{
    public class ProductTypesSalesChartItem : SalesChartItem
    {
        public ProductTypesSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Product.ProductType.ToString();

        public override string Title => "Тип оборудования";
    }
}