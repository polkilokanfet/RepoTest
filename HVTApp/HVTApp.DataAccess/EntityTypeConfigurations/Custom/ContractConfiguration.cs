namespace HVTApp.DataAccess
{
    public partial class ContractConfiguration 
    {
        public ContractConfiguration()
        {
            HasRequired(contract => contract.Contragent).WithMany().WillCascadeOnDelete(false);
            HasOptional(contract => contract.ContragentEmployee).WithMany().WillCascadeOnDelete(false);
        }
    }
}