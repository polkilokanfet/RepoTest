using System;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace HVTApp.Infrastructure.Prism
{
    public abstract class ModuleBase : IModule
    {
        protected IUnityContainer Container { get; }
        protected IRegionManager RegionManager { get; }
        private readonly IEventAggregator _eventAggregator;

        protected ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;

            _eventAggregator = Container.Resolve<IEventAggregator>();
        }

        public void Initialize()
        {
            RegisterTypes();
            ResolveOutlookGroup();
            _eventAggregator.GetEvent<ModuleIsInitializedEvent>().Publish(this.GetType());
        }

        /// <summary>
        /// Регистрация специфических для модуля типов
        /// </summary>
        protected abstract void RegisterTypes();

        /// <summary>
        /// Кнопка аутлук
        /// </summary>
        protected abstract void ResolveOutlookGroup();
    }

    public class ModuleIsInitializedEvent : PubSubEvent<Type>
    {
    }
}
