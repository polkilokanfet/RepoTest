using System.Collections;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectListView
    {
        public ProjectListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProjectsProperty = DependencyProperty.Register(
            "Projects", typeof (IEnumerable), typeof (ProjectListView), new PropertyMetadata(default(IEnumerable)));

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
