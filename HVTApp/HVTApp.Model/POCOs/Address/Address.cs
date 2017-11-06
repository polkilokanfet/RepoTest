using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Address : BaseEntity
    {
        public string Description { get; set; }
        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            return Locality.ToString() + "; " + Description;
        }

    }
}