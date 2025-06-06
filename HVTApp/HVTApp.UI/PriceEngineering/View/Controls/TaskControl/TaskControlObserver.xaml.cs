﻿using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlObserver : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelObserver), typeof(TaskControlObserver), new PropertyMetadata(default(TaskViewModelObserver)));

        public TaskViewModelObserver TaskViewModel
        {
            get { return (TaskViewModelObserver) GetValue(TaskViewModelProperty); }
            set { SetValue(TaskViewModelProperty, value); }
        }

        public TaskControlObserver()
        {
            InitializeComponent();
        }
    }
}
