﻿using System;
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

                    var unitOfWork = _container.Resolve<IUnitOfWork>();

                    var priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTask>()
                        .Find(task => task.DesignDepartment != null);
                    var products = unitOfWork.Repository<Product>().GetAll();

                    var sb = new StringBuilder();
                    foreach (var priceEngineeringTask in priceEngineeringTasks)
                    {
                        foreach (var blockAdded in priceEngineeringTask.ProductBlocksAddedActual)
                        {
                            if (blockAdded.ProductBlock.IsKit == false) continue;
                            var product = products.Single(x => x.ProductBlock.Id == blockAdded.ProductBlock.Id);
                            priceEngineeringTask.DesignDepartment.Kits.Add(product);
                            sb.AppendLine($"{product.DesignationSpecial} => {priceEngineeringTask.DesignDepartment.Name}");
                        }
                    }

                    unitOfWork.SaveChanges();

                    container.Resolve<IMessageService>().Message("", sb.ToString());

                    //Clipboard.SetText(sb.ToString());
                });
        }
    }
}