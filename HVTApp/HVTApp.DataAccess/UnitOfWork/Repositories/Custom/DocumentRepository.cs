using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DocumentRepository
    {
        protected override IQueryable<Document> GetQuery()
        {
            return Context.Set<Document>().AsQueryable()
                .Include(document => document.Author)
                .Include(document => document.RecipientEmployee)
                .Include(document => document.SenderEmployee)
                .Include(document => document.RequestDocument);
        }
    }
}