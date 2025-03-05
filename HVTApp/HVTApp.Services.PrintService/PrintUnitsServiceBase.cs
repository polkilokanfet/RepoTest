using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public abstract class PrintUnitsServiceBase : PrintServiceBase
    {
        protected PrintUnitsServiceBase(IUnityContainer container) : base(container)
        {
        }

        protected static List<UnitsGroup> GetUnitsGroups(IEnumerable<IUnit> offerUnits)
        {
            //разбиваем на группы, а их делим по объектам
            var offerUnitsGroupsByFacilities = offerUnits
                .Select(offerUnit => new Unit(offerUnit))
                .GroupBy(unit => unit, new Unit.Comparer())
                .Select(x => new UnitsGroup(x))
                .OrderBy(x => x.Units.First(), new Unit.ProductCostComparer())
                .GroupBy(offerUnitsGroup => offerUnitsGroup.Facility)
                .ToList();

            var offerUnitsGroups = offerUnitsGroupsByFacilities.SelectMany(x => x).ToList();

            //назначаем позиции ТКП
            var i = 1;
            offerUnitsGroups.ForEach(offerUnitsGroup => offerUnitsGroup.Position = i++);

            return offerUnitsGroups;
        }


        protected void PrintUnitsTable(List<UnitsGroup> unitsGroups, WordDocumentWriter docWriter,
            Font fontBold, List<IGrouping<Facility, UnitsGroup>> unitsGroupsByFacilities,
            IVatContainer vatContainer)
        {
            var printComments = false;
            if (unitsGroups.Any(x => string.IsNullOrWhiteSpace(x.Comment) == false))
            {
                printComments = MessageService.ConfirmationDialog("Печать комментариев", "Вы хотите включить в таблицу комментарии?", defaultNo: true);
            }
            int columnsCount = printComments ? 7 : 6;

            var colorTableHeader = Colors.AliceBlue;

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            var tableProperties = GetTableProperties(docWriter, tableBorderProperties);
            tableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(columnsCount, tableProperties);

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            if (printComments)
                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                    "№", "Тип оборудования", "Обозначение", "Комментарий", "Кол.", "Стоимость", "Сумма");
            else
                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                    "№", "Тип оборудования", "Обозначение", "Кол.", "Стоимость", "Сумма");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            var dr2 = MessageService.ConfirmationDialog("Обозначение", "Использовать полное обозначение оборудования?", defaultYes: true);
            bool printFullDesignation = dr2;

            foreach (var unitsGroupByFacility in unitsGroupsByFacilities)
            {
                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = columnsCount;
                docWriter.PrintTableCell($"{unitsGroupByFacility.Key}", tableCellProperties, font: fontBold); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var offerUnitsGroup in unitsGroupByFacility)
                {
                    docWriter.StartTableRow();

                    docWriter.PrintTableCell(offerUnitsGroup.Position.ToString(), tableCellProperties); //номер строки ТКП
                    docWriter.PrintTableCell(offerUnitsGroup.Product.ProductType?.Name, tableCellProperties); //тип оборудования
                    var des = printFullDesignation ? offerUnitsGroup.Product.Designation : offerUnitsGroup.Product.Category.NameShort;
                    docWriter.PrintTableCell(des, tableCellProperties); //обозначение
                    if (printComments) docWriter.PrintTableCell($"{offerUnitsGroup.Comment}", tableCellProperties); //комментарий
                    docWriter.PrintTableCell($"{offerUnitsGroup.Amount:D}", tableCellProperties, parPropRight); //количество
                    docWriter.PrintTableCell($"{offerUnitsGroup.Cost:N}", tableCellProperties, parPropRight); //стоимость
                    docWriter.PrintTableCell($"{offerUnitsGroup.Total:N}", tableCellProperties, parPropRight); //сумма

                    docWriter.EndTableRow();
                }
            }

            //сумма ТКП
            var sum = unitsGroups.Sum(x => x.Amount * x.Cost);

            tableCellProperties.BackColor = colorTableHeader;

            PrintSumTableString("Итого без НДС:", sum, docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);
            PrintSumTableString($"НДС ({vatContainer.Vat} %):", sum * vatContainer.Vat / 100, docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);
            PrintSumTableString("Итого с НДС:", sum * (1 + vatContainer.Vat / 100), docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);

            docWriter.EndTable();
        }



        protected static void PrintSumTableString(
            string text, double sum, WordDocumentWriter docWriter, TableCellProperties tableCellProperties,
            Font font, ParagraphProperties parProp, int columnsCount)
        {
            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = columnsCount - 1;
            docWriter.PrintTableCell(text, tableCellProperties, null, font);
            tableCellProperties.ColumnSpan = 1;
            docWriter.PrintTableCell($"{sum:N}", tableCellProperties, parProp, font);
            docWriter.EndTableRow();
        }

        protected string GetShipmentConditions(List<UnitsGroup> offerUnitsGroups)
        {
            if (offerUnitsGroups.Any(x => x.CostDelivery.HasValue && x.CostDelivery > 0))
            {
                if (offerUnitsGroups.All(x => x.CostDelivery.HasValue && x.CostDelivery > 0))
                    return "В стоимости оборудования учтены расходы, связанные с его доставкой на объект.";

                var positions = offerUnitsGroups
                    .Where(x => x.CostDelivery.HasValue && x.CostDelivery > 0)
                    .Select(x => x.Position).ToList();
                var end = positions.Count == 1 ? "и" : "й";

                return $"В стоимости позиции{end} {positions.ToStringEnum(", ")} учтены расходы, связанные с его доставкой на объект.";
            }
            return "В стоимости оборудования не учтены расходы, связанные с его доставкой на объект.";
        }


        protected static string PrintPaymentConditions(string text, IEnumerable<IGrouping<PaymentConditionSet, UnitsGroup>> offerUnitsGroupsGrouped, DateTime date)
        {
            var result = text;
            var g = offerUnitsGroupsGrouped as IGrouping<PaymentConditionSet, UnitsGroup>[] ?? offerUnitsGroupsGrouped.ToArray();
            if (g.Length == 1)
            {
                var paymentConditionSet = g.First().Key;
                return result + Environment.NewLine + paymentConditionSet.GetStringForOffer(date);
            }

            foreach (var unitsGroups in g)
            {
                result += Environment.NewLine + "- ";
                var positions = unitsGroups.Select(x => x.Position).ToStringEnum(", ");
                var prefix = unitsGroups.Count() == 1 ? "позиции" : "позиций";
                result += PrintPaymentConditions($"{prefix} {positions}:", unitsGroups.GroupBy(x => x.PaymentConditionSet), date);
            }
            return result;
        }


        protected static string PrintConditions<T>(string text, IEnumerable<IGrouping<T, UnitsGroup>> unitsGroupsGrouped)
        {
            var result = text;
            if (unitsGroupsGrouped.Count() == 1)
            {
                return $"{text} {unitsGroupsGrouped.First().Key}.";
            }

            foreach (var unitsGroups in unitsGroupsGrouped)
            {
                result += Environment.NewLine + "- ";
                var positions = unitsGroups.Select(x => x.Position).ToStringEnum(", ");
                var prefix = unitsGroups.Count() == 1 ? "позиции" : "позиций";
                result += $"{prefix} {positions}: {unitsGroups.Key}.";
            }
            return result;
        }


        protected override string GetFullPath(Document document, string path)
        {
            throw new NotImplementedException();
        }

        protected override void PrintBody(Document document, WordDocumentWriter docWriter)
        {
            throw new NotImplementedException();
        }

    }
}