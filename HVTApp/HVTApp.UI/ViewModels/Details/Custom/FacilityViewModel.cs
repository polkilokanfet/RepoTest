using System;
using System.Collections.Generic;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
	public class FacilityViewModel : FacilityDetailsViewModel
	{
        public ICommand SelectAddressLocalityCommand { get; }
        public ICommand ClearAddressLocalityCommand { get; }

		public FacilityViewModel(IUnityContainer container) : base(container)
		{
            SelectAddressLocalityCommand = new DelegateLogCommand(
                () =>
                {
                    List<Locality> localities = UnitOfWork.Repository<Locality>().GetAll();
                    Locality locality = Container.Resolve<ISelectService>().SelectItem(localities, Item.Address.Locality?.Model.Id);
                    if (locality != null && locality.Id != Item.Address.Locality?.Model.Id)
                    {
                        locality = UnitOfWork.Repository<Locality>().GetById(locality.Id);
                        Item.Address.Locality = new LocalityWrapper(locality);
                    }
                });
			
            ClearAddressLocalityCommand = new DelegateLogCommand(
                () =>
                {
                    Item.Address.Locality = null;
                });
        }

        protected override void AfterLoading()
        {
            base.AfterLoading();

            //адрес не должен быть пустым
            if (Item.Address == null)
            {
                Item.Address = new AddressWrapper(new Address());
            }

            //реакция на смену владельца объекта
            this.Item.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Facility.OwnerCompany))
                {
                    if (Item.OwnerCompany != null && Item.Address.Locality == null)
                    {
                        Address address = Item.OwnerCompany.Model.GetCompanyOrParentAddress();
                        if (address == null)
                        {
                            return;
                        }
                        Item.Address.Locality = new LocalityWrapper(address.Locality);

                        if (string.IsNullOrEmpty(Item.Address.Description))
                        {
                            Item.Address.Description = Item.Model.ToString();
                        }
                    }
                }
            };
        }

    }
}
