using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ���.������� (������)
    /// </summary>
    [Designation("���.������� (������)")]
    [DesignationPlural("���.������� (������)")]
    public partial class TechnicalRequrementsTask : BaseEntity
    {
        [Designation("������ ����������"), Required, OrderStatus(20)]
        public virtual List<TechnicalRequrements> Requrements { get; set; } = new List<TechnicalRequrements>();

        [Designation("����������� front-���������"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }

        [Designation("����������� ��������������"), MaxLength(250), OrderStatus(5)]
        public string CommentBackOfficeBoss { get; set; }

        [Designation("����� � ���"), MaxLength(10), OrderStatus(4)]
        public string TceNumber { get; set; }

        [Designation("Back manager"), OrderStatus(1)]
        public virtual User BackManager { get; set; }

        [Designation("�����"), OrderStatus(3)]
        public virtual DateTime? Start { get; set; }

        [Designation("��������� �������� back-����������"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenBackManagerMoment { get; set; }

        [Designation("��������� �������� front-����������"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenFrontManagerMoment { get; set; }

        [Designation("������ �����"), OrderStatus(1), NotForListView]
        public virtual DateTime? FirstStartMoment { get; set; }

        [Designation("����������"), OrderStatus(-1), NotForListView]
        public virtual DateTime? RejectByBackManagerMoment { get; set; }

        [Designation("����������� �� ����������"), MaxLength(250), OrderStatus(-5), NotForListView]
        public string RejectComment { get; set; }

        [Designation("������� �������������"), OrderStatus(-10)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [Designation("�����-������ ���"), OrderStatus(-6)]
        public virtual List<AnswerFileTce> AnswerFiles { get; set; } = new List<AnswerFileTce>();

        [Designation("������������� ���"), OrderStatus(-4)]
        public bool LogisticsCalculationRequired { get; set; } = false;

        [Designation("������������� �����-������� ��"), OrderStatus(-5)]
        public bool ExcelFileIsRequired { get; set; } = true;

        [Designation("������� ����������")]
        public virtual List<TechnicalRequrementsTaskHistoryElement> HistoryElements { get; set; } = new List<TechnicalRequrementsTaskHistoryElement>();

        [Designation("����� ���")]
        public virtual List<ShippingCostFile> ShippingCostFiles { get; set; } = new List<ShippingCostFile>();
    }
}