using System.Collections.Generic;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ConverterExtansions
    {
        public static IEnumerable<ProductUnitsGroup> ToGroupUnits(this IEnumerable<IProductUnit> productUnits)
        {
            var converter = new ProductUnitsToProductGroupsConverter();
            return converter.Convert(productUnits, null, null, null) as IEnumerable<ProductUnitsGroup>;
        } 
    }
}