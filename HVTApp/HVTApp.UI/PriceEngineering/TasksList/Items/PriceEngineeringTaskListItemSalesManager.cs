using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemSalesManager : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemSalesManager(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}