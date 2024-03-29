﻿using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DesignationAttribute : Attribute
    {
        public string Designation { get; }

        public DesignationAttribute(string designation)
        {
            Designation = designation;
        }
    }
}
