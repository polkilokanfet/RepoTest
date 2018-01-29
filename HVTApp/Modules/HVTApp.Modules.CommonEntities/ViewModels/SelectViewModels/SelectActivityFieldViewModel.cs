using System.Collections.Generic;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class SelectActivityFieldServiceViewModel : ActivityFieldListServiceViewModel
    {
        private readonly IEnumerable<ActivityFieldWrapper> _items;

        public SelectActivityFieldServiceViewModel(IUnityContainer container, IEnumerable<ActivityFieldWrapper> items) : base(container)
        {
            _items = items;
        }

    }
}