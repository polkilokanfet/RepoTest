using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Wrapper.TaskScript
{
    public interface IScriptStep
    {
        /// <summary>
        /// ������� ������ ������
        /// </summary>
        PriceEngineeringTaskStatusEnum Status { get; }
    }
}