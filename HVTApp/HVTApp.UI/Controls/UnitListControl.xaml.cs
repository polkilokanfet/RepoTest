using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.UI.Converter;

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




        public static readonly DependencyProperty ChangeCommandProperty = DependencyProperty.Register(
            "ChangeCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand ChangeCommand
        {
            get { return (ICommand) GetValue(ChangeCommandProperty); }
            set { SetValue(ChangeCommandProperty, value); }
        }
    }
}
