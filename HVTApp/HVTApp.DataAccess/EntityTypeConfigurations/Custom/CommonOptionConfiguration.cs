namespace HVTApp.DataAccess
{
    public partial class CommonOptionConfiguration
    {
        public CommonOptionConfiguration()
        {
            HasMany(x => x.StandartPaymentsConditions).WithOptional().WillCascadeOnDelete(false);
        }
    }
}