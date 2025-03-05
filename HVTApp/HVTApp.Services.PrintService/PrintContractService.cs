using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class PrintContractService : PrintUnitsServiceBase, IPrintContract
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
            var unitsGroups = GetUnitsGroups(specification.SalesUnits);
            var unitsGroupsByFacilities = unitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.Facility).ToList();

            //полный путь к файлу (с именем файла)
            var fullPath = GetPath(specification.Contract.Number);

            var docWriter = GetWordDocumentWriter(fullPath);
            if (docWriter == null) return;
            docWriter.StartDocument();

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

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            PrintUnitsTable(unitsGroups, docWriter, fontBold, unitsGroupsByFacilities, specification);

            #endregion

            #region Print Conditions

            var paragraphProperties = docWriter.CreateParagraphProperties();
            paragraphProperties.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph("Условия поставки и оплаты оборудования:", paragraphProperties, fontBold);

            var conditions = new List<string>
            {
                GetShipmentConditions(unitsGroups),
                PrintPaymentConditions("Условия оплаты:", unitsGroups.GroupBy(x => x.PaymentConditionSet), specification.Date.AddDays(14)),
                PrintConditions("Срок производства (календарных дней, с правом досрочной поставки):", unitsGroups.GroupBy(offerUnitsGroup => offerUnitsGroup.ProductionTerm)),
                "Комплектация и характеристики оборудования в соответствии с техническим приложением к настоящему предложению.",
                "В случае изменения технических характеристик оборудования, объёма поставки или сроков заключения контракта условия предложения могут быть пересмотрены.",
            };


            var noBordersTableBorderProperties = docWriter.CreateTableBorderProperties();
            noBordersTableBorderProperties.Style = TableBorderStyle.None;
            noBordersTableBorderProperties.Sides = TableBorderSides.None;

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

            var part1 = "Поставщик" + Environment.NewLine + "______ В.Н. Калаущенко";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            docWriter.PrintTableCell(string.Empty);

            //ФИО подписывающего лица
            var part3 = "Покупатель" + Environment.NewLine + "______ В.Н. Калаущенко";
            docWriter.PrintTableCell(part3, tableCellProperties2);

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

            docWriter.PrintParagraph($"Техническое приложение к спецификации №{specification.Number} от {specification.Date.ToShortDateString()} г.", paragraphProperties, fontBold);
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
            writerSet.FooterWriterAllPages.AddTextRun($"Спецификация №{specification.Number} к договору {specification.Contract.Number}" + Environment.NewLine + "стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();

            writerSet.FooterWriterAllPages.Close();

            #endregion

            docWriter.EndDocument();
            docWriter.Close();

            OpenDocument(fullPath);
        }

        private static string GetPath(string fileName)
        {
            //удаляем некорректные символы
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";

            //возвращаем путь
            return Path.GetTempPath() + $"\\{fileName}";
        }

    }
}