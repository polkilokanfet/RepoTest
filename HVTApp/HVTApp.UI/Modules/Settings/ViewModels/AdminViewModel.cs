using System.Linq;
using System.Windows.Input;
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
                    var unitOfWork = container.Resolve<IUnitOfWork>();
                    var reqs = unitOfWork.Repository<IncomingRequest>().Find(x => x.Performers.Any() && !x.InstructionDate.HasValue);
                    reqs.ForEach(x => x.InstructionDate = x.Document.Date);
                    unitOfWork.SaveChanges();
                });
        }
    }
}