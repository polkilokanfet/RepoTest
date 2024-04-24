using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemConstructor : PriceEngineeringTaskListItemBase
    {
        public override bool ToShow => base.ToShow && (IsForWork || IsForInspection);

        private bool IsForWork => Entity.UserConstructor?.Id == GlobalAppProperties.User.Id;

        private bool IsForInspection => Entity.UserConstructorInspector?.Id == GlobalAppProperties.User.Id;

        public PriceEngineeringTaskListItemConstructor(PriceEngineeringTask entity) : base(entity)
        {
        }
    }
}