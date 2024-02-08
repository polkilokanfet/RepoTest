namespace HVTApp.DataAccess
{
    public partial class NotificationUnitConfiguration
    {
        public NotificationUnitConfiguration()
        {
            HasRequired(x => x.RecipientUser).WithMany().HasForeignKey(x => x.RecipientUserId).WillCascadeOnDelete(false);
            HasRequired(x => x.SenderUser).WithMany().HasForeignKey(x => x.SenderUserId).WillCascadeOnDelete(false);
        }
    }
}