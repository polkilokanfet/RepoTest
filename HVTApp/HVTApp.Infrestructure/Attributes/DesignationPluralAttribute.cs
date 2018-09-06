using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DesignationPluralAttribute : Attribute
    {
        public string Designation { get; }

        public DesignationPluralAttribute(string designation)
        {
            Designation = designation;
        }
    }
}