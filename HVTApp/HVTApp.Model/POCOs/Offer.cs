using System;
using System.Collections.Generic;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("���")]
    [DesignationPlural("�����������")]
    public class Offer : Document
    {
        [Designation("������")]
        public virtual Project Project { get; set; }

        [Designation("���� ��������")]
        public DateTime ValidityDate { get; set; } // ���� �� ������� ��� �������������.

        [Designation("���")]
        public double Vat { get; set; }
    }
}