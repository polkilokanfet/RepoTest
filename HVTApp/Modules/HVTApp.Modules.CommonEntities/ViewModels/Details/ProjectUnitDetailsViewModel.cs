using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitDetailsViewModel
    {
        public ICommand SelectProductCommand { get; private set; }
        public ICommand SelectFacilityCommand { get; private set; }
        public ICommand SelectProducerCommand { get; private set; }

        protected override void InitCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProduct_Execute);
            SelectFacilityCommand = new DelegateCommand(SelectFacility_Execute);
            SelectProducerCommand = new DelegateCommand(SelectProducer_Execute);
        }

        private async void SelectProduct_Execute()
        {
            await Container.Resolve<IGetProductService>().GetProductAsync(Item.Product?.Model);
        }

        private async void SelectProducer_Execute()
        {
            var producers = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            var producerLookups = producers.Where(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum)
                            .Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment)).Select(x => new CompanyLookup(x));
            var producer = Container.Resolve<ISelectService>().SelectItem(producerLookups);
            if (producer != null)
                Item.Producer = new CompanyWrapper(producer.Entity);
        }

        private async void SelectFacility_Execute()
        {
            var facilities = (await UnitOfWork.GetRepository<Facility>().GetAllAsync()).Select(x => new FacilityLookup(x));
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities);
            if(facility != null)
                Item.Facility = new FacilityWrapper(facility.Entity);
        }
    }
}
