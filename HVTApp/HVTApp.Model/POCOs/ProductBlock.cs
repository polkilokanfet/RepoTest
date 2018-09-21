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
    [Designation("����")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("�����������"), NotMapped]
        public string Designation { get; set; }


        [Designation("����������� �����������"), MaxLength(50)]
        public string DesignationSpecial { get; set; }


        [Designation("���������")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("�������������")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("�����������"), MaxLength(10)]
        public string StructureCostNumber { get; set; }

        [Designation("������"), MaxLength(25)]
        public string Design { get; set; }

        [Designation("������"), NotMapped]
        public bool IsService { get; set; } = false;

        [Designation("���")]
        public double Weight { get; set; }

        [Designation("���� ���������� ������"), NotMapped]
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
        /// ������� ������������� ��������� �����.
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