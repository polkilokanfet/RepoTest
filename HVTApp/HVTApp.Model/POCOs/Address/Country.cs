using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������
    /// </summary>
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<District> Districts { get; set; } // ������.
        public override string ToString()
        {
            return Name;
        }
    }
}