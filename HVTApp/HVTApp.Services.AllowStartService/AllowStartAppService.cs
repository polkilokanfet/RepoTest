using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.AllowStartService
{
    public class AllowStartAppService : IAllowStartService
    {
        private readonly IUnityContainer _container;

        public AllowStartAppService(IUnityContainer container)
        {
            _container = container;
        }

        public bool AllowStart()
        {
            if (GlobalAppProperties.Actual.Developer != null)
            {
                var unitOfWork = _container.Resolve<IUnitOfWork>();
                var globalProperties = unitOfWork.Repository<GlobalProperties>().GetAll().OrderBy(properties => properties.Date).LastOrDefault();
                if (globalProperties != null && GlobalAppProperties.Actual.Developer.Id == GlobalAppProperties.User.Id)
                {
                    globalProperties.LastDeveloperVizit = DateTime.Today;
                    unitOfWork.SaveChanges();
                    GlobalAppProperties.Actual.LastDeveloperVizit = DateTime.Today;
                }

                if (GlobalAppProperties.Actual.LastDeveloperVizit.HasValue && (DateTime.Today - GlobalAppProperties.Actual.LastDeveloperVizit.Value).Days > 90)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
