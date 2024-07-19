using System.Windows;
using HVTApp.Infrastructure.Interfaces;

namespace HVTApp.UI.Controls
{
    public partial class ButtonIsVisibleWhenCanExecute
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommandIsVisibleWhenCanExecute), 
            typeof(ButtonIsVisibleWhenCanExecute), 
            new PropertyMetadata(default(ICommandIsVisibleWhenCanExecute)));

        public ICommandIsVisibleWhenCanExecute Command
        {
            get => (ICommandIsVisibleWhenCanExecute) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", 
            typeof(object), 
            typeof(ButtonIsVisibleWhenCanExecute), 
            new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get => (object) GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ButtonIsVisibleWhenCanExecute()
        {
            InitializeComponent();
        }
    }
}
