using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������������ ������������ ���������, ��� ������� ���� �������� �� ����� ������
    /// </summary>
    public class RequiredPreviousParameters : BaseEntity
    {
        public virtual Parameter Parameter { get; set; }
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();
    }
}