using System.Windows;
using System.Windows.Controls;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskStatusControl : UserControl
    {
        public static readonly DependencyProperty StatusEnumProperty = DependencyProperty.Register(
            "StatusEnum", typeof(PriceEngineeringTaskStatusEnum), typeof(PriceEngineeringTaskStatusControl), new PropertyMetadata(default(PriceEngineeringTaskStatusEnum)));

        public PriceEngineeringTaskStatusEnum StatusEnum
        {
            get => (PriceEngineeringTaskStatusEnum) GetValue(StatusEnumProperty);
            set => SetValue(StatusEnumProperty, value);
        }

        public PriceEngineeringTaskStatusControl()
        {
            InitializeComponent();
        }
    }
}
