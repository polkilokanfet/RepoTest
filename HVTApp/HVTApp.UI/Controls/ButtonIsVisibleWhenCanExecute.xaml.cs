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


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(ButtonIsVisibleWhenCanExecute), 
            new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ButtonIsVisibleWhenCanExecute()
        {
            InitializeComponent();
        }
    }
}
