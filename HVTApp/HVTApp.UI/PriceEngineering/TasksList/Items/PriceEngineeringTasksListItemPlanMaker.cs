using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTasksListItemPlanMaker : 
        PriceEngineeringTasksListItemBase<PriceEngineeringTaskListItemPlanMaker>
    {
        public bool? IsUploadedDocumentationToTeamCenter
        {
            get
            {
                if (this.ChildPriceEngineeringTasks.All(x => x.IsUploadedDocumentationToTeamCenter == true)) return true;
                if (this.ChildPriceEngineeringTasks.All(x => x.IsUploadedDocumentationToTeamCenter == false)) return false;
                return default;
            }
        }

        public PriceEngineeringTasksListItemPlanMaker(PriceEngineeringTasks entity) : base(entity)
        {
        }

        protected override IEnumerable<SalesUnit> GetSalesUnits()
        {
            return Entity.ChildPriceEngineeringTasks
                .Where(task => task.GetSuitableTasksForOpenOrder(GlobalAppProperties.User).Any())
                .SelectMany(task => task.SalesUnits);
        }

        protected override IEnumerable<PriceEngineeringTaskListItemPlanMaker> GetChildTasks()
        {
            return Entity
                .GetSuitableTasksForOpenOrder(GlobalAppProperties.User)
                .Select(task => new PriceEngineeringTaskListItemPlanMaker(task));
        }

        public override bool ToShow =>
            base.ToShow &&
            Entity.ChildPriceEngineeringTasks.Any(x =>
                x.UserPlanMaker?.Id == GlobalAppProperties.User.Id &&
                x.Status.Equals(ScriptStep.ProductionRequestStart));

        public void RefreshIsUploadedDocumentationToTeamCenter()
        {
            RaisePropertyChanged(nameof(IsUploadedDocumentationToTeamCenter));
        }
    }
}