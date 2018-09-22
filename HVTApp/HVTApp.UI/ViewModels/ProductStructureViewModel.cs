using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;

namespace HVTApp.UI.ViewModels
{
    public class ProductStructureViewModel
    {
        public List<ProductStructure> ProductStructures { get; }
        public ProductStructureViewModel(SalesUnit salesUnit)
        {
            ProductStructures = ProductStructure.GenerateProductStructures(salesUnit).ToList();
        }
    }
}