using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Modules.Sales
{
    public class ProjectUnitsStore : IProjectUnitsStore
    {
        private readonly IModelsStore _modelsStore;
        private readonly Dictionary<Guid, List<Guid>> _dictionary = new Dictionary<Guid, List<Guid>>();

        public ProjectUnitsStore(IModelsStore modelsStore, IEventAggregator eventAggregator)
        {
            _modelsStore = modelsStore;

            Load();

            //реакция на удаление юнита
            eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(
                unit =>
                {
                    if (unit.Project == null)
                    {
                        foreach (var element in _dictionary)
                        {
                            if (element.Value.Contains(unit.Id))
                            {
                                element.Value.Remove(unit.Id);
                                break;
                            }
                        }
                    }
                    else
                    {
                        _dictionary[unit.Project.Id].Remove(unit.Id);
                    }
                });

            //реакция на сохранение юнита
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(
                unit =>
                {
                    //если такого проекта еще нет
                    if(!_dictionary.ContainsKey(unit.Project.Id))
                        _dictionary.Add(unit.Project.Id, new List<Guid>());

                    if(!_dictionary[unit.Project.Id].Contains(unit.Id))
                        _dictionary[unit.Project.Id].Add(unit.Id);

                    //если юнит перенесли в другой проект
                    if (_dictionary.SelectMany(x => x.Value).Count(unitId => unitId == unit.Id) > 1)
                    {
                        //удаляем юнит из того проекта
                        foreach (var element in _dictionary)
                        {
                            if (element.Value.Contains(unit.Id) && element.Key != unit.Project.Id)
                            {
                                element.Value.Remove(unit.Id);
                                break;
                            }
                        }
                    }
                });
        }

        private void Load()
        {
            //загрузка всех юнитов и сопоставление их с проектами
            List<SalesUnit> salesUnits = _modelsStore.UnitOfWork.Repository<SalesUnit>().GetAll();
            var salesUnitsGroups = salesUnits.GroupBy(salesUnit => salesUnit.Project.Id);
            foreach (var salesUnitsGroup in salesUnitsGroups)
            {
                _dictionary.Add(salesUnitsGroup.Key, salesUnitsGroup.Select(x => x.Id).Distinct().ToList());
            }
        }

        public IEnumerable<Guid> GetUnitsIds(Guid projectId)
        {
            if (!_dictionary.ContainsKey(projectId))
            {
                _dictionary.Add(projectId, new List<Guid>());
            }

            return _dictionary[projectId];
        }
    }
}