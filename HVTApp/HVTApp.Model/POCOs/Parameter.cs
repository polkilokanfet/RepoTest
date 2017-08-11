using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Parameter : BaseEntity
    {
        public virtual ParameterGroup Group { get; set; }
        public string Value { get; set; }
        public virtual List<RequiredPreviousParameters> RequiredPreviousParameters { get; set; } = new List<RequiredPreviousParameters>();



        public override string ToString()
        {
            string result = Value;
            if (Group.Measure != null)
                result = result + " " + Group.Measure.ShortName;
            return result;
        }
    }
}