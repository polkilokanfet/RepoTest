using System.Linq;
using Infragistics.Windows.Controls;
using Infragistics.Windows.DataPresenter;

namespace HVTApp.UI
{
    public static class ExtWpf
    {
        /// <summary>
        /// Установить фильтр в 0 Layout (сначала скидывает всё, что было в этом фильтре).
        /// </summary>
        /// <param name="xamDataGrid">Таблица</param>
        /// <param name="fieldName">Имя столбца (поля)</param>
        /// <param name="addConditionFlag">Условие добавления фильтра</param>
        /// <param name="conditionValue">Значение фильтра</param>
        public static void SetFilter(this XamDataGrid xamDataGrid, string fieldName, bool addConditionFlag, bool conditionValue)
        {
            //поиск в таблице конкретного фильтра
            var recordFilter = xamDataGrid.FieldLayouts[0].RecordFilters
                .Where(filter => filter.FieldName == fieldName)
                .OrderBy(filter => filter.Version)
                .Last();

            //очистка этого фильтра
            recordFilter.Conditions.Clear();

            //если нужно скрыть реализованные строки
            if (addConditionFlag)
            {
                ComparisonCondition condition = new ComparisonCondition(ComparisonOperator.Equals, conditionValue);
                recordFilter.Conditions.Add(condition);
            }
        }

    }
}