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
    /// ���.������� (������)
    /// </summary>
    [Designation("���.������� (������)")]
    [DesignationPlural("���.������� (������)")]
    public partial class TechnicalRequrementsTask : BaseEntity
    {
        [Designation("������ ����������"), Required, OrderStatus(20)]
        public virtual List<TechnicalRequrements> Requrements { get; set; } = new List<TechnicalRequrements>();

        [Designation("����� � ���"), MaxLength(10), OrderStatus(4)]
        public string TceNumber { get; set; }

        [Designation("Back manager"), OrderStatus(1)]
        public virtual User BackManager { get; set; }


        [Designation("��������� �������� back-����������"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenBackManagerMoment { get; set; }

        [Designation("��������� �������� front-����������"), OrderStatus(1), NotForListView]
        public virtual DateTime? LastOpenFrontManagerMoment { get; set; }


        [Designation("������������� ���"), OrderStatus(-4)]
        public bool LogisticsCalculationRequired { get; set; } = false;

        [Designation("������������� �����-������� ��"), OrderStatus(-5)]
        public bool ExcelFileIsRequired { get; set; } = true;


        [Designation("������� �������������"), OrderStatus(-10)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [Designation("�����-������ ���"), OrderStatus(-6)]
        public virtual List<AnswerFileTce> AnswerFiles { get; set; } = new List<AnswerFileTce>();

        [Designation("����� ���")]
        public virtual List<ShippingCostFile> ShippingCostFiles { get; set; } = new List<ShippingCostFile>();

        [Designation("������� ����������")]
        public virtual List<TechnicalRequrementsTaskHistoryElement> HistoryElements { get; set; } = new List<TechnicalRequrementsTaskHistoryElement>();

        /// <summary>
        /// ��������� ���� ��������� ����������
        /// </summary>
        [Designation("����������� ��"), OrderStatus(1)]
        public virtual DateTime? DesiredFinishDate { get; set; }

        [Designation("Front manager"), OrderStatus(-10), NotMapped]
        public User FrontManager
        {
            get
            {
                return Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).FirstOrDefault()?.Project.Manager;
            }
        }

        [Designation("�����"), OrderStatus(3), NotMapped]
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

        [Designation("�����"), OrderStatus(2), NotMapped]
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
        /// ��������� ������ � ������� ����������
        /// </summary>
        public TechnicalRequrementsTaskHistoryElement LastHistoryElement => HistoryElements.OrderBy(historyElement => historyElement.Moment).LastOrDefault();

        /// <summary>
        /// ������� ����������
        /// </summary>
        [Designation("����������?")]
        public bool IsStarted => LastHistoryElement != null &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Create &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Reject &&
                                 LastHistoryElement.Type != TechnicalRequrementsTaskHistoryElementType.Stop;

        /// <summary>
        /// ������� ����������� ��
        /// </summary>
        [Designation("���������?")]
        public bool IsFinished => LastHistoryElement != null &&
                                  (LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Finish ||
                                   LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Accept);

        /// <summary>
        /// ������� ��������� ��
        /// </summary>
        [Designation("���������?")]
        public bool IsRejected => LastHistoryElement != null && 
                                  LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Reject;

        /// <summary>
        /// ������� ����������� ��
        /// </summary>
        [Designation("�����������?")]
        public bool IsStopped => LastHistoryElement != null &&
                                 LastHistoryElement.Type == TechnicalRequrementsTaskHistoryElementType.Stop;

        /// <summary>
        /// ������� ������� �� � ��
        /// </summary>
        [Designation("�������?")]
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
            return $"������ TCE: {projectName} (Id: {Id}).";
        }
    }
}