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
            "ParametersToSelect", typeof (ObservableCollection<ParametersToSelect>), typeof (SelectProduct), new PropertyMetadata(default(ObservableCollection<ParametersToSelect>)));

        public ObservableCollection<ParametersToSelect> ParametersToSelect
        {
            get { return (ObservableCollection<ParametersToSelect>) GetValue(ParametersToSelectProperty); }
            set { SetValue(ParametersToSelectProperty, value); }
        }
    }
}
