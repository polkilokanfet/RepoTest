using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Категория продукта")]
    public class ProductCategory : BaseEntity
    {
        [Designation("Название полное"), Required, MaxLength(150), OrderStatus(90)]
        public string NameFull { get; set; }

        [Designation("Название сокращенное"), Required, MaxLength(30), OrderStatus(80)]
        public string NameShort { get; set; }

        [Designation("Параметры"), Required, OrderStatus(50)]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();


        /// <summary>
        /// Является ли эта категория заглушкой (для продуктов где категория не определена)
        /// </summary>
        [NotMapped, NotForListView, NotForDetailsView, NotForWrapper]
        public bool IsStub { get; set; } = false;

        public override string ToString()
        {
            return NameShort;
        }
    }
}