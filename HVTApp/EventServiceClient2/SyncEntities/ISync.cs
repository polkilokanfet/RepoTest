using System;

namespace EventServiceClient2.SyncEntities
{
    public interface ISync : IDisposable
    {
        Type ModelType { get; }
        Type EventType { get; }
        void Publish(object model);
    }
}