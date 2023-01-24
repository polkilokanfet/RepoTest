using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesSingleControl : UserControl
    {
        #region Caption

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            "Caption", typeof(string), typeof(FilesSingleControl), new PropertyMetadata(default(string)));

        public string Caption
        {
            get => (string) GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        #endregion

        #region LoadAllFilesCommand

        public static readonly DependencyProperty LoadAllFilesCommandProperty = DependencyProperty.Register(
            "LoadAllFilesCommand", typeof(ICommand), typeof(FilesSingleControl), new PropertyMetadata(default(ICommand)));

        public ICommand LoadAllFilesCommand
        {
            get => (ICommand) GetValue(LoadAllFilesCommandProperty);
            set => SetValue(LoadAllFilesCommandProperty, value);
        }

        #endregion

        #region OpenFileCommand

        public static readonly DependencyProperty OpenFileCommandProperty = DependencyProperty.Register(
            "OpenFileCommand", typeof(ICommand), typeof(FilesSingleControl), new PropertyMetadata(default(ICommand)));

        public ICommand OpenFileCommand
        {
            get => (ICommand) GetValue(OpenFileCommandProperty);
            set => SetValue(OpenFileCommandProperty, value);
        }

        #endregion

        #region Files

        public static readonly DependencyProperty FilesProperty = DependencyProperty.Register("Files", typeof(IEnumerable<object>), typeof(FilesSingleControl), new PropertyMetadata(default(IEnumerable<object>)));

        public IEnumerable<object> Files
        {
            get => (IEnumerable<object>) GetValue(FilesProperty);
            set => SetValue(FilesProperty, value);
        }

        #endregion

        #region SelectedFile

        public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register("SelectedFile", typeof(object), typeof(FilesSingleControl), new PropertyMetadata(default(object)));

        public object SelectedFile
        {
            get => (object) GetValue(SelectedFileProperty);
            set => SetValue(SelectedFileProperty, value);
        }

        #endregion

        #region ButtonsArea

        public static readonly DependencyProperty ButtonsAreaProperty = DependencyProperty.Register("ButtonsArea", typeof(object), typeof(FilesSingleControl), new PropertyMetadata(default(object)));

        public object ButtonsArea
        {
            get => (object) GetValue(ButtonsAreaProperty);
            set => SetValue(ButtonsAreaProperty, value);
        }

        #endregion

        public FilesSingleControl()
        {
            InitializeComponent();
        }
    }
}
