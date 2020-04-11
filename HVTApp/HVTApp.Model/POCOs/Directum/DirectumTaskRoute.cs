using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Маршрут задачи
    /// </summary>
    [Designation("Маршрут")]
    public class DirectumTaskRoute : BaseEntity
    {
        /// <summary>
        /// Этапы маршрута
        /// </summary>
        [Designation("Этапы"), OrderStatus(10), Required]
        public virtual List<DirectumTaskRouteItem> Items { get; set; }

        /// <summary>
        /// Задача параллельная
        /// </summary>
        [Designation("Параллельность"), OrderStatus(5)]
        public bool IsParallel { get; set; } = true;
    }
}