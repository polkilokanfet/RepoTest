namespace HVTApp.Model
{
    public class Address : BaseEntity
    {
        public string Description { get; set; }
        public virtual Locality Locality { get; set; }
    }
}