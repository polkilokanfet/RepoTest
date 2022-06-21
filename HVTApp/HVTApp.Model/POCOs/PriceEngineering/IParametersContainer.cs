using System.Collections.Generic;

namespace HVTApp.Model.POCOs
{
    public interface IParametersContainer
    {
        List<Parameter> Parameters { get; }
    }
}