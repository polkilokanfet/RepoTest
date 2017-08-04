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

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Interaction logic for SelectProduct.xaml
    /// </summary>
    public partial class SelectProduct : UserControl
    {
        public SelectProduct()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ParametersToSelectProperty = DependencyProperty.Register(
            "ParameterSelectors", typeof (ObservableCollection<ParameterSelector>), typeof (SelectProduct), new PropertyMetadata(default(ObservableCollection<ParameterSelector>)));

        public ObservableCollection<ParameterSelector> ParametersToSelect
        {
            get { return (ObservableCollection<ParameterSelector>) GetValue(ParametersToSelectProperty); }
            set { SetValue(ParametersToSelectProperty, value); }
        }
    }
}
