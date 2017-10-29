using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DocumentsRegistrationDetailsRepository : BaseRepository<DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsRepository
    {
        public DocumentsRegistrationDetailsRepository(DbContext context) : base(context)
        {
        }
    }
}
