using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class LocalityLookupListViewModel
    {
        protected override bool UnionItemsAction(IUnitOfWork unitOfWork, Locality mainItem, List<Locality> otherItems)
        {
            //замена населенного пункта в адресах
            foreach (var locality in otherItems)
            {
                var addresses = unitOfWork.Repository<Address>().Find(address => Equals(address.Locality, locality)).ToList();
                addresses.ForEach(address => address.Locality = mainItem);
            }

            return true;
        }
    }
}