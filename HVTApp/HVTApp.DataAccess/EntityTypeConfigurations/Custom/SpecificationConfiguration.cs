namespace HVTApp.DataAccess
{
    public partial class SpecificationConfiguration 
    {
        public SpecificationConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Vat).IsRequired();
            HasRequired(x => x.Contract).WithMany();
        }
    }
}