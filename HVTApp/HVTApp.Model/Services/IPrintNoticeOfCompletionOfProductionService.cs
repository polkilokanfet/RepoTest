using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.ProductionViewModelEntities;

namespace HVTApp.Model.Services
{
    public interface IPrintNoticeOfCompletionOfProductionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productionGroups"></param>
        /// <param name="letter"></param>
        /// <param name="path"></param>
        /// <param name="employee">Сотрудник, ответственный за отгрузку</param>
        void PrintNoticeOfCompletionOfProduction(IEnumerable<ProductionGroup> productionGroups, Document letter, string path, Employee employee);
    }
}