using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class TceStructureCostVersion : TceStructureCostVersionWrapper
    {
        public PriceEngineeringTask ParentPriceEngineeringTask { get; }
        public IProductBlockContainer ParentUnit { get; }

        public string Name => ParentUnit.ToString();
        public string StructureCostVersionFromConstructor => ParentUnit.ProductBlock.StructureCostNumber;

        public TceStructureCostVersion(PriceEngineeringTaskTceStructureCostVersion model, IEnumerable<PriceEngineeringTask> priceEngineeringTasks) : base(model)
        {
            //поиск родительской сущности
            foreach (var priceEngineeringTask in priceEngineeringTasks)
            {
                if (ParentUnit != null)
                {
                    break;
                }

                ParentPriceEngineeringTask = priceEngineeringTask;
                var tasks = priceEngineeringTask.GetAllPriceEngineeringTasks();
                foreach (var task in tasks)
                {
                    if (ParentUnit != null)
                    {
                        break;
                    }

                    //сама задача?
                    if (model.ParentUnitId == task.Id)
                    {
                        ParentUnit = task;
                        break;
                    }

                    //добавленный блок?
                    foreach (var blockAdded in task.ProductBlocksAdded)
                    {
                        if (model.ParentUnitId == blockAdded.Id)
                        {
                            ParentUnit = blockAdded;
                            break;
                        }
                    }
                }
            }

            if (ParentUnit == null)
            {
                throw new Exception("Связанная сущность не найдена");
            }
        }
    }
}