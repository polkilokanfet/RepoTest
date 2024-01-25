using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemBackManager : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemBackManager(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}