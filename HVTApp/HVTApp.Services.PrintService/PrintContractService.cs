using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintContractService : PrintServiceBase, IPrintOfferContract
    {
        private readonly PrintProductService _printProductService;

        public PrintContractService(IUnityContainer container) : base(container)
        {
            _printProductService = container.Resolve<IPrintProductService>() as PrintProductService;
        }

        public void PrintContract(Guid contractId)
        {
            
        }

        public void PrintSpecification(Guid specificationId)
        {
            var specification = Container.Resolve<IUnitOfWork>().Repository<Specification>().GetById(specificationId);
            var offerUnitsGroups = GetUnitsGroupsGroupByFacilities(specification);
            var unitsGroupsByFacilities = offerUnitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.Facility).ToList();

            //полный путь к файлу (с именем файла)
            var fullPath = GetPath(specification.Contract.Number);

            var docWriter = GetWordDocumentWriter(fullPath);
            if (docWriter == null) return;
            docWriter.StartDocument();

            #region Print Header

            docWriter.StartParagraph();
            docWriter.AddInlinePicture(GetImage("header.jpg"));
            docWriter.EndParagraph();

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            var noBordersTableBorderProperties = docWriter.CreateTableBorderProperties();
            noBordersTableBorderProperties.Style = TableBorderStyle.None;
            noBordersTableBorderProperties.Sides = TableBorderSides.None;

            var headTableProperties = GetTableProperties(docWriter, noBordersTableBorderProperties);
            headTableProperties.Alignment = ParagraphAlignment.Left;
            headTableProperties.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(2, headTableProperties);

            //таблица с номером ТКП
            docWriter.StartTableRow();
            TableCellProperties tableCellProperties1 = docWriter.CreateTableCellProperties();
            tableCellProperties1.PreferredWidthAsPercentage = 50;
            docWriter.StartTableCell(tableCellProperties1);
            docWriter.StartTable(4, headTableProperties);

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "исх.№ ",
                $"{specification.RegNumber}",
                "от",
                $"{specification.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "на № ",
                specification.RequestDocument == null ? string.Empty : $"{specification.RequestDocument.RegNumber}",
                "от",
                specification.RequestDocument == null ? string.Empty : $"{specification.RequestDocument.Date.ToShortDateString()} г.");

            docWriter.EndTable();
            docWriter.EndTableCell();

            var recipient = specification.RecipientEmployee;
            docWriter.PrintTableCell("Получатель: " + Environment.NewLine + $"{recipient.Position} {recipient.Company.Form.ShortName} \"{recipient.Company.ShortName}\"" + Environment.NewLine + $"{recipient.Person}");

            docWriter.EndTableRow();

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                $"О предложении поставки оборудования для нужд объектов: {offerUnitsGroups.Select(x => x.Facility.ToString()).Distinct().OrderBy(x => x).ToStringEnum(", ")}", string.Empty);

            docWriter.EndTable();

            #endregion

            #region Print Text Above Table

            docWriter.PrintParagraph(string.Empty);

            var paraFormat = docWriter.CreateParagraphProperties();
            paraFormat.Alignment = ParagraphAlignment.Center;
            docWriter.PrintParagraph($"Спецификация №{specification.Number} от {specification.Date.ToShortDateString()} г.", paraFormat);

            var paraFormat1 = docWriter.CreateParagraphProperties();
            paraFormat1.Alignment = ParagraphAlignment.Both;
            Company c1 = GlobalAppProperties.Actual.OurCompany;
            Company c2 = specification.Contract.Contragent;
            docWriter.PrintParagraph($"{c1.Form.FullName} «{c1.FullName}» (сокращенное наименование {c1.Form.ShortName} «{c1.ShortName}»), именуемое в дальнейшем Поставщик, в лице генерального директора Владимира Николаевича Калаущенко, действующего на основании устава, с одной стороны, и {c1.Form.FullName} «{c1.FullName}» (сокращенное наименование {c2.Form.ShortName} «{c2.ShortName}»), именуемое в дальнейшем Покупатель, в лице Генерального директора Козлова Алексея Ивановича, действующего на основании устава, с другой стороны, вместе именуемые Стороны, по отдельности Сторона, заключили настоящую спецификацию к договору поставки от {specification.Contract.Date.ToLongDateString()} {specification.Contract.Number} (далее - спецификация) о нижеследующем:", paraFormat1);

            #endregion

            #region Print Main Table

            var printComments = false;
            if (offerUnitsGroups.Any(x => string.IsNullOrWhiteSpace(x.Comment) == false))
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

            foreach (var offerUnitsGroupsByFacility in unitsGroupsByFacilities)
            {
                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = columnsCount;
                docWriter.PrintTableCell($"{offerUnitsGroupsByFacility.Key}", tableCellProperties, font: fontBold); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
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
            var sum = offerUnitsGroups.Sum(x => x.Amount * x.Cost);

            tableCellProperties.BackColor = colorTableHeader;

            PrintSumTableString("Итого без НДС:", sum, docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);
            PrintSumTableString($"НДС ({specification.Vat} %):", sum * specification.Vat / 100, docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);
            PrintSumTableString("Итого с НДС:", sum * (1 + specification.Vat / 100), docWriter, tableCellProperties, fontBold, parPropRight, columnsCount);

            docWriter.EndTable();

            #endregion

            #region Print Conditions

            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph("Условия поставки и оплаты оборудования:", paragraphProperties, fontBold);

            var conditions = new List<string>
            {
                GetShipmentConditions(offerUnitsGroups),
                PrintPaymentConditions("Условия оплаты:", offerUnitsGroups.GroupBy(x => x.PaymentConditionSet), specification.ValidityDate),
                PrintConditions("Срок производства (календарных дней, с правом досрочной поставки):", offerUnitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.ProductionTerm)),
                $"Настоящее предложение действительно при заключении контракта до {specification.ValidityDate.ToShortDateString()} года.",
                "Комплектация и характеристики оборудования в соответствии с техническим приложением к настоящему предложению.",
                "В случае изменения технических характеристик оборудования, объёма поставки или сроков заключения контракта условия предложения могут быть пересмотрены.",
            };

            docWriter.StartTable(2, GetTableProperties(docWriter, noBordersTableBorderProperties));

            var nn = 1;
            foreach (var condition in conditions)
            {
                docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                    $"{nn++}.", condition);
            }

            docWriter.EndTable();

            #endregion

            #region Sender

            docWriter.PrintParagraph(string.Empty);

            var bordProps = docWriter.CreateTableBorderProperties();
            bordProps.Style = TableBorderStyle.None;
            bordProps.Sides = TableBorderSides.None;

            TableProperties tableProperties2 = GetTableProperties(docWriter, bordProps);
            tableProperties2.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(3, tableProperties2);

            TableCellProperties tableCellProperties2 = docWriter.CreateTableCellProperties();
            tableCellProperties2.PreferredWidthAsPercentage = 33;

            docWriter.StartTableRow();

            var part1 = "С уважением," + Environment.NewLine + $"{specification.SenderEmployee.Position}";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            //подпись
            if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer?.Id)
            {
                var drt = MessageService.ConfirmationDialog("Подпись", "Печать с подписью?", defaultNo: true);
                if (drt)
                {
                    try
                    {
                        docWriter.StartTableCell(tableCellProperties2);

                        var prpr = docWriter.CreateParagraphProperties();
                        prpr.Alignment = ParagraphAlignment.Center;
                        docWriter.StartParagraph(prpr);
                        docWriter.AddInlinePicture(GetImage("sign_deev.png"));
                        docWriter.EndParagraph();

                        docWriter.EndTableCell();

                    }
                    catch (Exception e)
                    {
                        MessageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
                        docWriter.PrintTableCell(string.Empty);
                    }
                }
                else
                {
                    docWriter.PrintTableCell(string.Empty);
                }
            }
            else
            {
                docWriter.PrintTableCell(string.Empty);
            }

            //ФИО подписывающего лица
            var part3 = Environment.NewLine + $"{specification.SenderEmployee.Person.Name[0]}.{specification.SenderEmployee.Person.Patronymic?[0]}.{specification.SenderEmployee.Person.Surname}";
            var pp = docWriter.CreateParagraphProperties();
            pp.Alignment = ParagraphAlignment.Right;
            docWriter.PrintTableCell(part3, tableCellProperties2, pp);

            docWriter.EndTableRow();

            //docWriter.PrintTableRow(
            //    tableCellProperties2,
            //    docWriter.CreateTableRowProperties(),
            //    docWriter.CreateParagraphProperties(),
            //    docWriter.CreateFont(),
            //    "С уважением," + Environment.NewLine + $"{offer.SenderEmployee.Position}",
            //    string.Empty,
            //    Environment.NewLine + $"{offer.SenderEmployee.Person.Name[0]}.{offer.SenderEmployee.Person.Patronymic?[0]}.{offer.SenderEmployee.Person.Surname}");

            docWriter.EndTable();

            #endregion

            #region Print Technical Details

            paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.PageBreakBefore = true;

            docWriter.PrintParagraph($"Техническое приложение к предложению исх.№ {specification.RegNumber} от {specification.Date.ToShortDateString()} г.", paragraphProperties, fontBold);
            docWriter.PrintParagraph("Технические характеристики оборудования:");
            foreach (var offerUnitsGroupsByFacility in unitsGroupsByFacilities)
            {
                foreach (var offerUnitsGroup in offerUnitsGroupsByFacility)
                {
                    docWriter.PrintParagraph(Environment.NewLine + $"{offerUnitsGroup.Position}. {offerUnitsGroup.Product} - {offerUnitsGroup.Amount} шт.:");
                    _printProductService.Print(docWriter, offerUnitsGroup.Product);

                    // включенное в состав оборудование
                    if (offerUnitsGroup.ProductsIncluded.Any())
                    {
                        docWriter.PrintParagraph(Environment.NewLine + "Дополнительное оборудование и услуги, включенные в состав:");

                        int n = 1;
                        var productsIncluded = offerUnitsGroup.ProductsIncluded.GroupBy(x => new
                        {
                            x.Product,
                            x.Amount
                        });
                        foreach (var productIncluded in productsIncluded)
                        {
                            docWriter.PrintParagraph(Environment.NewLine + $"{offerUnitsGroup.Position}.{n++} {productIncluded.Key.Product} - {productIncluded.Count() * productIncluded.Key.Amount} шт.:");
                            _printProductService.Print(docWriter, productIncluded.Key.Product);
                        }
                    }
                }
            }

            #endregion

            #region Author Footer

            var parts = SectionHeaderFooterParts.FooterAllPages;
            var writerSet = docWriter.AddSectionHeaderFooter(parts);
            writerSet.FooterWriterAllPages.Open();

            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun("Исполнитель:" + Environment.NewLine + $"{specification.Author}" + Environment.NewLine + $"тел.: {specification.Author.PhoneNumber}; e-mail: {specification.Author.Email}; ");
            writerSet.FooterWriterAllPages.AddHyperlink(@"http://www.uetm.ru", "uetm.ru");
            writerSet.FooterWriterAllPages.AddTextRun(Environment.NewLine + $"{specification.RegNumber} от {specification.Date.ToShortDateString()} г. - стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();

            writerSet.FooterWriterAllPages.Close();

            #endregion

            docWriter.EndDocument();
            docWriter.Close();

            OpenDocument(fullPath);
        }


        private static void PrintSumTableString(string text, double sum, WordDocumentWriter docWriter, TableCellProperties tableCellProperties,
            Font font, ParagraphProperties parProp, int columnsCount)
        {
            docWriter.StartTableRow();
            tableCellProperties.ColumnSpan = columnsCount - 1;
            docWriter.PrintTableCell(text, tableCellProperties, null, font);
            tableCellProperties.ColumnSpan = 1;
            docWriter.PrintTableCell($"{sum:N}", tableCellProperties, parProp, font);
            docWriter.EndTableRow();
        }


        private static string GetPath(string fileName)
        {
            //удаляем некорректные символы
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";

            //возвращаем путь
            return Path.GetTempPath() + $"\\{fileName}";
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