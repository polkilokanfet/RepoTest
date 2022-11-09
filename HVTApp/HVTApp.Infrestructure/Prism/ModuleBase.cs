using System;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Ioc;
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

        //public void Initialize()
        //{
        //}

        /// <summary>
        /// Регистрация специфических для модуля типов
        /// </summary>
        protected abstract void RegisterTypes();

        /// <summary>
        /// Кнопка аутлук
        /// </summary>
        protected abstract void ResolveOutlookGroup();

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterTypes();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ResolveOutlookGroup();
            _eventAggregator.GetEvent<ModuleIsInitializedEvent>().Publish(this.GetType());
        }
    }

    public class ModuleIsInitializedEvent : PubSubEvent<Type>
    {
    }
}
