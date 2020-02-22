using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel : ViewModelBase
    {
        private void InitItemsRefrefresher(IEventAggregator eventAggregator)
        {

            //реакция на изменение проекта
            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
            {
                ProjectItems.Where(x => x.Project.Id == project.Id).ForEach(x => x.Project = project);
            });

            //реакция на удаление тендера
            eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
            {
                _tenders.RemoveById(tender);
                foreach (var projectItem in ProjectItems)
                {
                    var targetTenders = _tenders.Where(x => x.Project.Id == projectItem.Project.Id);
                    projectItem.RefreshTenderInformation(targetTenders);
                }
            });

            //реакция на удаление тендера
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                _tenders.ReAddById(tender);
                var targetTenders = _tenders.Where(x => x.Project.Id == tender.Project.Id);
                ProjectItems.Where(x => x.Project.Id == tender.Project.Id).ForEach(x => x.RefreshTenderInformation(targetTenders));
            });

            //реакция на удаления юнита
            eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                var projectItem = ProjectItems.SingleOrDefault(x => x.SalesUnits.ContainsById(salesUnit));
                RemoveSalesUnitFromProjectItem(projectItem, salesUnit);
            });

            //реакция на сохранения юнита
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                //все айтемы проекта
                var itemsOfProject = ProjectItems.Where(x => x.Project.Id == salesUnit.Project.Id).ToList();
                
                //айтем, в котором содержится юнит
                var itemContainsSalesUnit = itemsOfProject.SingleOrDefault(x => x.SalesUnits.ContainsById(salesUnit));
                
                //айтемы, в которые подойдет юнит
                var targetItems = itemsOfProject.Where(x => x.Fits(salesUnit)).ToList();

                if (targetItems.Any())
                {
                    //если подходит более, чем в 2 айтема, то это дичь какая-то
                    if(targetItems.Count > 2) throw new NotImplementedException("Подходит более, чем в 2 айтема");

                    //если подходит сразу в 2 айтема
                    if (targetItems.Count == 2)
                    {
                        targetItems.Remove(itemContainsSalesUnit);
                        //то тот айтем, что содержал юнит ранее - лишний
                        ProjectItems.Remove(itemContainsSalesUnit);
                    }
                    else
                    {
                        itemContainsSalesUnit?.SalesUnits.RemoveById(salesUnit);
                    }
                    targetItems.First().SalesUnits.Add(salesUnit);
                }
                else
                {
                    ProjectItems.Add(new ProjectItem(new[] { salesUnit }));
                    RemoveSalesUnitFromProjectItem(itemContainsSalesUnit, salesUnit);
                }
            });
        }

        private void RemoveSalesUnitFromProjectItem(ProjectItem item, SalesUnit salesUnit)
        {
            if (item == null) return;

            if (item.SalesUnits.Count == 1)
            {
                ProjectItems.Remove(item);
            }
            else
            {
                item.SalesUnits.RemoveById(salesUnit);
            }
        }

    }
}
