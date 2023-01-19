using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public interface IStatusesContainer
    {
        StatusesCollection Statuses { get; }
        PriceEngineeringTaskStatusEnum Status { get; }
        PriceEngineeringTask Model { get; }
    }
}