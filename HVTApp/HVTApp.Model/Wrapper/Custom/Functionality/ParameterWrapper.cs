using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ParameterWrapper
    {
        /// <summary>
        /// Может ли быть выбран параметр совместно с другими
        /// </summary>
        /// <param name="parameters">Параметры, совместно с которыми требуется выбрать этот параметр</param>
        /// <returns></returns>
        public bool CanBeSelected(IEnumerable<ParameterWrapper> parameters)
        {
            //если нет обязательных родительских параметров
            if (!ParameterRelations.Any()) return true;

            //если обязательные параметры выбраны
            return ParameterRelations.Any(requiredParentParameters => requiredParentParameters.RequiredParameters.All(parameters.Contains));
        }
    }
}
