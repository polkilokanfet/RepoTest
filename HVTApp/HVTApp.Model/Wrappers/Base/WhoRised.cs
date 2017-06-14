namespace HVTApp.Model.Wrappers
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
            if (other == null) return false;

            return Equals(this.Sender, other.Sender) && Equals(this.PropertyName, other.PropertyName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Sender != null ? Sender.GetHashCode() : 0)*397) ^ (PropertyName != null ? PropertyName.GetHashCode() : 0);
            }
        }
    }
}