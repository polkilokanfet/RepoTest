using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService.ChooseWindow
{
    /// <summary>
    /// Interaction logic for ChooseWindow.xaml
    /// </summary>
    public partial class ChooseWindow : Window, IDialog
    {
        public ChooseWindow()
        {
            InitializeComponent();
        }
    }
}
