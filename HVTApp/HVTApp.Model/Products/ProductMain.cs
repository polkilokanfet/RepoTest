using System.Collections.Generic;

namespace HVTApp.Model
{
    public class ProductMain : ProductBase
    {
        /// <summary>
        /// Дополнительное оборудование, зависимое от настоящего изделия.
        /// </summary>
        public virtual List<ProductsOptionalGroup> ProductsOptionalGroups { get; set; }

    }
}
