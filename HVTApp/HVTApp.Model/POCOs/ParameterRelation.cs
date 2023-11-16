using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

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
            if (RequiredParameters.Any() == false) return "ParameterRelation is empty";

            return RequiredParameters
                .OrderBy(parameter => parameter)
                .Select(parameter => parameter.ToStringWithGroup())
                .ToStringEnum();
        }
    }
}