using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;

namespace HVTApp.Model.POCOs
{
    [Designation("Блок")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("Обозначение"), NotMapped]
        public string Designation => GlobalAppProperties.ProductDesignationService.GetDesignation(this);


        [Designation("Специальное обозначение"), MaxLength(50)]
        public string DesignationSpecial { get; set; }


        [Designation("Тип"), NotMapped, OrderStatus(10)]
        public ProductType ProductType => GlobalAppProperties.ProductDesignationService.GetProductType(this);


        [Designation("Параметры")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("Себестоимости")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("Фиксированные цены")]
        public virtual List<SumOnDate> FixedCosts { get; set; } = new List<SumOnDate>();

        [Designation("Сралчахвост"), MaxLength(50)]
        public string StructureCostNumber { get; set; }

        [Designation("Чертеж"), MaxLength(25)]
        public string Design { get; set; }

        [Designation("Услуга"), NotMapped]
        public bool IsService => Parameters.ContainsById(GlobalAppProperties.Actual.ServiceParameter);

        [Designation("Шеф-монтаж"), NotMapped]
        public bool IsSupervision => Parameters.ContainsById(GlobalAppProperties.Actual.SupervisionParameter);

        [Designation("Доставка"), NotMapped]
        public bool IsDelivery { get; set; } = false;

        [Designation("Вес")]
        public double Weight { get; set; }

        [Designation("Дата последнего прайса"), NotMapped]
        public DateTime? LastPriceDate => Prices.Any() ? Prices.Max(x => x.Date) : default(DateTime?);

        public override bool Equals(object other)
        {
            return base.Equals(other) || Equals(other as ProductBlock);
        }

        protected bool Equals(ProductBlock other)
        {
            return other != null && this.Parameters.MembersAreSame(other.Parameters, new ParameterComparer());
        }

        /// <summary>
        /// Вернуть упорядоченные параметры блока.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parameter> GetOrderedParameters()
        {
            return Parameters.OrderByDescending(x => x.GetWeight(this));
        }

        public string ParametersToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var parameter in Parameters.OrderByDescending(x => x.GetWeight(this)))
                stringBuilder.Append($"{parameter.ParameterGroup}: {parameter.Value}; ");

            return stringBuilder.ToString();
        }


        public override string ToString()
        {
            if (DesignationSpecial != null) return DesignationSpecial;
            if (Designation != null) return Designation;
            return ParametersToString();
        }
    }
}