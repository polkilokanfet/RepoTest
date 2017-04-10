using System;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProjectWrapper
    {
        protected override void RunInConstructor()
        {
            this.ProjectsUnits.CollectionChanged += ProjectsUnitsOnCollectionChanged;
        }

        private void ProjectsUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public SumAndVatWrapper Cost { get; } = SumAndVatWrapper.GetWrapper();
    }
}
