using System;
using HVTApp.Infrastructure;
using HVTApp.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = $"Управление продажами ВВА / \"Make UETM great again! (c)\" [{GlobalAppProperties.User.Employee.Person}] [{GlobalAppProperties.User.RoleCurrent.ToString()}]";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<Uri> NavigateCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<Uri>(Navigate);
            Commands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private void Navigate(Uri uri)
        {
            if (uri != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }
    }
}
