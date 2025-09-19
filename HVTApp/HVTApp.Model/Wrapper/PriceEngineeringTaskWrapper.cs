using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public partial class PriceEngineeringTaskWrapper : WrapperBase<PriceEngineeringTask>
    {
        public override void InitializeOther()
        {
            if (Model.Statuses == null) throw new ArgumentException($"{nameof(Model.Statuses)} cannot be null");
            Statuses = new ValidatableChangeTrackingCollection<PriceEngineeringTaskStatusWrapper>(Model.Statuses.OrderBy(status => status.Moment).Select(status => new PriceEngineeringTaskStatusWrapper(status)));
            RegisterCollection(Statuses, Model.Statuses);
        }
    }
}