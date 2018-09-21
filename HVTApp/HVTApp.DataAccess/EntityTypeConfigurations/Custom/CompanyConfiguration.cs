namespace HVTApp.DataAccess
{
    public partial class CompanyConfiguration 
    {
        public CompanyConfiguration()
        {
            HasRequired(x => x.Form).WithMany();
            HasOptional(x => x.AddressLegal).WithMany();
            HasOptional(x => x.AddressPost).WithMany();
            HasMany(x => x.ActivityFilds).WithMany();
            HasMany(x => x.BankDetailsList).WithOptional();
            HasOptional(x => x.ParentCompany).WithMany();
        }
    }
}