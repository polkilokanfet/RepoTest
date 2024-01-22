using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using HVTApp.Model;
using HVTApp.Model.Services;
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
                    try
                    {
                        _container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                        _container.Resolve<IEmailService>().SendMail("kosolapov.ep@mail.ru", "SubjTest", "BodyTest");
                        _container.Resolve<IMessageService>().Message("Send letter", "Success!");
                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IHvtAppLogger>().LogError(e.PrintAllExceptions(), e);
                        _container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.PrintAllExceptions());
                    }


                    //var unitOfWork = _container.Resolve<IUnitOfWork>();
                    //var tasks = unitOfWork.Repository<TechnicalRequrementsTask>()
                    //    .Find(task => task.HistoryElements.Any() &&
                    //                  task.HistoryElements.Any(x => x.Type == TechnicalRequrementsTaskHistoryElementType.Instruct) &&
                    //                  task.HistoryElements.Any(x => x.Type == TechnicalRequrementsTaskHistoryElementType.Start) == false);

                    //foreach (var task in tasks)
                    //{
                    //    var g = task.HistoryElements.Single(x => x.Type == TechnicalRequrementsTaskHistoryElementType.Create);
                    //    task.HistoryElements.Add(new TechnicalRequrementsTaskHistoryElement()
                    //    {
                    //        Type = TechnicalRequrementsTaskHistoryElementType.Start,
                    //        Moment = g.Moment.AddSeconds(1),
                    //        User = g.User
                    //    });
                    //}

                    //unitOfWork.SaveChanges();

                    //Clipboard.SetText(sb.ToString());
                });
        }
    }
}