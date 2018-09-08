using Prism.Modularity;
using Prism.Regions;
using System;

namespace HVTApp.Module.Price
{
    public class PriceModule : IModule
    {
        IRegionManager _regionManager;

        public PriceModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}