using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.ProductionViewModelEntities;

namespace HVTApp.Model.Services
{
    public interface IPrintNoticeOfCompletionOfProductionService
    {
        void PrintNoticeOfCompletionOfProduction(IEnumerable<ProductionGroup> productionGroups, Document letter, string path);
    }
}