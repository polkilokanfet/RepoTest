using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DocumentsRegistrationDetailsConfiguration : EntityTypeConfiguration<DocumentsRegistrationDetails>
    {
        public DocumentsRegistrationDetailsConfiguration()
        {
            Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
            Property(x => x.RegistrationDate).IsRequired();
        }
    }
}