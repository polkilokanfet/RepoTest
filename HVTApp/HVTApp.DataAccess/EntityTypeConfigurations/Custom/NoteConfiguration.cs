namespace HVTApp.DataAccess
{
    public partial class NoteConfiguration
    {
        public NoteConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Text).IsRequired();
        }
    }
}