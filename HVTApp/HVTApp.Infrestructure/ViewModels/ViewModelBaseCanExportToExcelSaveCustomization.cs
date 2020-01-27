using System;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class ViewModelBaseCanExportToExcelSaveCustomization : ViewModelBaseCanExportToExcel
    {
        public ICommand SaveGridCustomisationsCommand { get; }

        public event Action SaveGridCustomisationEvent;

        protected ViewModelBaseCanExportToExcelSaveCustomization(IUnityContainer container) : base(container)
        {
            SaveGridCustomisationsCommand = new DelegateCommand(() => { SaveGridCustomisationEvent?.Invoke(); });
        }
    }
}