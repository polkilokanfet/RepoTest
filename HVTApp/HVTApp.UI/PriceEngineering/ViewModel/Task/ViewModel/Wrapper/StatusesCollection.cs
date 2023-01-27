using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public class StatusesCollection : ValidatableChangeTrackingCollection<PriceEngineeringTaskStatusEmptyWrapper>
    {
        public StatusesCollection(IEnumerable<PriceEngineeringTaskStatus> items) 
            : base(items.Select(x => new PriceEngineeringTaskStatusEmptyWrapper(x)))
        {
        }

        public void Add(PriceEngineeringTaskStatusEnum statusEnum, string comment = null)
        {
            var status = new PriceEngineeringTaskStatus
            {
                StatusEnum = statusEnum,
                Moment = DateTime.Now,
                Comment = comment
            };

            this.Add(new PriceEngineeringTaskStatusEmptyWrapper(status));
        }
    }
}