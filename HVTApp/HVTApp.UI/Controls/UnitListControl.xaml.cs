using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Controls
{
    public partial class UnitListControl
    {
        public UnitListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UnitsGroupsProperty = DependencyProperty.Register(
            "UnitsGroups", typeof(IEnumerable<IUnitsGroup>), typeof(UnitListControl), new PropertyMetadata(default(IEnumerable<IUnitsGroup>)));

        public IEnumerable<IUnitsGroup> UnitsGroups
        {
            get { return (IEnumerable<IUnitsGroup>) GetValue(UnitsGroupsProperty); }
            set { SetValue(UnitsGroupsProperty, value); }
        }



        public static readonly DependencyProperty SelectedUnitsGroupProperty = DependencyProperty.Register(
            "SelectedUnitsGroup", typeof(IUnitsGroup), typeof(UnitListControl), new PropertyMetadata(default(IUnitsGroup)));

        public IUnitsGroup SelectedUnitsGroup
        {
            get { return (IUnitsGroup) GetValue(SelectedUnitsGroupProperty); }
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



        public static readonly DependencyProperty ChangePaymentsCommandProperty = DependencyProperty.Register(
            "ChangePaymentsCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand ChangePaymentsCommand
        {
            get { return (ICommand) GetValue(ChangePaymentsCommandProperty); }
            set { SetValue(ChangePaymentsCommandProperty, value); }
        }
    }
}
