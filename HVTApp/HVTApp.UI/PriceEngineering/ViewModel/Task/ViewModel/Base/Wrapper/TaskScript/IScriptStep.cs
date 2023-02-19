using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Wrapper.TaskScript
{
    public interface IScriptStep
    {
        /// <summary>
        /// Текущий статус задачи
        /// </summary>
        PriceEngineeringTaskStatusEnum Status { get; }
    }
}