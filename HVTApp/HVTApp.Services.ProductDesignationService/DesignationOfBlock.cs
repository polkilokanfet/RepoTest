using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.ProductDesignationService
{
    internal readonly struct DesignationOfBlock
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
            //return $"{Designation} Parameters: {Parameters.OrderBy(x => x.ToString()).ToStringEnum()}";
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj is DesignationOfBlock other)
        //    {
        //        if (!Equals(this.Designation, other.Designation)) return false;
        //        if (!this.Parameters.MembersAreSame(other.Parameters)) return false;
        //        return true;
        //    }

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}