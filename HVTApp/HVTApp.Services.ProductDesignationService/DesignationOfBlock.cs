using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.ProductDesignationService
{
    internal struct DesignationOfBlock
    {
        public IEnumerable<Parameter> Parameters { get; }
        public string Designation { get; }

        public DesignationOfBlock(IEnumerable<Parameter> parameters, string designation)
        {
            Parameters = parameters;
            Designation = designation;
        }

        public override string ToString()
        {
            return Designation;
        }
    }
}