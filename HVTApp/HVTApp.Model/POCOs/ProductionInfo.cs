using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    public partial class ProductionInfo : BaseEntity
    {
        [Designation("Id ��� (��� ������������)")]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("�����")]
        public virtual Order Order { get; set; }

        [Designation("�������"), MaxLength(4)]
        public string OrderPosition { get; set; }

        [Designation("�����"), MaxLength(10)]
        public string SerialNumber { get; set; }

        [Designation("���� ������")]
        public int? AssembleTerm { get; set; }

        [Designation("������ ��������� � ������������")]
        public DateTime SignalToStartProduction { get; set; } = DateTime.Now;

        [Designation("���� ���������� � ������������")]
        public DateTime? SignalToStartProductionDone { get; set; }

        [Designation("���� ������ ������������")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("���� ������������")]
        public DateTime? PickingDate { get; set; }

        [Designation("�������� ���� ��������� ������������")]
        public DateTime? EndProductionPlanDate { get; set; }

        [Designation("���� ��������� ������������")]
        public DateTime? EndProductionDate { get; set; }


        public override string ToString()
        {
            return this.Order != null 
                ? $"�/� {this.Order.Number}, ���.�{this.SerialNumber}" 
                : "����� �� ������";
        }
    }
}