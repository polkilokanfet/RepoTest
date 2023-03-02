using System.Windows;
using System.Windows.Controls;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskStatusControl : UserControl
    {
        public static readonly DependencyProperty StatusEnumProperty = DependencyProperty.Register(
            "StatusEnum", typeof(ScriptStep2), typeof(PriceEngineeringTaskStatusControl), new PropertyMetadata(default(ScriptStep2)));

        public ScriptStep2 StatusEnum
        {
            get => (ScriptStep2) GetValue(StatusEnumProperty);
            set => SetValue(StatusEnumProperty, value);
        }

        public PriceEngineeringTaskStatusControl()
        {
            InitializeComponent();
        }
    }
}
