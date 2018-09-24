using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Номер документа")]
    public class DocumentNumber : BaseEntity
    {
        [Key]
        public int Number { get; set; }

        public override string ToString()
        {
            return Number.ToString();
        }

    }
}