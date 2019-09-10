using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ����������� ����� (��� � �.�.)
    /// </summary>
    [Designation("����������� �����")]
    [DesignationPlural("����������� �����")]
    public partial class CountryUnion : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(50), OrderStatus(2)]
        public string Name { get; set; }

        [Designation("������ �����������"), Required, OrderStatus(1)]
        public virtual List<Country> Countries { get; set; } = new List<Country>();

        public override string ToString()
        {
            return Name;
        }
    }
}