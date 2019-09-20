using System;
using System.Collections.Generic;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������������ ������������ ���������, ��� ������� ���� �������� �� ����� ������
    /// </summary>
    [Designation("����������� ������������� ���������")]
    public partial class ParameterRelation : BaseEntity
    {
        [Designation("������������ ��������� �����")]
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();

        public Guid ParameterId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("������������ ���������: ");
            RequiredParameters.ForEach(x => sb.Append($"{x.ToString().ToLower()}; "));
            return sb.ToString();
        }
    }
}