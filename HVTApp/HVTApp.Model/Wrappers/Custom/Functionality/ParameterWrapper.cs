using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrappers
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

            var p = parameters.ToList();
            //если обязательные параметры выбраны
            foreach (var requiredParentParameters in RequiredPreviousParameters)
                if (requiredParentParameters.RequiredParameters.All(p.Contains)) return true;

            return false;
        }
    }
}
