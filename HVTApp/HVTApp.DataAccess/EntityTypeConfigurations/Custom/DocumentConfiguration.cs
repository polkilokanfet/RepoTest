namespace HVTApp.DataAccess
{
    public partial class DocumentConfiguration 
    {
        public DocumentConfiguration()
        {
            HasOptional(x => x.RequestDocument).WithOptionalDependent();

            HasOptional(x => x.Author).WithMany().WillCascadeOnDelete(false);

            HasRequired(x => x.SenderEmployee).WithMany().HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);
            HasRequired(x => x.RecipientEmployee).WithMany().HasForeignKey(x => x.RecipientId).WillCascadeOnDelete(false);

            HasMany(x => x.CopyToRecipients).WithMany();

            HasRequired(x => x.RegistrationDetailsOfSender).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.RegistrationDetailsOfRecipient).WithMany().WillCascadeOnDelete(false);

            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}