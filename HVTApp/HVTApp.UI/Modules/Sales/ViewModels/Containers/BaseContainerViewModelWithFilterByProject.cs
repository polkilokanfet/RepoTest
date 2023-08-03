using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public abstract class BaseContainerViewModelWithFilterByProject<TItem, TLookup, TAfterSaveItemEvent, TAfterRemoveItemEvent, TEditView> :
        BaseContainerViewModel<TItem, TLookup, TAfterSaveItemEvent, TAfterRemoveItemEvent,
            TEditView>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem>
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
    {
        /// <summary>
        /// Проект, выбранный в текущий момент
        /// </summary>
        protected Project SelectedProject;

        protected BaseContainerViewModelWithFilterByProject(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container)
        {
            vm.SelectedProjectItemChanged += item =>
            {
                this.SelectedProject = item?.Project;
                this.Clear();
                if (item != null)
                {
                    this.AddRange(GetActualLookups(item.Project));
                }

                this.SelectedItem = null;
            };
        }

        protected abstract IEnumerable<TLookup> GetActualLookups(Project project);
    }
}