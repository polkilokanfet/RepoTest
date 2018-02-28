namespace HVTApp.UI.Events
{
    public class PubSubEventArgs<TEntity>
    {
        public object Sender { get; }
        public TEntity Entity { get; }

        public PubSubEventArgs(object sender, TEntity entity)
        {
            Sender = sender;
            Entity = entity;
        }
    }
}