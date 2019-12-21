using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitPaymentsWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitPaymentsWrapper(SalesUnit model) : base(model) { }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
            PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
            RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);
        }
    }


    public class Payments2ViewModel : ViewModelBase
    {
        public Payments2ViewModel(IUnityContainer container) : base(container)
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
        }
    }
}