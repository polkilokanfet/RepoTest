using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial interface IDocumentRepository
    {
        IEnumerable<Document> GetAllOfCurrentUser();
    }

    public partial class DocumentRepository
    {
        protected override IQueryable<Document> GetQuery()
        {
            return Context.Set<Document>().AsQueryable()
                .Include(document => document.Author)
                .Include(document => document.RecipientEmployee)
                .Include(document => document.SenderEmployee)
                .Include(document => document.WhoRegisteredUser)
                .Include(document => document.RequestDocument);
        }


        public IEnumerable<Document> GetAllOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuery()
                .Where(document => document.WhoRegisteredUserId.HasValue && 
                                   document.WhoRegisteredUserId.Value == GlobalAppProperties.User.Id)
                .AsNoTracking()
                .ToList();
        }
    }

    public partial class DocumentRepositoryTest
    {
        public IEnumerable<Document> GetAllOfCurrentUser()
        {
            throw new System.NotImplementedException();
        }
    }
}