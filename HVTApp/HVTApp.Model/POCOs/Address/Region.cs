using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// �������, ����, ���������� � �.�.
    /// </summary>
    public partial class Region : BaseEntity
    {
        public string Name { get; set; }
        public virtual Guid DistrictId { get; set; }
        public virtual List<Locality> Localities { get; set; } // ���������� ������.

        public override string ToString()
        {
            return Name;
        }
    }
}