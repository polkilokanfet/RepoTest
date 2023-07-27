using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
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


                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    int days = 180;
                    var blocks = unitOfWork.Repository<ProductBlock>()
                        .Find(block => block.Prices.Count > 1 && 
                                       block.Prices.Any(x => x.Date <= DateTime.Today.AddDays(days)));
                    foreach (var block in blocks)
                    {
                        foreach (var sumOnDate in block.Prices.Where(x => x.Date > DateTime.Today.AddDays(days)).ToList())
                        {
                            block.Prices.Remove(sumOnDate);
                            unitOfWork.Repository<SumOnDate>().Delete(sumOnDate);
                        }
                    }


                    unitOfWork.SaveChanges();
                    //StringBuilder sb = new StringBuilder();
                    //res
                    //    .OrderBy(x => x.Project.Manager.Id)
                    //    .ThenBy(x => x.Project.Id)
                    //    .ThenBy(x => x.Product.Id)
                    //    .ToList()
                    //    .ForEach(x => sb.AppendLine($"{x.Project.Manager.Employee.Person.Surname}: {x};"));
                    //Clipboard.SetText(sb.ToString());

                    _container.Resolve<IMessageService>().ShowOkMessageDialog("", $"Finish");

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