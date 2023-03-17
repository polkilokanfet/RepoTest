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

                    var tasks = unitOfWork.Repository<PriceEngineeringTask>()
                        .Find(task => task.DesignDepartment == null)
                        .ToList();

                    foreach (var task in tasks)
                    {
                        var moment = task.Statuses.Max(x => x.Moment);
                        task.Statuses.Single(x => x.StatusEnum == ScriptStep.Create.Value).Moment = moment;
                        task.Statuses.Single(x => x.StatusEnum == ScriptStep.ProductionRequestStart.Value).Moment = moment.AddSeconds(2);
                        task.Statuses.Add(new PriceEngineeringTaskStatus
                        {
                            Moment = moment.AddSeconds(1), StatusEnum = ScriptStep.Start.Value
                        });

                        var technicalRequrements = unitOfWork.Repository<TechnicalRequrements>()
                            .Find(x =>
                                x.IsActual &&
                                x.SalesUnits.AllContainsIn(task.SalesUnits))
                            .First();

                        var trt = unitOfWork.Repository<TechnicalRequrementsTask>()
                            .Find(x => x.Requrements.Contains(technicalRequrements)).First();

                        var priceEngineeringTasks = task.GetPriceEngineeringTasks(unitOfWork);
                        priceEngineeringTasks.BackManager = trt.BackManager;

                        foreach (var file in task.FilesTechnicalRequirements.ToList())
                        {
                            task.FilesTechnicalRequirements.Remove(file);
                            unitOfWork.Repository<PriceEngineeringTaskFileTechnicalRequirements>().Delete(file);
                        }

                        foreach (var file in technicalRequrements.Files.Where(x => x.IsActual))
                        {
                            var f = new PriceEngineeringTaskFileTechnicalRequirements
                            {
                                Name = file.Name, CreationMoment = file.Date, Id = file.Id
                            };
                            task.FilesTechnicalRequirements.Add(f);
                        }
                    }

                    unitOfWork.SaveChanges();

                    messageService.ShowOkMessageDialog("", $"Finish {tasks.ToStringEnum()}");

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