using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Причина проигрыша")]
    public class LosingReason : BaseEntity
    {
        [Designation("Название"), Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}