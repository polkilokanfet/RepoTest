using System;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;

namespace HVTApp.UI.Modules.Reports.FlatReport.Comparator
{
    public class BudgetComparisonItem
    {
        private readonly BudgetUnit _budgetUnit;
        private readonly FlatReportItem _flatReportItem;

        public SalesUnit SalesUnit => _budgetUnit?.SalesUnit ?? _flatReportItem.SalesUnit;

        /// <summary>
        /// Удалено из старого бюджета
        /// </summary>
        public bool IsRemoved
        {
            get
            {
                if (_flatReportItem == null)
                    return true;

                if (_flatReportItem != null && _budgetUnit == null)
                    return !_flatReportItem.InReport;

                if (_flatReportItem != null && _budgetUnit != null)
                    if (!_flatReportItem.InReport && !_budgetUnit.IsRemoved)
                        return true;

                return false;
            }
        }

        /// <summary>
        /// Новое в бюджете
        /// </summary>
        public bool IsNew
        {
            get
            {
                if (_budgetUnit == null)
                    return true;

                if (_flatReportItem != null && _budgetUnit != null)
                    if (_flatReportItem.InReport && _budgetUnit.IsRemoved)
                        return true;

                return false;

            }
        }

        /// <summary>
        /// Айтем не удален в каком-либо бюджете
        /// </summary>
        public bool ToComparision
        {
            get
            {
                if (_budgetUnit != null && _flatReportItem != null)
                {
                    if (_budgetUnit.IsRemoved && !_flatReportItem.InReport)
                        return false;
                }

                if (_budgetUnit != null && _flatReportItem == null)
                {
                    if (_budgetUnit.IsRemoved)
                        return false;
                }

                return true;
            }
        }

        public int? OrderInTakeDifference
        {
            get
            {
                if (IsNew || IsRemoved)
                    return null;

                var dif = _flatReportItem.EstimatedOrderInTakeDate.MonthsBetween(_budgetUnit.OrderInTakeDate);

                return dif == 0 ? default(int?) : dif;
            }
        }

        public int? RealizationDateDifference
        {
            get
            {
                if (IsNew || IsRemoved)
                    return null;

                var dif = _flatReportItem.EstimatedRealizationDate.MonthsBetween(_budgetUnit.RealizationDate);

                return dif == 0 ? default(int?) : dif;
            }
        }
        public double? CostDifference
        {
            get
            {
                if (IsNew || IsRemoved)
                    return null;

                var dif = _flatReportItem.EstimatedCost - _budgetUnit.Cost;

                return Math.Abs(dif) < 0.001 ? default(double?) : dif;
            }
        }

        public string Status
        {
            get
            {
                if (IsNew)
                    return "1 - новый";

                if (IsRemoved)
                    return "2 - удалён";

                if (OrderInTakeDifference != null || RealizationDateDifference != null || CostDifference != null)
                    return "3 - изменён";

                return "4 - не измененён";
            }
        }

        public BudgetComparisonItem(BudgetUnit budgetUnit)
        {
            _budgetUnit = budgetUnit;
        }

        public BudgetComparisonItem(BudgetUnit budgetUnit, FlatReportItem flatReportItem)
        {
            _budgetUnit = budgetUnit;
            _flatReportItem = flatReportItem;
        }
    }
}