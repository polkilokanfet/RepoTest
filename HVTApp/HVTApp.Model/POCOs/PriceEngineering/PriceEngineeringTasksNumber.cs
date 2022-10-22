using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PriceEngineeringTasksNumber : BaseEntity
    {
        [Key]
        public int Number { get; set; }

        public override string ToString()
        {
            return $"{Number:D4}";
        }
    }
}