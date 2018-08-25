using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Controls
{
    public partial class UnitListControl : UserControl
    {
        public UnitListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UnitsGroupsProperty = DependencyProperty.Register(
            "UnitsGroups", typeof(IEnumerable<IProductUnitsGroup>), typeof(UnitListControl), new PropertyMetadata(default(IEnumerable<IProductUnitsGroup>)));

        public IEnumerable<IProductUnitsGroup> UnitsGroups
        {
            get { return (IEnumerable<IProductUnitsGroup>) GetValue(UnitsGroupsProperty); }
            set { SetValue(UnitsGroupsProperty, value); }
        }



        public static readonly DependencyProperty SelectedUnitsGroupProperty = DependencyProperty.Register(
            "SelectedUnitsGroup", typeof(IProductUnitsGroup), typeof(UnitListControl), new PropertyMetadata(default(IProductUnitsGroup)));

        public IProductUnitsGroup SelectedUnitsGroup
        {
            get { return (IProductUnitsGroup) GetValue(SelectedUnitsGroupProperty); }
            set { SetValue(SelectedUnitsGroupProperty, value); }
        }
    }
}
