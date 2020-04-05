namespace HVTApp.DataAccess
{
    public partial class ConstructorsParametersConfiguration
    {
        public ConstructorsParametersConfiguration()
        {
            HasMany(x => x.Constructors).WithMany();
            HasMany(x => x.PatametersLists).WithMany();
        }
    }
}