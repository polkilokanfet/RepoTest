using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
    public class AfterSaveDirectumTaskSyncEvent : PubSubEvent<DirectumTask> { }
}