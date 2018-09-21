namespace HVTApp.DataAccess
{
    public partial class DocumentsRegistrationDetailsConfiguration
    {
        public DocumentsRegistrationDetailsConfiguration()
        {
            Property(x => x.Date).IsRequired();
        }
    }
}