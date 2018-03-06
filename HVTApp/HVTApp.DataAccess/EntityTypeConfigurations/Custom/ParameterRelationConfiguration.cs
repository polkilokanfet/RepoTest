namespace HVTApp.DataAccess
{
    public partial class ParameterRelationConfiguration 
    {
        public ParameterRelationConfiguration()
        {
            HasMany(x => x.RequiredParameters).WithMany();
        }
    }
}