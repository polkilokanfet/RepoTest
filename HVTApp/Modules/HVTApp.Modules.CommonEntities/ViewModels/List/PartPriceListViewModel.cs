using System;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    //public class PartPriceListViewModel : BaseWrapperListViewModel<PartWrapper, Product, PartDetailsViewModel, AfterSavePartEvent>
    //{
    //    private CostOnDateWrapper _selectedPrice;

    //    public PartPriceListViewModel(IUnityContainer container, PartWrapperDataService partWrapperDataService) : base(container, partWrapperDataService)
    //    {
    //        NewPriceCommand = new DelegateCommand(NewPriceCommand_Execute, NewPriceCommand_CanExecute);
    //    }

    //    public CostOnDateWrapper SelectedPrice
    //    {
    //        get { return _selectedPrice; }
    //        set
    //        {
    //            _selectedPrice = value;
    //            InvalidateCommands();
    //        }
    //    }

    //    public ICommand NewPriceCommand { get; }

    //    private void NewPriceCommand_Execute()
    //    {
    //        var costOnDate = new CostOnDate {Date = DateTime.Now};
    //        var wrapper = new CostOnDateWrapper(costOnDate);
    //        var flag = Container.Resolve<IUpdateDetailsService>().UpdateDetails<CostOnDate, CostOnDateWrapper>(wrapper);

    //        if (flag)
    //        {
    //            SelectedItem.Prices.Add(wrapper);
    //            SelectedItem.AcceptChanges();
    //            UnitOfWork.Complete();
    //        }
    //    }

    //    private bool NewPriceCommand_CanExecute()
    //    {
    //        return SelectedItem != null;
    //    }

    //    protected override void InvalidateCommands()
    //    {
    //        base.InvalidateCommands();
    //        ((DelegateCommand)NewPriceCommand).RaiseCanExecuteChanged();
    //    }
    //}
}
