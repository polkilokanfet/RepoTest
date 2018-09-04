using System.Windows.Controls;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ProjectView
    {
        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            var projectViewModel = container.Resolve<ProjectDetailsViewModel>();
            var detailsView = container.Resolve<ProjectDetailsView>();
            detailsView.DataContext = projectViewModel;
            DetailsControl.Content = detailsView;
            this.DataContext = projectViewModel;
        }
    }
}

