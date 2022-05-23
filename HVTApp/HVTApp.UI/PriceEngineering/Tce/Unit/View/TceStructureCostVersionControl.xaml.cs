using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public partial class TceStructureCostVersionControl : UserControl
    {
        public static readonly DependencyProperty TceStructureCostVersionProperty = DependencyProperty.Register(
            "TceStructureCostVersion", typeof(TceStructureCostVersion), typeof(TceStructureCostVersionControl), new PropertyMetadata(default(TceStructureCostVersion)));

        public TceStructureCostVersion TceStructureCostVersion
        {
            get { return (TceStructureCostVersion) GetValue(TceStructureCostVersionProperty); }
            set { SetValue(TceStructureCostVersionProperty, value); }
        }

        public TceStructureCostVersionControl()
        {
            InitializeComponent();
        }
    }
}
