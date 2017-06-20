using System;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductItemWrapper
    {
        public bool HasSameParameters(ProductItemWrapper productItem)
        {
            if (productItem == null)
                throw new ArgumentNullException();

            return !this.Parameters.Except(productItem.Parameters).Any();
        }

        public string ParametersToString
        {
            get
            {
                string result = string.Empty;
                foreach (var parameter in Parameters)
                    result = $"{result}; {parameter.Group.Name}: {parameter.Value}";
                return result.Remove(0, 2);
            }
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Designation)) return Designation;
            return ParametersToString;
        }
    }
}