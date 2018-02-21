using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Services.GenerateCalculatePriceTasksService
{
    public class GenerateCalculatePriceTasksServiceRealization : IGenerateCalculatePriceTasksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateCalculatePriceTasksServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CalculatePriceTask>> GenerateCalculatePriceTasks(ProductWrapper productWrapper,
            DateTime date)
        {
            var result = new List<CalculatePriceTask>();

            //подт€гиваем все актуальные задачи
            var actualTasks = (await _unitOfWork.GetRepository<CalculatePriceTask>().GetAllAsync()).
                Where(x => x.IsActual).ToList();

            //блоки с неактуальной ценой
            var blocks = productWrapper.GetBlocksWithoutActualPriceOnDate(date);
            foreach (var productBlockWrapper in blocks)
            {
                if (actualTasks.Any(x => x.ProductBlockId == productBlockWrapper.Id))
                    continue;

                var task = new CalculatePriceTask
                {
                    ProductBlockId = productBlockWrapper.Model.Id,
                    ProductBlock = productBlockWrapper.Model,
                    PriceOnDate = date
                };

                _unitOfWork.GetRepository<CalculatePriceTask>().Add(task);
                result.Add(task);
            }

            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}