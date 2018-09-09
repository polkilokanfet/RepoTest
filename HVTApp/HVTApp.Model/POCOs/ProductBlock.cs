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
    [Designation("����")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("�����������"), NotMapped]
        public string Designation { get; set; }

        [Designation("����������� �����������")]
        public string DesignationSpecial { get; set; }

        [Designation("���������")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("�������������")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("�����������")]
        public string StructureCostNumber { get; set; }

        [Designation("������")]
        public bool IsService { get; set; } = false;

        [Designation("���� ���������� ������"), NotMapped]
        public DateTime? LastPriceDate => Prices.Max(x => x.Date);

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;

            var otherBlock = obj as ProductBlock;
            if (otherBlock == null) return false;

            return this.Parameters.AllMembersAreSame(otherBlock.Parameters);
        }

        public string ParametersToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var parameter in Parameters.OrderBy(this.GetWeight))
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