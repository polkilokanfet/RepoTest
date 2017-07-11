using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class RequiredPreviousParameters : BaseEntity
    {
        public virtual Parameter Parameter { get; set; }
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>(); //обязательные родительские параметры, без которых этот параметр не имеет смысла
    }
}