namespace HVTApp.DataAccess
{
    public partial class CompanyConfiguration 
    {
        public CompanyConfiguration()
        {
            HasRequired(x => x.Form).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.AddressLegal).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.AddressPost).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.ActivityFilds).WithMany();
            HasMany(x => x.BankDetailsList).WithOptional().WillCascadeOnDelete(true);
            HasOptional(x => x.ParentCompany).WithMany().WillCascadeOnDelete(false);
        }
    }
}