using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context)
        {
        }
    }
}
