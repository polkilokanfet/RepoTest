using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Comparers;
using HVTApp.UI.Groups;
using Infragistics.Documents.Word;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintOfferService : IPrintOfferService
    {
        private readonly IUnityContainer _container;
        private readonly PrintProductService _printProductService;

        public PrintOfferService(IUnityContainer container)
        {
            _container = container;
            _printProductService = container.Resolve<IPrintProductService>() as PrintProductService;
        }

        public async Task PrintOfferAsync(Guid offerId)
        {

            #region Get OfferUnits

            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var offerUnits = (await unitOfWork.Repository<OfferUnit>().GetAllAsync()).Where(x => x.Offer.Id == offerId).ToList();
            var offer = offerUnits.First().Offer;

            //разбиваем на группы, а их делим по объектам
            var offerUnitsGroupsByFacilities = offerUnits
                .GroupBy(x => x, new OfferUnitsGroupsComparer())
                .Select(x => new OfferUnitsGroup(x))
                .OrderByDescending(x => x.Total)
                .GroupBy(x => x.Facility.Model)
                .ToList();

            var offerUnitsGroups = offerUnitsGroupsByFacilities.SelectMany(x => x.ToList()).ToList();

            //назначаем позиции ТКП
            var i = 1;
            offerUnitsGroups.ForEach(x => x.Position = i++);

            #endregion

            var offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestOfferDocument.docx";
            var docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();

            #region Print Header

            docWriter.PrintParagraph("Получатель");
            docWriter.PrintParagraph($"должность: {offer.RecipientEmployee.Position.Name}");
            docWriter.PrintParagraph($"компания: {offer.RecipientEmployee.Company}");
            docWriter.PrintParagraph($"Ф.И.О.: {offer.RecipientEmployee.Person.Surname} {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}");

            docWriter.PrintParagraph($"Проект: \"{offer.Project.Name}\"");
            docWriter.PrintParagraph($"Срок действия ТКП: {offer.ValidityDate.ToShortDateString()}");

            #endregion

            #region Print Main Table

            var colorTableHeader = Colors.AliceBlue;

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            docWriter.StartTable(6, GetTableProperties(docWriter, tableBorderProperties));

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold, "№",
                "Тип оборудования", "Обозначение", "Кол.", "Стоимость, руб.", "Сумма, руб.");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = 6;
                docWriter.PrintTableCell($"{offerUnitsGroupsByFacility.Key}", tableCellProperties); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.StartTableRow();

                    docWriter.PrintTableCell(offerUnitsGroup.Position.ToString(), tableCellProperties); //номер строки ТКП
                    docWriter.PrintTableCell(offerUnitsGroup.Product.ProductType?.Name, tableCellProperties);
                    //тип оборудования
                    docWriter.PrintTableCell(offerUnitsGroup.Product.Designation, tableCellProperties); //обозначение
                    docWriter.PrintTableCell($"{offerUnitsGroup.Amount:D}", tableCellProperties, parPropRight); //колличество
                    docWriter.PrintTableCell($"{offerUnitsGroup.Cost:N}", tableCellProperties, parPropRight); //стоимость
                    docWriter.PrintTableCell($"{offerUnitsGroup.Total:N}", tableCellProperties, parPropRight); //сумма

                    docWriter.EndTableRow();
                }
            }

            //сумма ТКП
            var sum = offerUnits.Sum(x => x.Cost);

            tableCellProperties.BackColor = colorTableHeader;

            PrintSumTableString("Итого без НДС:", sum, docWriter, tableCellProperties, fontBold, parPropRight);
            PrintSumTableString($"НДС ({offer.Vat} %):", sum * offer.Vat / 100, docWriter, tableCellProperties, fontBold, parPropRight);
            PrintSumTableString("Итого с НДС:", sum * (1 + offer.Vat / 100), docWriter, tableCellProperties, fontBold, parPropRight);

            docWriter.EndTable();

            #endregion

            #region Print Conditions

            var paragraphProperties = docWriter.CreateParagraphProperties();
            docWriter.PrintParagraph("Условия поставки оборудования:", paragraphProperties, fontBold);
            docWriter.PrintParagraph(string.Empty);

            PrintConditions("Оплата", offerUnitsGroups.GroupBy(x => x.PaymentConditionSet.Model), docWriter);
            docWriter.PrintParagraph(string.Empty);
            PrintConditions("Срок производства (календарных дней)", offerUnitsGroups.GroupBy(x => x.ProductionTerm), docWriter);

            #endregion

            #region Print Technical Details

            paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            docWriter.PrintParagraph("Технические характеристики оборудования:", paragraphProperties, fontBold);
            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.PrintParagraph($"{offerUnitsGroup.Position}. {offerUnitsGroup.Product} {offerUnitsGroupsByFacility.Count()} шт.:");
                    _printProductService.Print(docWriter, offerUnitsGroup.Product.Model);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph("Дополнительное оборудование, включенное в состав:");
                        foreach (var productIncluded in offerUnitsGroup.ProductsIncluded)
                        {
                            docWriter.PrintParagraph($"{productIncluded.Product} = {productIncluded.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Product.Model);
                        }
                    }

                    docWriter.PrintParagraph(Environment.NewLine);
                }
            }

            #endregion

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);

        }

        private static void PrintConditions<T>(string text, IEnumerable<IGrouping<T, OfferUnitsGroup>> offerUnitsGroupsGrouped, WordDocumentWriter docWriter)
        {
            docWriter.PrintParagraph(text);
            foreach (var offerUnitsGroupsByPaymentCondition in offerUnitsGroupsGrouped)
            {
                var stringBuilder = new StringBuilder();
                offerUnitsGroupsByPaymentCondition.ForEach(x => stringBuilder.Append($"{x.Position}, "));
                var positions = stringBuilder.Remove(stringBuilder.Length - 2, 2);
                docWriter.PrintParagraph($"Для позиций {positions}: {offerUnitsGroupsByPaymentCondition.Key}");
            }
        }

        private static void PrintSumTableString(string text, double sum, WordDocumentWriter docWriter, TableCellProperties tableCellProperties,
            Font font, ParagraphProperties parProp)
        {
            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = 5;
            docWriter.PrintTableCell(text, tableCellProperties, null, font);
            tableCellProperties.ColumnSpan = 1;
            docWriter.PrintTableCell($"{sum:N}", tableCellProperties, parProp, font);
            docWriter.EndTableRow();
        }

        /// <summary>
        /// Печать технических деталей ТКП
        /// </summary>
        /// <param name="docWriter"></param>
        /// <param name="offerUnitsGroupsByFacilities"></param>
        private void PrintTechnicalDetails(WordDocumentWriter docWriter, List<IGrouping<Facility, OfferUnitsGroup>> offerUnitsGroupsByFacilities)
        {
            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            Font fontHeader = docWriter.CreateFont();
            fontHeader.Bold = true;

            docWriter.PrintParagraph("Технические характеристики оборудования(в соответствии с позициями таблицы):", paragraphProperties, fontHeader);
            int positionNumber = 1;
            foreach (var offerUnitsGroupsByFacility in offerUnitsGroupsByFacilities)
            {
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.PrintParagraph($"{positionNumber++}. {offerUnitsGroup.Product} = {offerUnitsGroupsByFacility.Count()} шт.:");
                    _printProductService.Print(docWriter, offerUnitsGroup.Product.Model);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph("Дополнительное оборудование, включенное в состав:");
                        foreach (var productIncluded in offerUnitsGroup.ProductsIncluded)
                        {
                            docWriter.PrintParagraph($"{productIncluded.Product} {productIncluded.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Product.Model);
                        }
                    }

                    docWriter.PrintParagraph(Environment.NewLine);
                }
            }
        }


        private TableBorderProperties GetTableBorderProperties(WordDocumentWriter docWriter)
        {
            var borderProps = docWriter.CreateTableBorderProperties();
            borderProps.Color = Colors.Black;
            borderProps.Style = TableBorderStyle.Single;
            return borderProps;
        }

        private TableProperties GetTableProperties(WordDocumentWriter docWriter, TableBorderProperties borderProps)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Both;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;
            return tableProps;
        }
    }
}