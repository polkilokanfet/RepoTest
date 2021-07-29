using System;
using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.Controls
{
    public partial class LoadableContentControl : UserControl
    {
        public static readonly DependencyProperty ContentIsLoadedProperty = DependencyProperty.Register(
            nameof(ContentIsLoaded), typeof(bool), typeof(LoadableContentControl), new PropertyMetadata(default(bool)));

        public bool ContentIsLoaded
        {
            get => (bool) GetValue(ContentIsLoadedProperty);
            set => SetValue(ContentIsLoadedProperty, value);
        }

        public LoadableContentControl()
        {
            InitializeComponent();
        }
    }
}
