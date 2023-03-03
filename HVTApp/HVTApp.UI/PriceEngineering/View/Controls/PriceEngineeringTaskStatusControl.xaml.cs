using System.Windows;
using System.Windows.Controls;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskStatusControl : UserControl
    {
        public static readonly DependencyProperty StatusEnumProperty = DependencyProperty.Register(
            "StatusEnum", typeof(ScriptStep), typeof(PriceEngineeringTaskStatusControl), new PropertyMetadata(default(ScriptStep)));

        public ScriptStep StatusEnum
        {
            get => (ScriptStep) GetValue(StatusEnumProperty);
            set => SetValue(StatusEnumProperty, value);
        }

        public PriceEngineeringTaskStatusControl()
        {
            InitializeComponent();
        }
    }
}
