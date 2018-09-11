using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public partial class Tender : BaseEntity
    {
        [Designation("������")]
        public virtual Project Project { get; set; }

        [Designation("����")]
        public virtual List<TenderType> Types { get; set; } = new List<TenderType>();

        [Designation("��������")]
        public DateTime DateOpen { get; set; }

        [Designation("��������")]
        public DateTime DateClose { get; set; }

        [Designation("�����")]
        public DateTime? DateNotice { get; set; }

        [Designation("���������")]
        public virtual List<Company> Participants { get; set; } = new List<Company>();

        [Designation("����������")]
        public virtual Company Winner { get; set; }

        public override string ToString()
        {
            return $"Tender {Types} of {Project}";
        }
    }

    [Designation("��� �������")]
    [DesignationPlural("���� �������")]
    public partial class TenderType : BaseEntity
    {
        public string Name { get; set; }
        public TenderTypeEnum Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TenderTypeEnum
    {
        ToProject,
        ToSupply,
        ToWork
    }

}