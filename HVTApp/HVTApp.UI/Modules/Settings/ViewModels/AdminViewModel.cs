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
                    var salesUnits = unitOfWork.Repository<PriceEngineeringTask>()
                        .Find(x => x.ParentPriceEngineeringTaskId == null && x.SalesUnits.Any())
                        .Where(x => x.SalesUnits.All(s => s.Order == null) && x.SalesUnits.Any(s => s.SignalToStartProduction.HasValue))
                        .Where(x => x.Status.Equals(ScriptStep.ProductionRequestFinish) == false && x.Status.Equals(ScriptStep.ProductionRequestStart) == false)
                        .SelectMany(x => x.SalesUnits)
                        .Where(x => x.SignalToStartProduction.HasValue)
                        .ToList();

                    salesUnits.ForEach(x => x.SignalToStartProduction = null);

                    var priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTask>()
                        .Find(x => x.Status.Equals(ScriptStep.ProductionRequestFinish) && x.UserPlanMaker == null)
                        .ToList();

                    var user = unitOfWork.Repository<User>().Find(x => x.Employee.Person.Surname == "Игнатенко").Single();
                    priceEngineeringTasks.ForEach(x => x.UserPlanMaker = user);

                    unitOfWork.SaveChanges();

                    messageService.ShowOkMessageDialog("", $"Finish {salesUnits.ToStringEnum()}");

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