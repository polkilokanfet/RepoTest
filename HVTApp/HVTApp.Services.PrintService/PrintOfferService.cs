using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
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

        public void PrintOffer(Guid offerId)
        {

            #region Get OfferUnits

            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var offerUnits = unitOfWork.Repository<OfferUnit>().GetAll().Where(x => x.Offer.Id == offerId).ToList();
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


            var offerDocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\OfferDocument.docx";
            var docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.DefaultParagraphProperties.Alignment = ParagraphAlignment.Both;
            docWriter.StartDocument();

            #region Print Header

            //BitmapSource headerBitmapSource = GetImage("header.jpg");
            //AnchoredPicture headerPicture = docWriter.CreateAnchoredPicture(headerBitmapSource);
            //docWriter.StartParagraph();
            //docWriter.AddAnchoredPicture(headerPicture);
            //docWriter.EndParagraph();

            docWriter.PrintParagraph($"Дата: {offer.Date.ToShortDateString()} исх.№ {offer.RegNumber}");
            docWriter.PrintParagraph($"Получатель: {offer.RecipientEmployee.Position.Name} {offer.RecipientEmployee.Company.Form.ShortName} \"{offer.RecipientEmployee.Company.ShortName}\" { offer.RecipientEmployee.Person}");

            docWriter.PrintParagraph(string.Empty);
            docWriter.PrintParagraph(offer.RecipientEmployee.Person.IsMan
                ? $"Уважаемый {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}!"
                : $"Уважаемая {offer.RecipientEmployee.Person.Name} {offer.RecipientEmployee.Person.Patronymic}!");
            docWriter.PrintParagraph($"В ответ на Ваш запрос, предоставляем технико-коммерческое предложение на поставку электротехнического оборудования для нужд {offerUnits.Select(x => x.Facility).ToStringEnum()} (по проекту \"{offer.Project.Name}\").");
            docWriter.PrintParagraph("Стоимость оборудования приведена в таблице:");

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

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold, 
                "№", "Тип оборудования", "Обозначение", "Кол.", "Стоимость, руб.", "Сумма, руб.");

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
                docWriter.PrintTableCell($"{offerUnitsGroupsByFacility.Key}", tableCellProperties, font: fontBold); //объект

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
            docWriter.PrintParagraph(string.Empty);
            docWriter.PrintParagraph("Условия поставки оборудования:", paragraphProperties, fontBold);
            PrintConditions("1. Условия оплаты:", offerUnitsGroups.GroupBy(x => x.PaymentConditionSet.Model), docWriter);
            PrintConditions("2. Срок производства (календарных дней, с правом досрочной поставки):", offerUnitsGroups.GroupBy(x => x.ProductionTerm), docWriter);
            docWriter.PrintParagraph($"3. Точный срок поставки оборудования уточняется при заключении договора.");
            docWriter.PrintParagraph($"4. Цена и сроки поставки могут быть пересмотрены после окончательного согласования опросных листов на оборудование.");
            docWriter.PrintParagraph($"5. Настоящее предложение действительно до {offer.ValidityDate.ToShortDateString()} г.");
            docWriter.PrintParagraph($"6. Заводская гарантия на оборудование - 5 лет.");

            #endregion

            #region Sender

            docWriter.PrintParagraph(string.Empty);

            var bordProps = docWriter.CreateTableBorderProperties();
            bordProps.Style = TableBorderStyle.None;
            bordProps.Sides = TableBorderSides.None;

            docWriter.StartTable(3, GetTableProperties(docWriter, bordProps));

            docWriter.PrintTableRow(
                docWriter.CreateTableCellProperties(),
                docWriter.CreateTableRowProperties(),
                docWriter.CreateParagraphProperties(),
                docWriter.CreateFont(),
                $"С уважением, {offer.SenderEmployee.Position}",
                $"                     ",
                $"{offer.SenderEmployee.Person}");

            docWriter.EndTable();

            #endregion

            #region Author

            docWriter.PrintParagraph(string.Empty);
            docWriter.PrintParagraph($"Исполнитель: {offer.Author} тел.: {offer.Author.PhoneNumber}; e-mail: {offer.Author.Email}");

            #endregion

            #region Print Technical Details

            paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            docWriter.PrintParagraph($"Техническое приложение к предложению исх.№ {offer.RegNumber} от {offer.Date.ToShortDateString()} г.", paragraphProperties, fontBold);
            docWriter.PrintParagraph("Технические характеристики оборудования:");
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
            if (offerUnitsGroupsGrouped.Count() == 1)
            {
                docWriter.PrintParagraph($"{text} {offerUnitsGroupsGrouped.First().Key}.");
            }
            else
            {
                foreach (var unitsGroups in offerUnitsGroupsGrouped)
                {
                    var sb = new StringBuilder();
                    unitsGroups.ForEach(x => sb.Append($"{x.Position}, "));
                    var positions = sb.Remove(sb.Length - 2, 2);
                    docWriter.PrintParagraph($"{text} позиций {positions}: {unitsGroups.Key}.");
                }
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

        #region GetImage
        private BitmapSource GetImage(string resourceName)
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/HVTApp.Services.PrintService;component/Images/" + resourceName));
        }
        #endregion GetImage
    }
}