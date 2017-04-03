namespace HVTApp.Model.Wrapper
{
    internal class WhoRised
    {
        public WhoRised(object sender, string propertyName)
        {
            Sender = sender;
            PropertyName = propertyName;
        }

        public object Sender { get; }
        public string PropertyName { get; }

        public override bool Equals(object obj)
        {
            WhoRised other = obj as WhoRised;
            return other != null && Equals(this.Sender, other.Sender) && Equals(this.PropertyName, other.PropertyName);
        }
    }
}