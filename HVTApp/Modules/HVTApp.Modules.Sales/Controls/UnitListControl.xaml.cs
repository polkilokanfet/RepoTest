using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.Controls
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



        public static readonly DependencyProperty SelectedProductIncludedProperty = DependencyProperty.Register(
            "SelectedProductIncluded", typeof(ProductIncludedWrapper), typeof(UnitListControl), new PropertyMetadata(default(ProductIncludedWrapper)));

        public ProductIncludedWrapper SelectedProductIncluded
        {
            get { return (ProductIncludedWrapper) GetValue(SelectedProductIncludedProperty); }
            set { SetValue(SelectedProductIncludedProperty, value); }
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


        public static readonly DependencyProperty ChangePaymentsCommandProperty = DependencyProperty.Register(
            "ChangePaymentsCommand", typeof(ICommand), typeof(UnitListControl), new PropertyMetadata(default(ICommand)));

        public ICommand ChangePaymentsCommand
        {
            get { return (ICommand) GetValue(ChangePaymentsCommandProperty); }
            set { SetValue(ChangePaymentsCommandProperty, value); }
        }


        public static readonly DependencyProperty PriceErrorsProperty = DependencyProperty.Register(
            "PriceErrors", typeof(string), typeof(UnitListControl), new PropertyMetadata(default(string)));

        public string PriceErrors
        {
            get { return (string) GetValue(PriceErrorsProperty); }
            set { SetValue(PriceErrorsProperty, value); }
        }
    }
}
