namespace HVTApp.DataAccess
{
    public partial class CreateNewProductTaskConfiguration
    {
        public CreateNewProductTaskConfiguration()
        {
            Property(x => x.Designation).IsRequired();
            Property(x => x.StructureCostNumber).IsRequired();
            HasRequired(x => x.Product).WithOptional();
        }
    }
}