using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Блок")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("Обозначение"), NotMapped]
        public string Designation { get; set; }

        [Designation("Специальное обозначение")]
        public string DesignationSpecial { get; set; }

        [Designation("Параметры")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("Себестоимости")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("Сралчахвост")]
        public string StructureCostNumber { get; set; }

        [Designation("Услуга")]
        public bool IsService { get; set; } = false;

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
            return other != null && this.Parameters.Select(x => x.Id).MembersAreSame(other.Parameters.Select(x => x.Id));
        }

        public string ParametersToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var parameter in Parameters.OrderByDescending(this.GetWeight))
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