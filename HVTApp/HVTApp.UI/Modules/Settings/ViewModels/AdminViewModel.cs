using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Serialization;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using HVTApp.Model;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class AdminViewModel
    {
        private readonly IUnityContainer _container;
        public DelegateLogCommand Command { get; }

        public AdminViewModel(IUnityContainer container)
        {
            _container = container;
            Command = new DelegateLogCommand(
                () =>
                {
                    //try
                    //{
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ep@mail.ru", "SubjTest", "BodyTest");
                    //    _container.Resolve<IMessageService>().ShowOkMessageDialog("Send letter", "Success!");
                    //}
                    //catch (Exception e)
                    //{
                    //    _container.Resolve<IHvtAppLogger>().LogError(e.PrintAllExceptions(), e);
                    //    _container.Resolve<IMessageService>().ShowOkMessageDialog("Error", e.PrintAllExceptions());
                    //}


                    var messageService = _container.Resolve<IMessageService>();
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    var engineeringTasks = unitOfWork.Repository<PriceEngineeringTask>().GetAll();
                    engineeringTasks.ForEach(x => x.IsValidForProduction = true);

                    User backManagerBoss = unitOfWork.Repository<User>().Find(x => x.Employee.Person.Surname == "Игнатенко").First();

                    var priceEngineeringTasks = engineeringTasks
                        .Where(x => x.ParentPriceEngineeringTaskId == null)
                        .Where(x => x.SalesUnits.Any())
                        .Where(x => x.GetPriceEngineeringTasks(unitOfWork).BackManager != null)
                        .Where(x => x.GetAllPriceEngineeringTasks().All(x1 => x1.Status.Equals(ScriptStep.Accept)))
                        .ToList();

                    foreach (var task in priceEngineeringTasks)
                    {
                        var t = task.GetAllPriceEngineeringTasks();
                        var tasks = task.GetPriceEngineeringTasks(unitOfWork);
                        foreach (var tItem in t)
                        {
                            var moment = tItem.Statuses.Max(x => x.Moment).AddSeconds(1);
                            tItem.Statuses.Add(new PriceEngineeringTaskStatus
                            {
                                Moment = moment,
                                StatusEnum = ScriptStep.LoadToTceStart.Value
                            });

                            tItem.Messages.Add(new PriceEngineeringTaskMessage()
                            {
                                Author = backManagerBoss,
                                Moment = moment.AddSeconds(2),
                                Message = $"Назначен бэк-менеджер: {tasks.BackManager.Employee.Person}"
                            });

                            if (string.IsNullOrWhiteSpace(tasks.CommentBackOfficeBoss) == false)
                            {
                                tItem.Messages.Add(new PriceEngineeringTaskMessage()
                                {
                                    Author = backManagerBoss,
                                    Moment = moment.AddSeconds(2),
                                    Message = $"{tasks.CommentBackOfficeBoss}"
                                });
                            }
                        }
                    }

                    unitOfWork.SaveChanges();

                    foreach (var task in priceEngineeringTasks)
                    {
                        var tasks = task.GetPriceEngineeringTasks(unitOfWork);

                        var vm = new TaskViewModelBackManager(new TasksWrapperBackManager(tasks, container), container, task.Id);
                        if (vm.TasksTceItem.SccVersions.Where(x => x.IsActual).All(x => x.Version.HasValue) == false)
                        {
                            continue;
                        }

                        var t = task.GetAllPriceEngineeringTasks();
                        var finishMoment = tasks.PriceCalculations
                            .Where(x => x.TaskOpenMoment.HasValue)
                            .OrderBy(x => x.TaskOpenMoment.Value)
                            .LastOrDefault()?
                            .TaskCloseMoment;

                        foreach (var tItem in t)
                        {
                            var moment = tItem.Statuses.Max(x => x.Moment).AddSeconds(1);
                            var fm = finishMoment.HasValue && finishMoment.Value > moment
                                ? finishMoment.Value
                                : moment.AddSeconds(3);
                            tItem.Statuses.Add(new PriceEngineeringTaskStatus
                            {
                                Moment = fm,
                                StatusEnum = ScriptStep.LoadToTceFinish.Value
                            });
                        }
                    }

                    unitOfWork.SaveChanges();

                    priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTask>()
                        .Find(x => x.ParentPriceEngineeringTaskId == null)
                        .Where(x => x.GetAllPriceEngineeringTasks().All(x1 => x1.Status.Equals(ScriptStep.LoadToTceFinish)))
                        .Where(x => x.SalesUnits.All(s => s.Order != null))
                        .Where(x => x.SalesUnits.Select(s => s.Order).Distinct().Count() == 1)
                        .ToList();

                    foreach (var task in priceEngineeringTasks)
                    {
                        var mom = task.Statuses.Max(x => x.Moment).AddSeconds(1);
                        var dt = task.SalesUnits.First().SignalToStartProduction;
                        if (dt.HasValue && dt.Value > mom)
                            mom = dt.Value;

                        task.Statuses.Add(new PriceEngineeringTaskStatus
                        {
                            Moment = mom,
                            StatusEnum = ScriptStep.ProductionRequestStart.Value
                        });

                        task.Messages.Add(new PriceEngineeringTaskMessage()
                        {
                            Author = task.GetPriceEngineeringTasks(unitOfWork).BackManager,
                            Moment = mom.AddSeconds(1),
                            Message = $"Открыт з/з: {task.SalesUnits.First().Order.Number}"
                        });


                        dt = task.SalesUnits.First().SignalToStartProductionDone;
                        if (dt.HasValue && dt.Value > mom)
                            mom = dt.Value;
                        else
                            mom = mom.AddSeconds(2);

                        task.Statuses.Add(new PriceEngineeringTaskStatus
                        {
                            Moment = mom,
                            StatusEnum = ScriptStep.ProductionRequestFinish.Value
                        });
                    }

                    unitOfWork.SaveChanges();

                    messageService.ShowOkMessageDialog("", "Finish");


                    //unitOfWork.SaveChanges();
                    //unitOfWork.Dispose();

                    //_container.Resolve<IMessageService>().ShowOkMessageDialog("fin", "!!!");



                    ////var unitOfWork = container.Resolve<IUnitOfWork>();

                    //var dependent = new Dependent {Name = "dep1"};
                    //var mList = new List<Main>();
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    mList.Add(new Main {Name = $"main{i}", Dependent = dependent});
                    //}

                    //var serializer = new XmlSerializer(typeof(List<Main>));
                    //using (var fs = new FileStream(@"G:\main.xml", FileMode.Create))
                    //{
                    //    serializer.Serialize(fs, mList);
                    //}

                    //using (var fs = new FileStream(@"G:\main.xml", FileMode.Open))
                    //{
                    //    var list = (List<Main>)serializer.Deserialize(fs);
                    //    var dep = list.Select(x => x.Dependent).Distinct().ToList();
                    //}
                });
        }
    }

    [Serializable]
    public class Main
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "main";
        public Dependent Dependent { get; set; }
    }

    [Serializable]
    public class Dependent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "dependent";

    }
}