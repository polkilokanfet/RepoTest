using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            HasOptional(x => x.RequestDocument).WithOptionalDependent();
            HasOptional(x => x.Author).WithMany();
            HasRequired(x => x.SenderEmployee).WithMany();
            HasRequired(x => x.RecipientEmployee).WithMany();
            HasMany(x => x.CopyToRecipients).WithMany();
            HasOptional(x => x.RegistrationDetailsOfSender).WithRequired();
            HasOptional(x => x.RegistrationDetailsOfRecipient).WithRequired();

            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}