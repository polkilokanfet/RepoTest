using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class SelectedSpecificationChangedEvent : PubSubEvent<Specification> { }
}