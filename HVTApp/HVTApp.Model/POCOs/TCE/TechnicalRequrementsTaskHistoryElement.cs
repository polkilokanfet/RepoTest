using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ���.������� (������)")]
    public class TechnicalRequrementsTaskHistoryElement : BaseEntity
    {
        public virtual Guid TechnicalRequrementsTaskId { get; set; }

        [Designation("������"), Required, OrderStatus(90)]
        public DateTime Moment { get; set; } = DateTime.Now;

        [Designation("���"), Required, OrderStatus(80)]
        public TechnicalRequrementsTaskHistoryElementType Type { get; set; } = TechnicalRequrementsTaskHistoryElementType.Create;

        [Designation("�����������"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; } = string.Empty;
    }

    public enum TechnicalRequrementsTaskHistoryElementType
    {
        /// <summary>
        /// �������
        /// </summary>
        Create,

        /// <summary>
        /// ����������
        /// </summary>
        Start,

        //���������
        Finish,

        /// <summary>
        /// ��������� �������������
        /// </summary>
        Reject,

        /// <summary>
        /// ��������� ���������������
        /// </summary>
        RejectByFrontManager,

        /// <summary>
        /// ����������� ��
        /// </summary>
        Stop,

        /// <summary>
        /// ��������
        /// </summary>
        Instruct,

        /// <summary>
        /// �������
        /// </summary>
        Accept
    }
}