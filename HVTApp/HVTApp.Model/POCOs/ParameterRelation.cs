using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Обязательные родительские параметры, без которых этот параметр не имеет смысла
    /// </summary>
    public partial class ParameterRelation : BaseEntity
    {
        public virtual Guid ParameterId { get; set; }
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();

        public override string ToString()
        {
            var result = "Обязятельные параметры: ";
            foreach (var parameter in RequiredParameters)
            {
                result += $"{parameter.ToString().ToLower()}; ";
            }
            return result;
        }
    }
}