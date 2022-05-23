using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class PriceEngineeringTaskTceViewModel : BaseDetailsViewModel<PriceEngineeringTaskTceWrapper1, PriceEngineeringTaskTce, AfterSavePriceEngineeringTaskTceEvent>
    {
        public PriceEngineeringTaskTceViewModel(IUnityContainer container) : base(container)
        {
            
        }

        public void Create(IEnumerable<PriceEngineeringTask> tasks)
        {
            var priceEngineeringTasks = tasks.Select(x => this.UnitOfWork.Repository<PriceEngineeringTask>().GetById(x.Id)).ToList();
            var wrapper = new PriceEngineeringTaskTceWrapper1(new PriceEngineeringTaskTce());
            wrapper.Story.Add(new PriceEngineeringTaskTceStoryItemWrapper(new PriceEngineeringTaskTceStoryItem {StoryAction = PriceEngineeringTaskTceStoryItemStoryAction.Start, PriceEngineeringTaskTceId = wrapper.Model.Id}));

            foreach (var priceEngineeringTask in priceEngineeringTasks)
            {
                wrapper.PriceEngineeringTaskList.Add(new PriceEngineeringTaskEmptyWrapper(priceEngineeringTask));
                foreach (var task in priceEngineeringTask.GetAllPriceEngineeringTasks())
                {
                    var structureCostVersion = new PriceEngineeringTaskTceStructureCostVersion
                    {
                        ParentUnitId = task.Id, 
                        PriceEngineeringTaskTceId = wrapper.Model.Id
                    };
                    wrapper.SccVersions.Add(new TceStructureCostVersion(structureCostVersion, priceEngineeringTasks));

                    foreach (var blockAdded in task.ProductBlocksAdded)
                    {
                        var structureCostVersion1 = new PriceEngineeringTaskTceStructureCostVersion
                        {
                            ParentUnitId = blockAdded.Id,
                            PriceEngineeringTaskTceId = wrapper.Model.Id
                        };
                        wrapper.SccVersions.Add(new TceStructureCostVersion(structureCostVersion1, priceEngineeringTasks));
                    }
                }
            }

            this.Load(wrapper, this.UnitOfWork);
        }
    }
}