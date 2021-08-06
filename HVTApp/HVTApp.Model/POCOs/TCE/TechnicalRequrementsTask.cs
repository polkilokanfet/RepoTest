using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

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

        [Designation("Комментарий front-менеджера"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }

        [Designation("Комментарий распределителя"), MaxLength(250), OrderStatus(5)]
        public string CommentBackOfficeBoss { get; set; }

        [Designation("Номер в ТСЕ"), MaxLength(10), OrderStatus(4)]
        public string TceNumber { get; set; }

        [Designation("Back manager"), OrderStatus(1)]
        public virtual User BackManager { get; set; }

        [Designation("Старт"), OrderStatus(3)]
        public virtual DateTime? Start { get; set; }

        [Designation("Последний просмотр back-менеджером"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenBackManagerMoment { get; set; }

        [Designation("Последний просмотр front-менеджером"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenFrontManagerMoment { get; set; }

        [Designation("Первый старт"), OrderStatus(1), NotForListView]
        public virtual DateTime? FirstStartMoment { get; set; }

        [Designation("Отклонение"), OrderStatus(-1), NotForListView]
        public virtual DateTime? RejectByBackManagerMoment { get; set; }

        [Designation("Комментарий по отклонению"), MaxLength(250), OrderStatus(-5), NotForListView]
        public string RejectComment { get; set; }

        [Designation("Расчеты себестоимости"), OrderStatus(-10)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [Designation("Файлы-ответы ОГК"), OrderStatus(-6)]
        public virtual List<AnswerFileTce> AnswerFiles { get; set; } = new List<AnswerFileTce>();

        [Designation("Необходимость РТЗ"), OrderStatus(-4)]
        public bool LogisticsCalculationRequired { get; set; } = false;

        [Designation("Необходимость файла-расчета ПЗ"), OrderStatus(-5)]
        public bool ExcelFileIsRequired { get; set; } = true;

        [Designation("История проработки")]
        public virtual List<TechnicalRequrementsTaskHistoryElement> HistoryElements { get; set; } = new List<TechnicalRequrementsTaskHistoryElement>();

        [Designation("Файлы РТЗ")]
        public virtual List<ShippingCostFile> ShippingCostFiles { get; set; } = new List<ShippingCostFile>();
    }
}