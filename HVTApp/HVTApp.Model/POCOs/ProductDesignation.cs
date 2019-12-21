using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Обозначение продукта")]
    public partial class ProductDesignation : BaseEntity
    {
        [Designation("Обозначение"), Required, MaxLength(50), OrderStatus(10)]
        public string Designation { get; set; }

        [Designation("Параметры"), Required]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("Родители")]
        public virtual List<ProductDesignation> Parents { get; set; } = new List<ProductDesignation>();

        public Dictionary<List<Parameter>, string> GetDesignationDictionary()
        {
            if (!Parents.Any())
            {
                return new Dictionary<List<Parameter>, string> {{Parameters.ToList(), Designation}};
            }

            var result = new Dictionary<List<Parameter>, string>(); 

            foreach (var parent in Parents)
            {
                var parentDictionary = parent.GetDesignationDictionary();
                foreach (var kvp in parentDictionary)
                {
                    var key = kvp.Key.ToList();
                    key.AddRange(Parameters);
                    var designation = $"{parentDictionary[kvp.Key]}{Designation}";
                    result.Add(key, designation);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return Designation;
        }
    }
}