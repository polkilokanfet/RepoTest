using System.Collections.Generic;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class SelectActivityFieldViewModel : ActivityFieldListViewModel
    {
        private readonly IEnumerable<ActivityFieldWrapper> _items;

        public SelectActivityFieldViewModel(IUnityContainer container, ActivityFieldWrapperDataService wrapperDataService, IEnumerable<ActivityFieldWrapper> items) : base(container, wrapperDataService)
        {
            _items = items;
        }

    }
}