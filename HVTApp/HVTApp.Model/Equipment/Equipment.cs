using System;
using System.Linq;

namespace HVTApp.Model
{
    public class Equipment : BaseEntity
    {
        /// <summary>
        /// Обозначение типа оборудования
        /// </summary>
        public string DesignationType { get; set; }
        /// <summary>
        /// Обозначение серии оборудования (основное обозначение)
        /// </summary>
        public string DesignationSeries { get; set; }
        /// <summary>
        /// Обозначение опций оборудования (вспомогательные характеристики)
        /// </summary>
        public string DesignationOptions { get; set; }
        /// <summary>
        /// Список характеристик оборудования
        /// </summary>
        public virtual TechLinksCollection Links { get; set; }


        public override bool EqualsProperties(object obj)
        {
            Equipment otherEquipment = obj as Equipment;
            if (otherEquipment == null)
                throw new ArgumentNullException();

            return !this.Links.Except(otherEquipment.Links).Any();
        }
    }
}
