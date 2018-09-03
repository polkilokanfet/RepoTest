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




        public static readonly DependencyProperty ChangeFacilityCommandProperty = DependencyProperty.Register(
            "ChangeFacilityCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand ChangeFacilityCommand
        {
            get { return (ICommand) GetValue(ChangeFacilityCommandProperty); }
            set { SetValue(ChangeFacilityCommandProperty, value); }
        }




        public static readonly DependencyProperty ChangeProductCommandProperty = DependencyProperty.Register(
            "ChangeProductCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand ChangeProductCommand
        {
            get { return (ICommand) GetValue(ChangeProductCommandProperty); }
            set { SetValue(ChangeProductCommandProperty, value); }
        }


        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(
            "AddCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddCommand
        {
            get { return (ICommand) GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }




        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register(
            "RemoveCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveCommand
        {
            get { return (ICommand) GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }



        public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register(
            "RefreshCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand RefreshCommand
        {
            get { return (ICommand) GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
    }
}
