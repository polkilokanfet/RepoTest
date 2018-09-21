namespace HVTApp.DataAccess
{
    public partial class ContractConfiguration 
    {
        public ContractConfiguration()
        {
            HasRequired(x => x.Contragent).WithMany();
        }
    }
}