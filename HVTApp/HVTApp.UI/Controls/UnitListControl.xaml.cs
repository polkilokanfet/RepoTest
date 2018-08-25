using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Controls
{
    public partial class UnitListControl : UserControl
    {
        public UnitListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UnitsProperty = DependencyProperty.Register(
            "Units", typeof(IEnumerable<IProductUnit>), typeof(UnitListControl), new PropertyMetadata(default(IEnumerable<IProductUnit>)));

        public IEnumerable<IProductUnit> Units
        {
            get { return (IEnumerable<IProductUnit>) GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }



        public static readonly DependencyProperty SelectedUnitProperty = DependencyProperty.Register(
            "SelectedUnit", typeof(IProductUnit), typeof(UnitListControl), new PropertyMetadata(default(IProductUnit)));

        public IProductUnit SelectedUnit
        {
            get { return (IProductUnit) GetValue(SelectedUnitProperty); }
            set { SetValue(SelectedUnitProperty, value); }
        }
    }
}
