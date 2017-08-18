using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            //HasOptional(x => x.RequestDocument).WithOptionalDependent();
            ////HasOptional(x => x.Author).WithMany().WillCascadeOnDelete(false);

            ////HasRequired(x => x.SenderEmployee).WithMany().WillCascadeOnDelete(false);
            ////HasRequired(x => x.RecipientEmployee).WithMany().WillCascadeOnDelete(false);

            //HasOptional(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId).WillCascadeOnDelete(false);

            //HasRequired(x => x.SenderEmployee).WithMany().HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);
            //HasRequired(x => x.RecipientEmployee).WithMany().HasForeignKey(x => x.RecipientId).WillCascadeOnDelete(false);

            ////HasMany(x => x.CopyToRecipients).WithMany();
            //HasOptional(x => x.RegistrationDetailsOfSender).WithRequired();
            //HasOptional(x => x.RegistrationDetailsOfRecipient).WithRequired();

            //Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}