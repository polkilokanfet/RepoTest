using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class OrderPositionsCollection : ValidatableChangeTrackingCollection<SalesUnitWrapperTip>
    {
        public OrderPositionsCollection(IEnumerable<SalesUnitWrapperTip> items) : base(items)
        {
        }

        public override void AcceptChanges()
        {
            foreach (var item in this.ToList())
            {
                foreach (var item2 in this.Except(new[] {item}).ToList())
                {
                    if (item.Order.Number.Trim() == item2.Order.Number.Trim())
                    {
                        item2.Order = item.Order;
                    }
                }
            }

            base.AcceptChanges();
        }
    }
}