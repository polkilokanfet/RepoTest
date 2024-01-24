namespace HVTApp.DataAccess
{
    public partial class CompanyConfiguration 
    {
        public CompanyConfiguration()
        {
            HasRequired(company => company.Form).WithMany().WillCascadeOnDelete(false);
            HasOptional(company => company.AddressLegal).WithMany().WillCascadeOnDelete(false);
            HasOptional(company => company.AddressPost).WithMany().WillCascadeOnDelete(false);
            HasMany(company => company.ActivityFilds).WithMany();
            HasMany(company => company.BankDetailsList).WithOptional().WillCascadeOnDelete(true);
            HasOptional(company => company.ParentCompany).WithMany().WillCascadeOnDelete(false);
        }
    }
}