using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Comparers;

namespace HVTApp.Model.POCOs
{
    [Designation("����")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("����������� �����������"), MaxLength(256), OrderStatus(8)]
        public string DesignationSpecial { get; set; }

        [Designation("���������"), NotForListView]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("�������������"), NotForListView]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("������������� ����"), NotForListView]
        public virtual List<SumOnDate> FixedCosts { get; set; } = new List<SumOnDate>();

        [Designation("�����������"), MaxLength(50), OrderStatus(7)]
        public string StructureCostNumber { get; set; }

        /// <summary>
        /// ����������� ���������
        /// </summary>
        [Designation("����������� ���������"), OrderStatus(8)]
        public bool StructureCostNumberIsRequired { get; set; } = true;

        [Designation("������"), MaxLength(25)]
        public string Design { get; set; }

        [Designation("���")]
        public double Weight { get; set; }

        [Designation("������������ (�/� �� ��.)")]
        public virtual double? LaborCosts { get; set; }


        #region NotMapped

        /// <summary>
        /// ��������� (�������������)
        /// </summary>
        [Designation("��������� (�������������)"), NotMapped, OrderStatus(-10)]
        public List<Parameter> ParametersOrdered => Parameters.OrderBy(parameter => parameter).ToList();

        [Designation("�����������"), NotMapped, OrderStatus(9)]
        public string Designation => string.IsNullOrEmpty(this.DesignationSpecial)
            ? GlobalAppProperties.ProductDesignationService?.GetDesignation(this)
            : this.DesignationSpecial;

        [Designation("���"), NotMapped, OrderStatus(10)]
        public ProductType ProductType => GlobalAppProperties.ProductDesignationService?.GetProductType(this);

        [Designation("���� �����"), NotMapped]
        public bool HasPrice => Prices.Any();

        [Designation("���� ���������� ������"), NotMapped]
        public DateTime? LastPriceDate => Prices.Any() 
            ? Prices.Max(sumOnDate => sumOnDate.Date) 
            : default(DateTime?);

        [Designation("���� ������������� �����"), NotMapped]
        public bool HasFixedPrice => FixedCosts.Any();

        [Designation("�����"), NotMapped]
        public bool IsNew => Parameters.ContainsById(GlobalAppProperties.Actual.NewProductParameter);

        [Designation("������"), NotMapped]
        public bool IsService => Parameters.ContainsById(GlobalAppProperties.Actual.ServiceParameter);

        [Designation("���-������"), NotMapped]
        public bool IsSupervision => Parameters.ContainsById(GlobalAppProperties.Actual.SupervisionParameter);

        [Designation("��������"), NotMapped]
        public bool IsDelivery { get; set; } = false;

        [Designation("��������"), NotMapped]
        public bool IsKit => Parameters.ContainsById(GlobalAppProperties.Actual.ComplectsParameter);

        #endregion



        public override int GetHashCode()
        {
            return Parameters.GetHashSum();
        }

        public override bool Equals(object other)
        {
            return base.Equals(other) || Equals(other as ProductBlock);
        }

        protected bool Equals(ProductBlock other)
        {
            if (other == null) return false;
            return this.Parameters.MembersAreSame(other.Parameters, new ParameterComparer());
        }

        ///// <summary>
        ///// ������� ������������� ��������� �����.
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
            return Designation ?? ParametersToString();
        }

        /// <summary>
        /// ������� ��������� ������������� ��������� �� ������������ ����
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public double? GetFixedCost(DateTime date)
        {
            if (this.FixedCosts.Any() == false) return default;
            var costs = this.FixedCosts.Where(x => x.Date <= date).ToList();
            if (costs.Any() == false) return default;
            return costs.OrderBy(x => x.Date).Last().Sum;
        }

        private const int DaysForPrices = 150;
        private const double Tolerance = 0.5;
        public bool AllowAddThisPriceFromCalculation(SumOnDate price)
        {
            if (price.Date < DateTime.Today) return false;
            if (price.Date > DateTime.Today.AddDays(DaysForPrices)) return false;
            if (this.Prices.Any(p => p.Date == price.Date && Math.Abs(p.Sum - price.Sum) < 0.0001)) return false;

            //�� ������� �� ����� ��������
            for (int i = 0; i < 5; i++)
            {
                var quarter = price.Date.QuarterAbsolute() - i;
                var pricesOfQuarter = this.Prices
                    .Where(x => x.Date.QuarterAbsolute() == quarter)
                    .ToList();
                if (pricesOfQuarter.Any())
                {
                    var average = pricesOfQuarter.Select(x => x.Sum).Average();
                    if ((average * (1 + Tolerance)) < price.Sum)
                        return false;
                    if ((average * (1 - Tolerance)) > price.Sum)
                        return false;

                    return true;
                }
            }

            return true;
        }
    }
}