using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��������� ��������")]
    public class ProductCategory : BaseEntity
    {
        [Designation("�������� ������"), Required, MaxLength(150), OrderStatus(90)]
        public string NameFull { get; set; }

        [Designation("�������� �����������"), Required, MaxLength(30), OrderStatus(80)]
        public string NameShort { get; set; }

        [Designation("���������"), Required, OrderStatus(50)]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();


        /// <summary>
        /// �������� �� ��� ��������� ��������� (��� ��������� ��� ��������� �� ����������)
        /// </summary>
        [NotMapped, NotForListView, NotForDetailsView, NotForWrapper]
        public bool IsStub { get; set; } = false;

        public override string ToString()
        {
            return NameShort;
        }
    }
}