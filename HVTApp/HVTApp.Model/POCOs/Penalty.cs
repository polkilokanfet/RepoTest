using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// �������� �������
    /// </summary>
    [Designation("�������� �������")]
    public class Penalty : BaseEntity
    {
        [Designation("% �� ���� ���������"), Required]
        public double PercentPerDay { get; set; } = 0.0001;

        [Designation("����������� ������"), Required]
        public double PercentLimit { get; set; } = 1;

        [Designation("���������� ���������� ������")]
        public double PenaltyPaid { get; set; } = 0;
    }
}