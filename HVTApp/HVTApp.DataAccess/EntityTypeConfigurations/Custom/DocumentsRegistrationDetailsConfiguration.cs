namespace HVTApp.DataAccess
{
    public partial class DocumentsRegistrationDetailsConfiguration
    {
        public DocumentsRegistrationDetailsConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(20);
            Property(x => x.Date).IsRequired();
        }
    }
}