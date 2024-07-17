namespace HVTApp.Model.Wrapper.Base
{
    public readonly struct DataErrorInfo
    {
        public string PropertyName { get; }
        public string Message { get; }

        public DataErrorInfo(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public override bool Equals(object obj)
        {
            return obj is DataErrorInfo other && this.Equals(other);
        }

        public bool Equals(DataErrorInfo other)
        {
            return PropertyName == other.PropertyName && Message == other.Message;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((PropertyName != null ? PropertyName.GetHashCode() : 0) * 397) ^ (Message != null ? Message.GetHashCode() : 0);
            }
        }
    }
}