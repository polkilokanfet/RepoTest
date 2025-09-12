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
        public DelegateLogCommand Command1 { get; }
        public DelegateLogCommand Command2 { get; }

        public AdminViewModel(IUnityContainer container)
        {
            Command1 = new DelegateLogCommand(
                () =>
                {
                    //try
                    //{
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ep@mail.ru", "SubjTest", "BodyTest");
                    //    _container.Resolve<IMessageService>().Message("Send letter", "Success!");
                    //}
                    //catch (Exception e)
                    //{
                    //    _container.Resolve<IHvtAppLogger>().LogError(e.PrintAllExceptions(), e);
                    //    _container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.PrintAllExceptions());
                    //}

                    var unitOfWork = container.Resolve<IUnitOfWork>();

                    var salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Specification != null && salesUnit.Producer == null);
                    var company = unitOfWork.Repository<Company>().GetById(GlobalAppProperties.Actual.OurCompany.Id);

                    var sb = new StringBuilder();
                    foreach (var salesUnit in salesUnits)
                    {
                        salesUnit.Producer = company;
                        sb.AppendLine(salesUnit.ToString());
                    }

                    unitOfWork.SaveChanges();

                    container.Resolve<IMessageService>().Message(sb.ToString());

                    //Clipboard.SetText(sb.ToString());
                });

            Command2 = new DelegateLogCommand(() =>
            {
                var folderPath = container.Resolve<IGetFilePaths>().GetFolderPath();
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                var gr = files.GroupBy(Path.GetFileName).Where(x => x.Count() > 1);
                StringBuilder sb = new StringBuilder();
                foreach (var g in gr)
                {
                    sb.AppendLine($"file name: {g.Key}");
                    foreach (var s in g)
                    {
                        sb.AppendLine($"    {s}");
                    }

                    sb.AppendLine("");
                }
                container.Resolve<IMessageService>().Message("", sb.ToString());
            });
        }
    }
}