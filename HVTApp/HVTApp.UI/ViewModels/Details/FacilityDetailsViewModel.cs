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
                addresses = addresses.Except(UnitOfWork.Repository<Facility>().Find(x => x.Address != null).Select(x => x.Address)).ToList();
                return addresses;
            };
        }
    }
}