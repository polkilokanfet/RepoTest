using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class FacilityDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectAddressCommand = () =>
            {
                //нельзя выбрать адрес из другого объекта
                var addresses = UnitOfWork.Repository<Address>().GetAll();
                addresses = addresses.Except(UnitOfWork.Repository<Facility>().Find(facility => facility.Address != null).Select(facility => facility.Address)).ToList();
                return addresses;
            };
        }
    }
}