namespace HVTApp.DataAccess
{
    public partial class DocumentConfiguration 
    {
        public DocumentConfiguration()
        {
            HasRequired(x => x.Number).WithOptional().WillCascadeOnDelete(false);

            HasRequired(x => x.SenderEmployee).WithMany().HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);
            HasRequired(x => x.RecipientEmployee).WithMany().HasForeignKey(x => x.RecipientId).WillCascadeOnDelete(false);

            HasOptional(x => x.Author).WithMany().WillCascadeOnDelete(false);

            HasOptional(x => x.RequestDocument).WithMany().WillCascadeOnDelete(false);

            HasOptional(x => x.RegistrationDetailsOfRecipient).WithOptionalPrincipal().WillCascadeOnDelete(true);

            HasMany(x => x.CopyToRecipients).WithMany();

            Property(x => x.Comment).IsOptional();
        }
    }
}