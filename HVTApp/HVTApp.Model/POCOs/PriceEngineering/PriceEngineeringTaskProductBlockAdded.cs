using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (добавленный блок)")]
    [DesignationPlural("Технико-стоимостная проработка (добавленные блоки)")]
    public class PriceEngineeringTaskProductBlockAdded : BaseEntity, IProductBlockContainer, IStructureCostVersionsContainer
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(500)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Количество"), Required, OrderStatus(950)]
        public int Amount { get; set; } = 1;

        [Designation("На каждый блок"), OrderStatus(800)]
        public bool IsOnBlock { get; set; } = true;

        [Designation("Блок продукта"), Required, OrderStatus(900)]
        public virtual ProductBlock ProductBlock { get; set; }

        [Designation("Удалено"), Required, OrderStatus(950)]
        public bool IsRemoved { get; set; } = false;
        
        [Designation("Версии SCC"), OrderStatus(80)]
        public virtual List<StructureCostVersion> StructureCostVersions { get; set; } = new List<StructureCostVersion>();

        /// <summary>
        /// Формирует продукт из проработанных блоков
        /// </summary>
        /// <returns></returns>
        public Product GetProduct()
        {
            return new Product
            {
                ProductBlock = this.ProductBlock
            };
        }

        public override string ToString()
        {
            string s = IsOnBlock ? "на каждый блок" : "на весь заказ";
            return $"{ProductBlock} = {Amount} шт. {s}, SCC: {ProductBlock.StructureCostNumber}";
        }
        /// <summary>
        /// Блок продукта конкретно из этой задачи имеет версию номера SCC в TCE
        /// </summary>
        public bool HasSccNumberInTce =>
            this.StructureCostVersions.Any(structureCostVersion =>
                structureCostVersion.Version.HasValue &&
                structureCostVersion.OriginalStructureCostNumber == this.ProductBlock.StructureCostNumber);
    }
}