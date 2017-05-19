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
            if (!RequiredParentParametersList.Any()) return true;

            //если обязательные параметры выбраны
            foreach (var requiredParentParameters in RequiredParentParametersList)
                if (requiredParentParameters.Parameters.All(parameters.Contains)) return true;

            return false;
        }

        public int Rank
        {
            get
            {
                int result = 0;
                foreach (var requiredParentParametersWrapper in RequiredParentParametersList)
                {
                    if (result < requiredParentParametersWrapper.Parameters.Count)
                        result = requiredParentParametersWrapper.Parameters.Count;
                }
                return result;
            }
        }
    }
}
