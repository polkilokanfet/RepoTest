using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class AdminViewModel
    {
        private readonly IUnityContainer _container;
        public ICommand Command { get; }

        public AdminViewModel(IUnityContainer container)
        {
            _container = container;
            Command = new DelegateCommand(
                () =>
                {
                    //_container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                    //var unitOfWork = container.Resolve<IUnitOfWork>();

                    var dependent = new Dependent {Name = "dep1"};
                    var mList = new List<Main>();
                    for (int i = 0; i < 4; i++)
                    {
                        mList.Add(new Main {Name = $"main{i}", Dependent = dependent});
                    }

                    var serializer = new XmlSerializer(typeof(List<Main>));
                    using (var fs = new FileStream(@"G:\main.xml", FileMode.Create))
                    {
                        serializer.Serialize(fs, mList);
                    }

                    using (var fs = new FileStream(@"G:\main.xml", FileMode.Open))
                    {
                        var list = (List<Main>)serializer.Deserialize(fs);
                        var dep = list.Select(x => x.Dependent).Distinct().ToList();
                    }

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