using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace HVTApp.Infrastructure.Prism
{
    public abstract class ModuleBase : IModule
    {
        protected IUnityContainer Container { get; }
        protected IRegionManager RegionManager { get; }

        protected ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }
        public void Initialize()
        {
            RegisterTypes();
            ResolveOutlookGroup();
        }

        protected abstract void RegisterTypes();
        protected abstract void ResolveOutlookGroup();
    }
}
