using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Тех.задание (задача)
    /// </summary>
    [Designation("Тех.задание (задача)")]
    [DesignationPlural("Тех.задания (задача)")]
    public partial class TechnicalRequrementsTask : BaseEntity
    {
        [Designation("Список требований"), Required, OrderStatus(20)]
        public virtual List<TechnicalRequrements> Requrements { get; set; } = new List<TechnicalRequrements>();

        [Designation("Номер в ТСЕ"), MaxLength(10), OrderStatus(4)]
        public string TceNumber { get; set; }

        [Designation("Back manager"), OrderStatus(1)]
        public virtual User BackManager { get; set; }


        [Designation("Последний просмотр back-менеджером"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenBackManagerMoment { get; set; }

        [Designation("Последний просмотр front-менеджером"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenFrontManagerMoment { get; set; }


        [Designation("Необходимость РТЗ"), OrderStatus(-4)]
        public bool LogisticsCalculationRequired { get; set; } = false;

        [Designation("Необходимость файла-расчета ПЗ"), OrderStatus(-5)]
        public bool ExcelFileIsRequired { get; set; } = true;


        [Designation("Расчеты себестоимости"), OrderStatus(-10)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [Designation("Файлы-ответы ОГК"), OrderStatus(-6)]
        public virtual List<AnswerFileTce> AnswerFiles { get; set; } = new List<AnswerFileTce>();

        [Designation("Файлы РТЗ")]
        public virtual List<ShippingCostFile> ShippingCostFiles { get; set; } = new List<ShippingCostFile>();

        [Designation("История проработки")]
        public virtual List<TechnicalRequrementsTaskHistoryElement> HistoryElements { get; set; } = new List<TechnicalRequrementsTaskHistoryElement>();

        /// <summary>
        /// Требуемая дата окончания проработки
        /// </summary>
        [Designation("Проработать до"), OrderStatus(1)]
        public virtual DateTime? DesiredFinishDate { get; set; }

        [Designation("Front manager"), OrderStatus(-10), NotMapped]
        public User FrontManager
        {
            get
            {
                return Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).FirstOrDefault()?.Project.Manager;
            }
        }

        [Designation("Старт"), OrderStatus(3), NotMapped]
        public DateTime? Start
        {
            get
            {
                if (this.IsStarted == false)
                    return null;

                return this.HistoryElements
                    .Where(historyElement => historyElement.Type == TechnicalRequrementsTaskHistoryElementType.Start)
                    .Max(historyElement => historyElement.Moment);
            }
        }

        [Designation("Финиш"), OrderStatus(2), NotMapped]
        public DateTime? Finish
        {
            get
            {
                if (this.IsFinished == false)
                    return null;

                return this.HistoryElements
                    .Where(historyElement => historyElement.Type == TechnicalRequrementsTaskHistoryElementType.Finish)
                    .Max(historyElement => historyElement.Moment);
            }
        }


        /// <summary>
        /// Последняя запись в истории проработки
        /// </summary>
        public TechnicalRequrementsTaskHistoryElement LastHistoryElement => HistoryElements.OrderBy(historyElement => historyElement.Moment).LastOrDefault();

        /// <summary>
        /// Задание стартовано
        /// </summary>
        [Designation("Стартовано?")]
        public bool IsStarted => LastHistoryElement != null &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Create &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Reject &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Stop;

        /// <summary>
        /// Задание проработано БМ
        /// </summary>
        [Designation("Завершено?")]
        public bool IsFinished => LastHistoryElement != null &&
                                  (LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Finish ||
                                   LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Accept);

        /// <summary>
        /// Задание отклонено БМ
        /// </summary>
        [Designation("Отклонено?")]
        public bool IsRejected => LastHistoryElement != null && 
                                  LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Reject;

        /// <summary>
        /// Задание остановлено ФМ
        /// </summary>
        [Designation("Остановлено?")]
        public bool IsStopped => LastHistoryElement != null &&
                                 LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Stop;

        /// <summary>
        /// Задание принято ФМ у БМ
        /// </summary>
        [Designation("Принято?")]
        public bool IsAccepted=> LastHistoryElement != null &&
                                 LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Accept;

        public string Products
        {
            get
            {
                return Requrements
                    .SelectMany(x => x.SalesUnits)
                    .Select(x => x.Product.Designation)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToStringEnum();
            }
        }

        public override string ToString()
        {
            var projectName = Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).FirstOrDefault()?.Project.Name;
            return $"Задача TCE: {projectName} (Id: {Id}).";
        }
    }
}