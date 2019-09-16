using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Штрафные санкции
    /// </summary>
    [Designation("Штрафные санкции")]
    public class Penalty : BaseEntity
    {
        [Designation("% за день просрочки"), Required]
        public double PercentPerDay { get; set; } = 0.0001;

        [Designation("Ограничение штрафа"), Required]
        public double PercentLimit { get; set; } = 1;

        [Designation("Фактически уплаченные штрафы")]
        public double PenaltyPaid { get; set; } = 0;
    }
}