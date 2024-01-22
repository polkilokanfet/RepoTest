using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Обязательные родительские параметры, без которых этот параметр не имеет смысла
    /// </summary>
    [Designation("Ограничение использования параметра")]
    public partial class ParameterRelation : BaseEntity
    {
        [Designation("Обязательные параметры перед")]
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();

        [Designation("Id связанного параметра")]
        public Guid ParameterId { get; set; }

        public override string ToString()
        {
            if (RequiredParameters.Any() == false) return $"{nameof(ParameterRelation)} is empty";

            return RequiredParameters
                .OrderBy(parameter => parameter)
                .ToStringEnum();
        }
    }
}