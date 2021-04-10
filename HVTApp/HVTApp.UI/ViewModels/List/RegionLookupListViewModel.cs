using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class RegionLookupListViewModel
    {
        protected override bool UnionItemsAction(IUnitOfWork unitOfWork, Region mainItem, List<Region> otherItems)
        {
            //замена региона в населенных пунктах
            foreach (var region in otherItems)
            {
                var localities = unitOfWork.Repository<Locality>().Find(locality => Equals(locality.Region, region)).ToList();
                localities.ForEach(locality => locality.Region = mainItem);
            }

            return true;
        }
    }
}