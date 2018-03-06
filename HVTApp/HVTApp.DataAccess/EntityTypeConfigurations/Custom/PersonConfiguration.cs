namespace HVTApp.DataAccess
{
    public partial class PersonConfiguration 
    {
        public PersonConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Surname).IsRequired().HasMaxLength(50);
        }
    }
}