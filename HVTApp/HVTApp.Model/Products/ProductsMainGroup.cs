using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model
{
    public class ProductsMainGroup : ProductsGroup<ProductMain>
    {
        /// <summary>
        /// Проект
        /// </summary>
        public virtual Project Project { get; set; }
        /// <summary>
        /// Объект
        /// </summary>
        public virtual Facility Facility { get; set; }
        /// <summary>
        /// Спецификация
        /// </summary>
        public virtual Specification Specification { get; set; }
        /// <summary>
        /// Дополнительное оборудование, зависимое от настоящей группы основоного.
        /// </summary>
        public virtual List<ProductsOptionalGroup> ProductsOptionalGroups { get; set; }

        /// <summary>
        /// Дополнительное оборудование, зависимое от настоящей группы основоного, включенное в стоимость.
        /// </summary>
        //public virtual List<ProductOptional> ProductsOptionalGroupsInCoast
        //{
        //    get
        //    {
        //        if (ProductsOptionalGroups == null)
        //            return new List<ProductOptional>();

        //    }

        //}

    }
}