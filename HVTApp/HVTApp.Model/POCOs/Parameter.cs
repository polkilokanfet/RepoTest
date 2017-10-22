using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Parameter : BaseEntity
    {
        public virtual Guid GroupId { get; set; }
        public string Value { get; set; }
        public virtual List<RequiredPreviousParameters> RequiredPreviousParameters { get; set; } = new List<RequiredPreviousParameters>();
    }
}