using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.ProductionViewModelEntities;
using HVTApp.Model.Services;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintNoticeOfCompletionOfProductionService : PrintServiceBase, IPrintNoticeOfCompletionOfProductionService
    {
        private List<ProductionGroup> _productionGroups;

        public PrintNoticeOfCompletionOfProductionService(IUnityContainer container) : base(container)
        {
        }

        public void PrintNoticeOfCompletionOfProduction(IEnumerable<ProductionGroup> productionGroups, Document letter, string path)
        {
            _productionGroups = productionGroups.ToList();
            this.PrintOnLetterhead(letter, path);
        }

        protected override string GetFullPath(Document document, string path)
        {
            return document.GetPath(path);
        }

        protected override void PrintBody(Document letter, WordDocumentWriter docWriter)
        {
            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            var productionGroupsByFacilities = _productionGroups.GroupBy(x => x.SalesUnit.Facility).ToList();

            #region Print Text Above Table

            docWriter.PrintParagraph(string.Empty);

            var specification = _productionGroups.First().SalesUnit.Specification;
            docWriter.PrintParagraph($"Настоящим письмом извещаем Вас о готовности оборудования, изготовленного в соответствии со спецификацией №{specification.Number} от {specification.Date.ToShortDateString()} г. к договору {specification.Contract.Number} от {specification.Contract.Date.ToShortDateString()} г.:");

            #endregion

            #region Print Main Table

            var printComments = false;
            if (_productionGroups.Any(x => string.IsNullOrWhiteSpace(x.SalesUnit.Comment) == false))
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
                    "№", "Тип оборудования", "Обозначение", "Комментарий", "Кол.", "Зав.зак.", "Зав.№№");
            else
                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                    "№", "Тип оборудования", "Обозначение", "Кол.", "Зав.зак.", "Зав.№№");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            var dr2 = MessageService.ConfirmationDialog("Обозначение", "Использовать полное обозначение оборудования?", defaultYes: true);
            bool printFullDesignation = dr2;

            foreach (var productionGroupByFacilities in productionGroupsByFacilities)
            {
                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = columnsCount;
                docWriter.PrintTableCell($"{productionGroupByFacilities.Key}", tableCellProperties, font: fontBold); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                int rowNumber = 1;
                foreach (var productionGroup in productionGroupByFacilities)
                {
                    docWriter.StartTableRow();

                    docWriter.PrintTableCell(rowNumber.ToString(), tableCellProperties); //номер строки
                    docWriter.PrintTableCell(productionGroup.SalesUnit.Product.ProductType?.Name, tableCellProperties); //тип оборудования
                    var des = printFullDesignation ? productionGroup.SalesUnit.Product.Designation : productionGroup.SalesUnit.Product.Category.NameShort;
                    docWriter.PrintTableCell(des, tableCellProperties); //обозначение
                    if (printComments) docWriter.PrintTableCell($"{productionGroup.SalesUnit.Comment}", tableCellProperties); //комментарий
                    docWriter.PrintTableCell($"{productionGroup.Amount:D}", tableCellProperties, parPropRight); //колличество
                    docWriter.PrintTableCell($"{productionGroup.SalesUnit.Order.Number}", tableCellProperties); //заводской заказ
                    docWriter.PrintTableCell($"{productionGroup.ProductionItems.Select(x => x.Model.SerialNumber).ToStringEnum()}", tableCellProperties); //заводской номер

                    rowNumber++;
                    docWriter.EndTableRow();
                }
            }

            docWriter.EndTable();

            #endregion

            #region Print Conditions

            docWriter.PrintParagraph("Просим Вас осуществить платежи в соответствии с условиями вышеназванной спецификации к договору. Счёт приложен к настоящему письму.");

            #endregion
        }
    }
}