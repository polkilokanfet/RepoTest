using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Interaction logic for SelectParameter.xaml
    /// </summary>
    public partial class SelectParameter : UserControl
    {
        public SelectParameter()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register(
            "Group", typeof (ParameterGroup), typeof (SelectParameter), new PropertyMetadata(default(ParameterGroup)));

        public ParameterGroup Group
        {
            get { return (ParameterGroup) GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }




        public static readonly DependencyProperty ParametersProperty = DependencyProperty.Register(
            "ParametersWithActualFlag", typeof (ObservableCollection<Parameter>), typeof (SelectParameter), new PropertyMetadata(default(ObservableCollection<Parameter>)));

        public ObservableCollection<Parameter> Parameters
        {
            get { return (ObservableCollection<Parameter>) GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }




        public static readonly DependencyProperty SelectedParameterProperty = DependencyProperty.Register(
            "SelectedParameter", typeof (Parameter), typeof (SelectParameter), new PropertyMetadata(default(Parameter)));

        public Parameter SelectedParameter
        {
            get { return (Parameter) GetValue(SelectedParameterProperty); }
            set { SetValue(SelectedParameterProperty, value); }
        }
    }
}
