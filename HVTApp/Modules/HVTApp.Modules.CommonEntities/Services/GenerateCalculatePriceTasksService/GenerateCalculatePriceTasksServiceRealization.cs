using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Prism.Events;

namespace HVTApp.UI.Services
{
    public class GenerateCalculatePriceTasksServiceRealization : IGenerateCalculatePriceTasksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;

        public GenerateCalculatePriceTasksServiceRealization(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
        }

        public async Task GenerateCalculatePriceTasks(ProductWrapper productWrapper, DateTime date, Guid senderId)
        {
            //подт€гиваем все актуальные задачи
            var actualTasks = (await _unitOfWork.GetRepository<CalculatePriceTask>().GetAllAsync()).
                Where(x => x.IsActual).ToList();

            //блоки с неактуальной ценой или без цены вообще
            var blocks = productWrapper.GetBlocksWithoutActualPriceOnDate(date).Union(productWrapper.GetBlocksWithoutAnyPriceOnDate());
            foreach (var productBlockWrapper in blocks)
            {
                //если блок уже в задаче на расчет
                var actualTask = actualTasks.SingleOrDefault(x => x.ProductBlock.Id == productBlockWrapper.Id);
                if (actualTask != null)
                {
                    var project = await _unitOfWork.GetRepository<Project>().GetByIdAsync(senderId);
                    if (project != null)
                    {
                        if(!actualTask.Projects.Contains(project))
                            actualTask.Projects.Add(project);
                        continue;
                    }

                    var offer = await _unitOfWork.GetRepository<Offer>().GetByIdAsync(senderId);
                    if (offer != null)
                    {
                        if (!actualTask.Offers.Contains(offer))
                            actualTask.Offers.Add(offer);
                        continue;
                    }

                    var spec = await _unitOfWork.GetRepository<Specification>().GetByIdAsync(senderId);
                    if (spec != null)
                    {
                        if (!actualTask.Specifications.Contains(spec))
                            actualTask.Specifications.Add(spec);
                    }

                }
                else
                {
                    var productBlock = await _unitOfWork.GetRepository<ProductBlock>().GetByIdAsync(productBlockWrapper.Id);
                    var task = new CalculatePriceTask
                    {
                        ProductBlockId = productBlock.Id,
                        ProductBlock = productBlock,
                        Date = date
                    };

                    _unitOfWork.GetRepository<CalculatePriceTask>().Add(task);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}