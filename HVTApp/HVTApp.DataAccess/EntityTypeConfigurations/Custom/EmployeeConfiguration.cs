namespace HVTApp.DataAccess
{
    public partial class EmployeeConfiguration
    {
        public EmployeeConfiguration()
        {
            HasRequired(x => x.Company).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Position).WithMany().WillCascadeOnDelete(false);

            Property(x => x.PersonalNumber).IsOptional();
        }
    }
}