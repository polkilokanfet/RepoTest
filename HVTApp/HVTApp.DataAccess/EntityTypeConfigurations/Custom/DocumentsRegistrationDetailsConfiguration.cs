namespace HVTApp.DataAccess
{
    public partial class DocumentsRegistrationDetailsConfiguration
    {
        public DocumentsRegistrationDetailsConfiguration()
        {
            Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
            Property(x => x.RegistrationDate).IsRequired();
        }
    }
}