using System.Collections;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectsView
    {
        public ProjectsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProjectsProperty = DependencyProperty.Register(
            "Projects", typeof (IEnumerable), typeof (ProjectsView), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Projects
        {
            get { return (IEnumerable) GetValue(ProjectsProperty); }
            set
            {
                SetValue(ProjectsProperty, value);
                //ProjectsDataGrid.DataSource = value;
            }
        }
    }
}
