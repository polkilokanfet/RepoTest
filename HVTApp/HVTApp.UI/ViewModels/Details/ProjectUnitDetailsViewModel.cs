using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    //public class ProjectUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    //{
    //    public ICommand SelectProductCommand { get; private set; }
    //    public ICommand SelectFacilityCommand { get; private set; }
    //    public ICommand SelectProducerCommand { get; private set; }
    //    public ICommand SelectProjectCommand { get; private set; }

    //    public ProjectUnitDetailsViewModel(IUnityContainer container) : base(container) { }

    //    protected override void InitSpecialCommands()
    //    {
    //        SelectProductCommand = new DelegateCommand(SelectProduct_Execute);
    //        SelectFacilityCommand = new DelegateCommand(SelectFacility_Execute);
    //        SelectProducerCommand = new DelegateCommand(SelectProducer_Execute);
    //        SelectProjectCommand = new DelegateCommand(SelectProject_Execute);
    //    }

    //    private async void SelectProduct_Execute()
    //    {
    //        var product = await Container.Resolve<IGetProductService>().GetProductAsync(Item.Product?.Model);
    //        if (product == null || Equals(Item.Product?.Id, product.Id)) return;

    //        var prod = await WrapperDataService.GetRepository<Product>().GetByIdAsync(product.Id);
    //        Item.Product = new ProductWrapper(prod);
    //    }

    //    private async void SelectProject_Execute()
    //    {
    //        var projects = await WrapperDataService.GetRepository<Project>().GetAllAsync();
    //        //SelectAndSetWrapper<Project, ProjectWrapper>(projects, nameof(Item.Project));
    //    }

    //    private async void SelectProducer_Execute()
    //    {
    //        var companies = await WrapperDataService.GetRepository<Company>().GetAllAsync();
    //        var producers = companies.Where(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
    //        SelectAndSetWrapper<Company, CompanyWrapper>(producers, nameof(Item.Producer));
    //    }

    //    private async void SelectFacility_Execute()
    //    {
    //        var facilities = await WrapperDataService.GetRepository<Facility>().GetAllAsync();
    //        SelectAndSetWrapper<Facility, FacilityWrapper>(facilities, nameof(Item.Facility));
    //    }

    //}
}
