using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
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
            if (!RequiredPreviousParameters.Any()) return true;

            //если обязательные параметры выбраны
            return RequiredPreviousParameters.Any(requiredParentParameters => requiredParentParameters.RequiredParameters.All(parameters.Contains));
        }
    }
}
