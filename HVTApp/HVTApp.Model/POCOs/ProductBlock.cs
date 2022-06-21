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
        [Designation("Специальное обозначение"), MaxLength(256)]
        public string DesignationSpecial { get; set; }

        [Designation("Параметры"), NotForListView]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("Себестоимости")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("Фиксированные цены")]
        public virtual List<SumOnDate> FixedCosts { get; set; } = new List<SumOnDate>();

        [Designation("Сралчахвост"), MaxLength(50)]
        public string StructureCostNumber { get; set; }

        [Designation("Чертеж"), MaxLength(25)]
        public string Design { get; set; }

        [Designation("Вес")]
        public double Weight { get; set; }


        #region NotMapped

        /// <summary>
        /// Параметры (упорядоченные)
        /// </summary>
        [Designation("Параметры (упорядоченные)"), NotMapped]
        public List<Parameter> ParametersOrdered => Parameters.OrderBy(parameter => parameter).ToList();

        [Designation("Обозначение"), NotMapped]
        public string Designation => GlobalAppProperties.ProductDesignationService.GetDesignation(this);

        [Designation("Тип"), NotMapped, OrderStatus(10)]
        public ProductType ProductType => GlobalAppProperties.ProductDesignationService.GetProductType(this);

        [Designation("Есть прайс"), NotMapped]
        public bool HasPrice => Prices.Any();

        [Designation("Дата последнего прайса"), NotMapped]
        public DateTime? LastPriceDate => Prices.Any() 
            ? Prices.Max(sumOnDate => sumOnDate.Date) 
            : default(DateTime?);

        [Designation("Есть фиксированный прайс"), NotMapped]
        public bool HasFixedPrice => FixedCosts.Any();

        [Designation("Новый"), NotMapped]
        public bool IsNew => Parameters.ContainsById(GlobalAppProperties.Actual.NewProductParameter);

        [Designation("Услуга"), NotMapped]
        public bool IsService => Parameters.ContainsById(GlobalAppProperties.Actual.ServiceParameter);

        [Designation("Шеф-монтаж"), NotMapped]
        public bool IsSupervision => Parameters.ContainsById(GlobalAppProperties.Actual.SupervisionParameter);

        [Designation("Доставка"), NotMapped]
        public bool IsDelivery { get; set; } = false;

        #endregion



        public override bool Equals(object other)
        {
            return base.Equals(other) || Equals(other as ProductBlock);
        }

        protected bool Equals(ProductBlock other)
        {
            return other != null && this.Parameters.MembersAreSame(other.Parameters, new ParameterComparer());
        }

        ///// <summary>
        ///// Вернуть упорядоченные параметры блока.
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<Parameter> GetOrderedParameters()
        //{
        //    return Parameters.OrderBy(x => x);
        //    //return Parameters.OrderByDescending(parameter => parameter.GetWeight(this));
        //}

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