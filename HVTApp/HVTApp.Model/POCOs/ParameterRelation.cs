using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������������ ������������ ���������, ��� ������� ���� �������� �� ����� ������
    /// </summary>
    public partial class ParameterRelation : BaseEntity
    {
        public virtual Guid ParameterId { get; set; }
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();

        public override string ToString()
        {
            var result = "������������ ���������: ";
            foreach (var parameter in RequiredParameters)
            {
                result += $"{parameter.ToString().ToLower()}; ";
            }
            return result;
        }
    }
}