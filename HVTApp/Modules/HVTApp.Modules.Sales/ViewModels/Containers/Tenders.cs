using System.Collections.ObjectModel;
using HVTApp.UI.Lookup;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Tenders : ObservableCollection<TenderLookup>, IShowable
    {
        public void ShowByProject(ProjectLookup projectLookup)
        {
            this.Clear();
            if (projectLookup != null)
                this.AddRange(projectLookup.Tenders);
        }
    }
}