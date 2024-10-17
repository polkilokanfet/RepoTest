using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class UpdateStructureCostNumberTask : BaseEntity
    {
        [Required]
        public DateTime MomentStart { get; set; }

        public DateTime? MomentFinish { get; set; }

        [Required]
        public virtual ProductBlock ProductBlock { get; set; }

        [Required, MaxLength(50)]
        public string StructureCostNumberOriginal { get; set; }

        [Required, MaxLength(50)]
        public string StructureCostNumber { get; set; }

        public bool? IsAccepted { get; set; }

        public override string ToString()
        {
            return $"{StructureCostNumberOriginal} => {StructureCostNumber} ({ProductBlock})";
        }
    }
}