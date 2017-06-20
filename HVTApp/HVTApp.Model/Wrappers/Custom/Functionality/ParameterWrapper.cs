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
            if (!RequiredParents.Any()) return true;

            var p = parameters.ToList();
            //если обязательные параметры выбраны
            foreach (var requiredParentParameters in RequiredParents)
                if (requiredParentParameters.Parameters.All(p.Contains)) return true;

            return false;
        }

        public int Rank
        {
            get
            {
                int result = 0;
                foreach (var requiredParentParametersWrapper in RequiredParents)
                {
                    if (result < requiredParentParametersWrapper.Parameters.Count)
                        result = requiredParentParametersWrapper.Parameters.Count;
                }
                return result;
            }
        }
    }
}
