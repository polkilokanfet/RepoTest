using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskChatControl : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModel), typeof(TaskChatControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskChatControl()
        {
            InitializeComponent();
        }

        private void TextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return; // if Enter key is pressed...
            // ... and Ctrl key is NOT... then ignore it
            if (Keyboard.Modifiers == ModifierKeys.Control)
                if (TaskViewModel.Messenger.SendMessageCommand.CanExecute())
                    TaskViewModel.Messenger.SendMessageCommand.Execute();
        }
    }
}
