using System;
using System.ComponentModel;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectUnitGroupWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += GroupPropertyChanged;
        }

        private void GroupPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {

            switch (propertyChangedEventArgs.PropertyName)
            {
                case nameof(this.Product):
                    ProjectUnits.ForEach(x => x.Product = Product);
                    break;
                case nameof(this.Cost):
                    ProjectUnits.ForEach(x => x.Cost = Cost);
                    break;
                case nameof(this.Facility):
                    ProjectUnits.ForEach(x => x.Facility = Facility);
                    break;
                case nameof(this.DeliveryDate):
                    ProjectUnits.ForEach(x => x.DeliveryDate = DeliveryDate);
                    break;
                case nameof(this.Producer):
                    ProjectUnits.ForEach(x => x.Producer = Producer);
                    break;
            }
        }
    }
}